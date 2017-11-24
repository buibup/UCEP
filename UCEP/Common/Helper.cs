using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UCEP.Models;
using static UCEP.Enums;
using System.Text.RegularExpressions;

namespace UCEP.Common
{
    public static class Helper
    {
        private static UCEPDbContext db = new UCEPDbContext();

        public static List<FsCatalogue> DTFsCatalogueToModelList(this DataTable dt)
        {
            List<FsCatalogue> result = new List<FsCatalogue>();

            decimal price = 0;
            DateTime effectiveDate;
            DateTime approvalDate;
            FsCatalogue model;

            foreach (DataRow row in dt.Rows)
            {
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

        public static List<DrugCatalogue> DTDrugCatalogueToModelList(this DataTable dt)
        {
            var result = new List<DrugCatalogue>();
            decimal unitPrice = 0;
            DateTime? dateChange;
            DateTime? dateUpdate;
            DateTime? dateEffective;
            DateTime? dateApproved;
            DrugCatalogue model;

            foreach (DataRow row in dt.Rows)
            {
                decimal.TryParse(row[(int)DrugCatalogueEnum.UNITPRICE].ToString(), out unitPrice);
                dateChange = row[(int)DrugCatalogueEnum.DATECHANGE].ToString().StringToDate();
                dateUpdate = row[(int)DrugCatalogueEnum.DATEUPDATE].ToString().StringToDate();
                dateEffective = row[(int)DrugCatalogueEnum.DATEEFFECTIVE].ToString().StringToDate();
                dateApproved = row[(int)DrugCatalogueEnum.DATE_APPROVED].ToString().StringToDate();

                model = new DrugCatalogue()
                {
                    HospDrugCode = row[(int)DrugCatalogueEnum.HospDrugCode].ToString(),
                    PRODUCTCAT = int.Parse(row[(int)DrugCatalogueEnum.PRODUCTCAT].ToString()),
                    TMTID = row[(int)DrugCatalogueEnum.TMTID].ToString(),
                    SPECPREP = row[(int)DrugCatalogueEnum.SPECPREP].ToString(),
                    GENERICNAME = row[(int)DrugCatalogueEnum.GENERICNAME].ToString(),
                    TRADENAME = row[(int)DrugCatalogueEnum.TRADENAME].ToString(),
                    DFSCODE = row[(int)DrugCatalogueEnum.DFSCODE].ToString(),
                    DOSAGEFORM = row[(int)DrugCatalogueEnum.DOSAGEFORM].ToString(),
                    STRENGTH = row[(int)DrugCatalogueEnum.STRENGTH].ToString(),
                    CONTENT = row[(int)DrugCatalogueEnum.CONTENT].ToString(),
                    UNITPRICE = unitPrice,
                    DISTRIBUTOR = row[(int)DrugCatalogueEnum.DISTRIBUTOR].ToString(),
                    MANUFACTURER = row[(int)DrugCatalogueEnum.MANUFACTURER].ToString(),
                    ISED = row[(int)DrugCatalogueEnum.ISED].ToString(),
                    NDC24 = row[(int)DrugCatalogueEnum.NDC24].ToString(),
                    PACKSIZE = row[(int)DrugCatalogueEnum.PACKSIZE].ToString(),
                    PACKPRICE = row[(int)DrugCatalogueEnum.PACKPRICE].ToString(),
                    UPDATEFLAG = row[(int)DrugCatalogueEnum.UPDATEFLAG].ToString(),
                    DATECHANGE = dateChange,
                    DATEUPDATE = dateUpdate,
                    DATEEFFECTIVE = dateEffective,
                    ISED_APPROVED = row[(int)DrugCatalogueEnum.ISED_APPROVED].ToString(),
                    NDC24_APPROVED = row[(int)DrugCatalogueEnum.NDC24_APPROVED].ToString(),
                    DATE_APPROVED = dateApproved,
                    ISED_STATUS = row[(int)DrugCatalogueEnum.ISED_STATUS].ToString()
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

        public static DateTime? StringToDateTime(this string datetime)
        {
            if (string.IsNullOrEmpty(datetime)) return null;

            var strDtm = Convert.ToDateTime($"{datetime}").ToString("dd/MM/yyyy HH:mm:ss");
            IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);

            var dtm = DateTime.Parse(strDtm, culture, System.Globalization.DateTimeStyles.AssumeLocal);

            return dtm;
        }

        public static DateTime? StringToDate(this string date)
        {
            if (string.IsNullOrEmpty(date)) return null;

            var strDtm = Convert.ToDateTime($"{date}").ToString("dd/MM/yyyy");
            IFormatProvider culture = new System.Globalization.CultureInfo("fr-FR", true);

            var dtm = DateTime.Parse(strDtm, culture, System.Globalization.DateTimeStyles.AssumeLocal);

            return dtm;
        }

        public static List<FsDrugTemplate> DTToFsDrugTemplateList(DataTable dt, string hosCode)
        {
            List<FsDrugTemplate> result = new List<FsDrugTemplate>();

            foreach (DataRow row in dt.Rows)
            {
                var fsCat = new FsCatalogue();
                var drugCat = new DrugCatalogue();

                string orderItemCode = row["HospitalCode"].ToString();
                decimal price = 0;

                var d = Convert.ToDateTime(row["OEORI_SttDat"].ToString());
                var t = Convert.ToDateTime(row["OEORI_SttTim"].ToString());

                var useDate = $"{d.Year}-{d.ToString("MM-dd")}";
                var useTime = $"{t.ToString("HH:mm:ss")}";

                string CodeNIEMS = string.Empty;
                string Category = string.Empty;
                string Unit = string.Empty;

                if (row["ARCBG_Code"].ToString().Trim() == "00100000")
                {
                    // Drug
                    drugCat = GlobalConfig.Connection.GetDrugCatalogue(orderItemCode, hosCode);
                    if (drugCat != null)
                    {
                        CodeNIEMS = drugCat.TMTID;
                        Category = "3";
                    }

                    FsDrugTemplate fsTemplate = new FsDrugTemplate()
                    {

                        UseDate = $"{useDate} {useTime}",
                        FSCodeOrTMTCode = CodeNIEMS,
                        HospitalCode = orderItemCode,
                        Category = Category,
                        Mean = row["Mean"].ToString(),
                        Unit = row["Unit"].ToString(),
                        PriceTotal = decimal.TryParse(row["PriceTotal"].ToString(), out price) ? price : 0
                    };
                    result.Add(fsTemplate);
                }
                else
                {
                    // FS
                    fsCat = GlobalConfig.Connection.GetFsCatalogue(orderItemCode, hosCode);
                    if (fsCat != null)
                    {
                        CodeNIEMS = fsCat.FSCodeNIEMS;
                        Category = fsCat.Category;
                    }

                    FsDrugTemplate fsTemplate = new FsDrugTemplate()
                    {

                        UseDate = $"{useDate} {useTime}",
                        FSCodeOrTMTCode = CodeNIEMS,
                        HospitalCode = orderItemCode,
                        Category = Category,
                        Mean = row["Mean"].ToString(),
                        Unit = row["Unit"].ToString(),
                        PriceTotal = decimal.TryParse(row["PriceTotal"].ToString(), out price) ? price : 0
                    };
                    result.Add(fsTemplate);
                }
            }

            result = result.OrderBy(a => string.IsNullOrEmpty(a.UseDate) ? (DateTime?)null : DateTime.ParseExact(a.UseDate, "yyyy-MM-dd HH:mm:ss", null)).ToList();

            return result;
        }

        public static void SetCatalogue(this string catalogue)
        {
            GlobalConfig.Catalogue = catalogue;
            if (catalogue == Catalogue.DrugCatalogue.ToString())
            {
                GlobalConfig.DrugCatalogueList = GlobalConfig.Connection.GetAllDrugCatalogue();
            }
            else if (catalogue == Catalogue.FSCatalogue.ToString())
            {
                GlobalConfig.FsCatalogueList = GlobalConfig.Connection.GetAllFsCatalogueByHospitalCode(GlobalConfig.Hospital.Item1);
            }
            else
            {
                GlobalConfig.FsCatalogueList = new List<FsCatalogue>();
                GlobalConfig.DrugCatalogueList = new List<DrugCatalogue>();
            }

        }


        public static void SetHospital(this string hospital)
        {
            if (hospital == Hospital.SVH.ToString())
            {
                GlobalConfig.Hospital = new Tuple<int, string>((int)Hospital.SVH, Hospital.SVH.ToString());
            }
            else if (hospital == Hospital.SNH.ToString())
            {
                GlobalConfig.Hospital = new Tuple<int, string>((int)Hospital.SNH, Hospital.SNH.ToString());
            }
            else
            {
                GlobalConfig.Hospital = Tuple.Create(0, "");
            }
        }

        public static List<FsCatalogue> GetFsCatalogueDiffFromDB(this List<FsCatalogue> fromUpload, List<FsCatalogue> fromDB)
        {
            var data = fromUpload.Where(a => !fromDB.Any(x => x.HospitalCode == a.HospitalCode && x.FSCodeNIEMS == a.FSCodeNIEMS && x.FSCodeHos == a.FSCodeHos)).ToList();
            return data;
        }

        public static List<DrugCatalogue> GetDrugCatalogueDiffFromDB(this List<DrugCatalogue> fromUpload, List<DrugCatalogue> fromDB)
        {
            var data = fromUpload.Where(a => !fromDB.Any(x => x.HospDrugCode == a.HospDrugCode && x.TMTID == a.TMTID)).ToList();
            return data;
        }

        public static int GetHospitalCode(this string name)
        {
            var hosCode = 0;
            var snh = Hospital.SNH;
            var svh = Hospital.SVH;
            if (name == svh.ToString())
            {
                hosCode = (int)svh;
            }
            else if (name == snh.ToString())
            {
                hosCode = (int)snh;
            }
            return hosCode;
        }

        public static string TimeFormat(this string time)
        {
            var reg = @"^(?:[01][0-9]|2[0-3]):[0-5][0-9]:[0-5][0-9]$";
            var match = Regex.Match(time.Trim(), reg);
            var result = time;

            if (!match.Success)
            {
                result = $"{time.Substring(0, 2)}:{time.Substring(2, 2)}:{time.Substring(4, 2)}";
            }
            return result;
        }

        public static void AddCataloguesToDB(this DataTable dt, string catalogue)
        {
            if (catalogue == Catalogue.DrugCatalogue.ToString()) // DrugCatalogues
            {
                // Get Item DrugCatalogues from database
                var DrugCataloguesDb = GlobalConfig.Connection.GetAllDrugCatalogue();

                // Convert datatable to model
                var DrugCatalogues = dt.DTDrugCatalogueToModelList();

                // get data diff between upload and database
                var dataToDb = DrugCatalogues.GetDrugCatalogueDiffFromDB(DrugCataloguesDb);
                GlobalConfig.DrugCatalogueListUpLoad = dataToDb;

                if (dataToDb.Count > 0)
                {
                    // save models to database
                    GlobalConfig.Connection.AddDrugCatalogues(dataToDb);
                }

            }
            else if (catalogue == Catalogue.FSCatalogue.ToString()) // FsCatalogues
            {
                // Get Item FsCatalogues from database
                var FsCataloguesDb = GlobalConfig.Connection.GetAllFsCatalogue();

                // Convert datatable to model
                var FsCatalogues = dt.DTFsCatalogueToModelList();

                // get data diff between upload and database
                var dataToDb = FsCatalogues.GetFsCatalogueDiffFromDB(FsCataloguesDb);
                GlobalConfig.FsCatalogueListUpLoad = dataToDb;

                if (dataToDb.Count > 0)
                {
                    // save models to database
                    GlobalConfig.Connection.AddFsCatalogues(dataToDb);
                }
            }
        }
    }
}
