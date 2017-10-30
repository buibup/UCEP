using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace UCEP.Controllers
{
  public class UCEPController : Controller
  {
    // GET: UCEP
    public ActionResult Index()
    {
      var models = GlobalConfig.Connection.GetAllFsCatalogue();
      return View(models);
    }

    // GET: UCEP/Details/5
    public ActionResult Details(int id)
    {
      return View();
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
      return View();
    }

    // POST: UCEP/Edit/5
    [HttpPost]
    public ActionResult Edit(int id, FormCollection collection)
    {
      try
      {
        // TODO: Add update logic here

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
      return View();
    }

    // POST: UCEP/Delete/5
    [HttpPost]
    public ActionResult Delete(int id, FormCollection collection)
    {
      try
      {
        // TODO: Add delete logic here

        return RedirectToAction("Index");
      }
      catch
      {
        return View();
      }
    }
  }
}
