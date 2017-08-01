using CRMWebApi.DA;
using LINQtoCSV;
using LumenWorks.Framework.IO.Csv;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCEP.Common;
using UCEP.Models;
using UCEP.ViewModels;

namespace UCEP.Controllers
{
    public class HomeController : Controller
    {
        private UCEPDbContext db = new UCEPDbContext();
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

                    if (upload.FileName.EndsWith(".csv"))
                    {
                        Stream stream = upload.InputStream;
                        DataTable csvTable = new DataTable();
                        using (CsvReader csvReader =
                            new CsvReader(new StreamReader(stream), true))
                        {
                            csvTable.Load(csvReader);
                        }

                        // Convert datatable to model
                        var FsCatalogues = Helper.DTFsCatalogueToModel(csvTable);

                        // truncate table
                        db.Database.ExecuteSqlCommand("TRUNCATE TABLE [FsCatalogues]");

                        // save models to database
                        db.FsCatalogues.AddRange(FsCatalogues);
                        db.SaveChanges();

                        
                        return View(csvTable);
                    }
                    else
                    {
                        ModelState.AddModelError("File", "This file format is not supported");
                        return View();
                    }
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
            return View();
        }

        [HttpPost]
        public ActionResult ExportCSV(PatientModel pt)
        {
            string hn = pt.HN;
            string dte = pt.Date;
            string tme = pt.Time;

            var dtmFrom = Helper.StringToDateTime(dte, tme);
            var dtmTo = dtmFrom.AddHours(72);

            var query = QueryString.GetPatientDetialByHn(hn, dtmFrom, dtmTo);

            var dt = InterSystemsDA.DTBindDataCommandWihDictionary(query.Item1, Constants.Chache89, query.Item2);

            var data = Helper.DTToFsTemplateList(dt);

            return View();
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
            db.FsCatalogues.AddRange(list);
            db.SaveChanges();
            return Redirect("GetAllEmployeeData");
        }
    }
};