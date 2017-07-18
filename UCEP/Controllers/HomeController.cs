using LINQtoCSV;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCEP.Models;

namespace UCEP.Controllers
{
    public class HomeController : Controller
    {
        private UCEPDbContext db = new UCEPDbContext();
        public ActionResult Index()
        {
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
            db.FsCatalogue.AddRange(list);
            db.SaveChanges();
            return Redirect("GetAllEmployeeData");
        }
    }
}