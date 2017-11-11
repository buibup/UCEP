using System;
using System.Collections.Generic;
using System.Web.Mvc;
using UCEP.Common;
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
        //[OutputCache(Duration = 60)]
        public ActionResult Index(string searchString, string catalogue, string hospital)
        {
            dynamic models = null;

            // clear list
            GlobalConfig.Clear();

            if (!string.IsNullOrEmpty(hospital))
            {
                hospital.SetHospital();
                ViewData["hospital"] = GlobalConfig.Hospital.Item2;
            }

            if (!string.IsNullOrEmpty(catalogue))
            {
                catalogue.SetCatalogue();
                ViewData["catalogue"] = catalogue;

                if (catalogue == Catalogue.FSCatalogue.ToString())
                {
                    ViewBag.Filter = "FSCodeNIEMS Or FSCodeHos";
                    GlobalConfig.FsCatalogueList = GlobalConfig.Connection.GetAllFsCatalogueByHospitalCode(GlobalConfig.Hospital.Item1);
                    models = GlobalConfig.FsCatalogueList;

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        var model = GlobalConfig.Connection.GetFsCatalogueFromGlobalConfig(searchString);
                        GlobalConfig.FsCatalogueList.Clear();
                        GlobalConfig.FsCatalogueList.Add(model);
                    }

                    ViewData["FsCatalogueList"] = GlobalConfig.FsCatalogueList;
                }
                else if (catalogue == Catalogue.DrugCatalogue.ToString())
                {
                    ViewBag.Filter = "HospDrugCode Or TMTID";
                    GlobalConfig.DrugCatalogueList = GlobalConfig.Connection.GetAllDrugCatalogue();
                    models = GlobalConfig.DrugCatalogueList;

                    if (!string.IsNullOrEmpty(searchString))
                    {
                        var model = GlobalConfig.Connection.GetDrugCatalogueFromGlobalConfig(searchString);
                        GlobalConfig.DrugCatalogueList.Clear();
                        GlobalConfig.DrugCatalogueList.Add(model);
                    }

                    ViewData["DrugCatalogueList"] = GlobalConfig.DrugCatalogueList;
                }
            }


            return View();
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
        public ActionResult Create(FsCatalogue fsCatalogue)
        {
            try
            {
                // TODO: Add insert logic here
                GlobalConfig.Connection.CreateFsCatalogue(fsCatalogue);

                return RedirectToAction("Index");
            }
            catch (Exception)
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
