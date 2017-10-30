using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using UCEP.Models;

namespace UCEP.DataAccess
{
  public class SqlConnector : IDataConnection
  {
    private UCEPDbContext db = new UCEPDbContext();
    public bool AddFsCatalogues(List<FsCatalogue> models)
    {
      try
      {
        // truncate table
        db.Database.ExecuteSqlCommand("TRUNCATE TABLE [FsCatalogues]");

        // save models to database
        db.FsCatalogues.AddRange(models);
        db.SaveChanges();
        return true;
      }
      catch (Exception)
      {
        return false;
      }
    }

    public FsCatalogue GetFsCatalogue(string FSCodeHos)
    {
      var item = db.FsCatalogues.Where(m => m.FSCodeHos == FSCodeHos).Select(m => new { m.HospitalCode, m.FSCodeNIEMS, m.FSCodeHos, m.Category, m.Meaning, m.Unit, m.Price, m.EffectiveDate, m.Status, m.ApprovalDate }).SingleOrDefault();

      var data = new FsCatalogue();

      if(item != null)
      {
        data.HospitalCode = item.HospitalCode;
        data.FSCodeNIEMS = item.FSCodeNIEMS;
        data.FSCodeHos = item.FSCodeHos;
        data.Category = item.Category;
        data.Meaning = item.Meaning;
        data.Unit = item.Unit;
        data.Price = item.Price;
        data.EffectiveDate = item.EffectiveDate;
        data.Status = item.Status;
        data.ApprovalDate = item.ApprovalDate;
      }

      return data;
    }
  }
}
