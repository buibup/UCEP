using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Web;
using UCEP.Models;

namespace UCEP.Common
{
    public class Helper
    {
        
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
                //effectiveDate = DateTime.TryParse(row[(int)FsCatalogueEnum.EffectiveDate].ToString(), out effectiveDate)? effectiveDate : Convert.ToDateTime(row[(int)FsCatalogueEnum.EffectiveDate]);
                //approvalDate = DateTime.TryParse(row[(int)FsCatalogueEnum.ApprovalDate].ToString(), out approvalDate)? approvalDate : Convert.ToDateTime(row[(int)FsCatalogueEnum.ApprovalDate]);
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
    }
}