using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using UCEP.Models;
using static UCEP.Enums;

namespace UCEP.Controllers
{
    public class UCEPController : Controller
    {
        public UCEPController()
        {
            GlobalConfig.InitializeConnections(DatabaseType.MySql);
        }
        // GET: UCEP
        public ActionResult Index()
        {
            var models = GlobalConfig.Connection.GetAllFsCatalogue();
            return View(models);
        }

        // GET: UCEP/Details/5
        public ActionResult Details(int id)
        {
            var model = GlobalConfig.Connection.GetFsCatalogue(id);

            return View(model);
        }

        // GET: UCEP/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UCEP/Create
        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UCEP/Edit/5
        public ActionResult Edit(int id)
        {
            var model = GlobalConfig.Connection.EditFsCatalogue(id);

            return View(model);
        }

        // POST: UCEP/Edit/5
        [HttpPost]
        public ActionResult Edit(FsCatalogue fsCatalogue)
        {
            try
            {
                // TODO: Add update logic here
                GlobalConfig.Connection.EditFsCatalogue(fsCatalogue);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: UCEP/Delete/5
        public ActionResult Delete(int id)
        {
            var model = GlobalConfig.Connection.DeleteFsCatalogue(id);

            return View(model);
        }

        // POST: UCEP/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here
                GlobalConfig.Connection.DeleteFsCatalogue(id, collection);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
