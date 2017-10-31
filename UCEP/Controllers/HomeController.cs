using CRMWebApi.DA;
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

    //private IDataConnection dbMySql = new MySqlConnector();
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

          if (upload.FileName.EndsWith(".csv"))
          {
            Stream stream = upload.InputStream;
           
            DataTable csvTable = new DataTable();
            using (CsvReader csvReader = new CsvReader(new StreamReader(stream, Encoding.UTF8, false),  true))
            {
              csvTable.Load(csvReader);
            }

            // Convert datatable to model
            var FsCatalogues = Helper.DTFsCatalogueToModel(csvTable);

            // save models to database
            GlobalConfig.Connection.AddFsCatalogues(FsCatalogues);

            return RedirectToAction("Index", "UCEP");
          }
          else
          {
            ModelState.AddModelError("File", "This file format is not supported, Please select csv file for upload.s");
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

      var models = Helper.DTToFsTemplateList(dt);

      var sb = new StringBuilder();
      sb.AppendLine($"use_date,F/S code หรือ TMT Code,Hospital code,category,mean,unit,price_total");
      foreach (var data in models)
      {
        sb.AppendLine($"{data.UseDate},{data.FSCodeOrTMTCode},{data.HospitalCode},{data.Category},{data.Mean},{data.Unit},{data.PriceTotal}");
      }

      return File(new UTF8Encoding().GetBytes(sb.ToString()), "text/csv", "export.csv");
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
