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
    public ActionResult Upload(HttpPostedFileBase upload, string catalogue)
    {
      if (ModelState.IsValid)
      {
        if (upload != null && upload.ContentLength > 0)
        {

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

            csvTable.AddCataloguesToDB(catalogue);

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

          result.Tables[0].AddCataloguesToDB(catalogue);

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

      var queryFSDrug = QueryString.GetPatientOrderFSDrug(hn, dtmFrom, dtmTo);
      var FsDrugTemplateList = InterSystemsDA.BindDataFsDrugTemplateList(queryFSDrug.Item1, Constants.Chache89, queryFSDrug.Item2, hos.GetHospitalCode().ToString());

      if (matchAll)
      {
        FsDrugTemplateList = FsDrugTemplateList.Where(d => !string.IsNullOrEmpty(d.FSCodeOrTMTCode)).ToList();
      }

      // order by datetime
      var result = FsDrugTemplateList.OrderBy(a => string.IsNullOrEmpty(a.UseDate) ? (DateTime?)null : DateTime.ParseExact(a.UseDate, "yyyy-MM-dd HH:mm:ss", null)).ToList();

      GlobalConfig.FsTemplateList = result;



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
