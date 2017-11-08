using CRMWebApi.DA;
using ExcelDataReader;
using LINQtoCSV;
using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using UCEP.Common;
using UCEP.DataAccess;
using UCEP.Models;
using UCEP.ViewModels;
using static UCEP.Enums;

namespace UCEP.Controllers
{
    public class HomeController : Controller
    {
        private UCEPDbContext db = new UCEPDbContext();

        public HomeController()
        {
            //set defualt database
            GlobalConfig.InitializeConnections(DatabaseType.MySql);
        }
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Upload()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Upload(HttpPostedFileBase upload)
        {
            if (ModelState.IsValid)
            {
                if (upload != null && upload.ContentLength > 0)
                {
                    var FsCatalogues = new List<FsCatalogue>();
                    var dataToDb = new List<FsCatalogue>();

                    // Get Item from database
                    var dataDb = GlobalConfig.Connection.GetAllFsCatalogue();

                    //ExcelDataReader works on binary excel file
                    Stream stream = upload.InputStream;
                    //We need to written the Interface.
                    IExcelDataReader reader = null;
                    if (upload.FileName.EndsWith(".csv"))
                    {

                        DataTable csvTable = new DataTable();
                        using (CsvReader csvReader = new CsvReader(new StreamReader(stream, Encoding.UTF8, false), true))
                        {
                            csvTable.Load(csvReader);
                        }

                        // Convert datatable to model
                        FsCatalogues = Helper.DTFsCatalogueToModel(csvTable);

                        // get data diff between upload and database
                        dataToDb = FsCatalogues.GetFsCatalogueDiffFromDB(dataDb);

                        if (dataToDb.Count > 0)
                        {
                            // save models to database
                            GlobalConfig.Connection.AddFsCatalogues(dataToDb);
                        }

                        return RedirectToAction("Index", "UCEP");
                    }
                    else if (upload.FileName.EndsWith(".xls"))
                    {
                        //reads the excel file with .xls extension
                        reader = ExcelReaderFactory.CreateBinaryReader(stream);

                    }
                    else if (upload.FileName.EndsWith(".xlsx"))
                    {
                        //reads excel file with .xlsx extension
                        reader = ExcelReaderFactory.CreateOpenXmlReader(stream);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }

                    //Adding reader data to DataSet()
                    var result = reader.AsDataSet(new ExcelDataSetConfiguration()
                    {
                        ConfigureDataTable = (_) => new ExcelDataTableConfiguration()
                        {
                            //treats the first row of excel file as Column Names
                            UseHeaderRow = true
                        }
                    });

                    // Convert datatable to list model
                    FsCatalogues = Helper.DTFsCatalogueToModel(result.Tables[0]);

                    // get data diff between upload and database
                    dataToDb = FsCatalogues.GetFsCatalogueDiffFromDB(dataDb);

                    if (dataToDb.Count > 0)
                    {
                        // save new item to database
                        GlobalConfig.Connection.AddFsCatalogues(dataToDb);
                    }

                    reader.Close();

                    return RedirectToAction("Index", "UCEP");
                }
                else
                {
                    ModelState.AddModelError("File", "Please Upload Your file");
                }
            }
            return View();
        }

        public ActionResult ExportCSV()
        {
            ViewData["hospital"] = GlobalConfig.Hospital;
            var pt = new PatientModel();
            //pt.PatientHospital = ((Tuple<int, string>)ViewData["hospital"]).Item2;
            return View();
        }

        [HttpPost]
        public ActionResult ExportCSV(PatientModel pt)
        {
            var hn = pt.HN;
            var dte = pt.Date;
            var tme = pt.Time.TimeFormat();
            var hos = pt.PatientHospital.ToString();
            var matchAll = pt.MatchAll;

            GlobalConfig.HN = hn.Trim();

            var dtmFrom = Helper.StringToDateTime(dte, tme);
            var dtmTo = dtmFrom.AddHours(72);

            //var queryFS = QueryString.GetPatientOrderFS(hn, dtmFrom, dtmTo);
            //var dtFS = InterSystemsDA.DTBindDataCommandWihDictionary(queryFS.Item1, Constants.Chache89, queryFS.Item2);
            //var fsTemplateListFS = Helper.DTToFsTemplateList(dtFS, hos.GetHospitalCode().ToString());

            //var queryDrug = QueryString.GetPatientOrderDrug(hn, dtmFrom, dtmTo);
            //var dtDrug = InterSystemsDA.DTBindDataCommandWihDictionary(queryDrug.Item1, Constants.Chache89, queryDrug.Item2);
            //var fsTemplateListDrug = Helper.DTToFsTemplateList(dtDrug, hos.GetHospitalCode().ToString());

            var queryFSDrug = QueryString.GetPatientOrderFSDrug(hn, dtmFrom, dtmTo);
            var dtFSDrug = InterSystemsDA.DTBindDataCommandWihDictionary(queryFSDrug.Item1, Constants.Chache89, queryFSDrug.Item2);
            var fsTemplateListFSDrug = Helper.DTToFsTemplateList(dtFSDrug, hos.GetHospitalCode().ToString());

            if (matchAll)
            {
                fsTemplateListFSDrug = fsTemplateListFSDrug.Where(d => !string.IsNullOrEmpty(d.FSCodeOrTMTCode)).ToList();
            }

            GlobalConfig.FsTemplateList = fsTemplateListFSDrug;



            return RedirectToAction("Export", "Home");
        }

        public ActionResult Export()
        {
            if (GlobalConfig.FsTemplateList == null) return View();

            return View(GlobalConfig.FsTemplateList);
        }

        public ActionResult DownloadCSV()
        {
            var sb = new StringBuilder();
            sb.AppendLine($"use_date,F/S code หรือ TMT Code,Hospital code,category,mean,unit,price_total");
            foreach (var data in GlobalConfig.FsTemplateList)
            {
                sb.AppendLine($"{data.UseDate},{data.FSCodeOrTMTCode},{data.HospitalCode},{data.Category},{data.Mean.Replace(",", " ")},{data.Unit},{data.PriceTotal}");
            }

            return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", $"{GlobalConfig.HN}.csv");
        }

        [HttpPost]
        public ActionResult UploadCsv(HttpPostedFileBase attachmentcsv)
        {
            CsvFileDescription csvFileDescription = new CsvFileDescription
            {
                SeparatorChar = ',',
                FirstLineHasColumnNames = true
            };
            CsvContext csvContext = new CsvContext();
            StreamReader streamReader = new StreamReader(attachmentcsv.InputStream);
            IEnumerable<FsCatalogue> list = csvContext.Read<FsCatalogue>(streamReader, csvFileDescription);
            //db.FsCatalogues.AddRange(list);
            //db.SaveChanges();
            return Redirect("");
        }
    }
};
