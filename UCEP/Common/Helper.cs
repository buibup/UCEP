using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UCEP.Models;
using System.IO;

namespace UCEP.Common
{
    public class Helper
    {
        private static UCEPDbContext db = new UCEPDbContext();
        public static List<FsCatalogue> DTFsCatalogueToModel(DataTable dt)
        {
            List<FsCatalogue> result = new List<FsCatalogue>();

            decimal price = 0;
            DateTime effectiveDate;
            DateTime approvalDate;

            foreach (DataRow row in dt.Rows)
            {
                FsCatalogue model = new FsCatalogue();
                decimal.TryParse(row[(int)FsCatalogueEnum.Price].ToString(), out price);
                var effDate = Convert.ToDateTime(row[(int)FsCatalogueEnum.EffectiveDate]).ToString("dd/MM/yyyy");
                var appDate = Convert.ToDateTime(row[(int)FsCatalogueEnum.ApprovalDate]).ToString("dd/MM/yyyy HH:mm");
                IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);

                effectiveDate = DateTime.Parse(effDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);
                approvalDate = DateTime.Parse(appDate, culture, System.Globalization.DateTimeStyles.AssumeLocal);

                model = new FsCatalogue()
                {
                    HospitalCode = row[(int)FsCatalogueEnum.HospitalCode].ToString(),
                    FSCodeNIEMS = row[(int)FsCatalogueEnum.FSCodeNIEMS].ToString(),
                    FSCodeHos = row[(int)FsCatalogueEnum.FSCodeHos].ToString(),
                    Category = row[(int)FsCatalogueEnum.Category].ToString(),
                    Meaning = row[(int)FsCatalogueEnum.Meaning].ToString(),
                    Unit = row[(int)FsCatalogueEnum.Unit].ToString(),
                    Price = price,
                    EffectiveDate = effectiveDate,
                    Status = row[(int)FsCatalogueEnum.Status].ToString(),
                    ApprovalDate = approvalDate
                };
                result.Add(model);
            }

            return result;
        }

        public static DateTime StringToDateTime(string dte, string tme)
        {
            var strDtm = Convert.ToDateTime($"{dte} {tme}").ToString("dd/MM/yyyy HH:mm:ss");
            IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);

            var dtm = DateTime.Parse(strDtm, culture, System.Globalization.DateTimeStyles.AssumeLocal);

            return dtm;
        }

        public static DateTime StringToDateTime(string datetime)
        {
            var strDtm = Convert.ToDateTime($"{datetime}").ToString("dd/MM/yyyy HH:mm:ss");
            IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);

            var dtm = DateTime.Parse(strDtm, culture, System.Globalization.DateTimeStyles.AssumeLocal);

            return dtm;
        }

        public static List<FsTemplate> DTToFsTemplateList(DataTable dt)
        {
            List<FsTemplate> result = new List<FsTemplate>();

            foreach (DataRow row in dt.Rows)
            {
                string hostCode = row["HospitalCode"].ToString();

                //var item = db.FsCatalogues.Where(m => m.FSCodeHos == hostCode).Select(m => new { m.FSCodeNIEMS, m.Category, m.Unit }).SingleOrDefault();
                var item = GlobalConfig.Connection.GetFsCatalogue(hostCode);

                decimal price = 0;

                var d = Convert.ToDateTime(row["OEORI_SttDat"].ToString());
                var t = Convert.ToDateTime(row["OEORI_SttTim"].ToString());

                var useDate = $"{d.Year}-{d.ToString("MM-dd")}";
                var useTime = $"{t.ToString("HH:mm:ss")}";

                string FSCodeNIEMS = string.Empty;
                string Category = string.Empty;
                string Unit = string.Empty;

                if (item != null)
                {
                    FSCodeNIEMS = item.FSCodeNIEMS;
                    Category = item.Category;
                }

                FsTemplate fs = new FsTemplate()
                {

                    UseDate = $"{useDate} {useTime}",
                    FSCodeOrTMTCode = FSCodeNIEMS,
                    HospitalCode = hostCode,
                    Category = Category,
                    Mean = row["Mean"].ToString(),
                    Unit = row["Unit"].ToString(),
                    PriceTotal = decimal.TryParse(row["PriceTotal"].ToString(), out price) ? price : 0
                };
                result.Add(fs);
            }

            result = result.OrderBy(a => string.IsNullOrEmpty(a.UseDate) ? (DateTime?)null : DateTime.ParseExact(a.UseDate, "yyyy-MM-dd HH:mm:ss", null)).ToList();

            return result;
        }

    }
}
