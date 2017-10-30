using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UCEP.Models;
using System.Web.Mvc;

namespace UCEP.DataAccess
{
  public class MySqlConnector : IDataConnection
  {
    private string conString = GlobalConfig.CnnString("UCEPMySqlDB");
    public bool AddFsCatalogues(List<FsCatalogue> models)
    {
      using (IDbConnection db = new MySqlConnection(conString))
      {
        db.Execute("truncate table FsCatalogues");
        db.Execute("INSERT INTO FsCatalogues (HospitalCode, FSCodeNIEMS, FSCodeHos, Category, Meaning, Unit, Price, EffectiveDate, Status, ApprovalDate) VALUES(@HospitalCode, @FSCodeNIEMS, @FSCodeHos, @Category, @Meaning, @Unit, @Price, @EffectiveDate, @Status, @ApprovalDate)", models);
      }

      return true;
    }

    public void CreateFsCatalogue(FsCatalogue model)
    {
      throw new NotImplementedException();
    }

    public FsCatalogue DeleteFsCatalogue(int id)
    {
      throw new NotImplementedException();
    }

    public void DeleteFsCatalogue(int id, FormCollection collection)
    {
      throw new NotImplementedException();
    }

    public FsCatalogue EditFsCatalogue(int id)
    {
      throw new NotImplementedException();
    }

    public void EditFsCatalogue(FsCatalogue model)
    {
      throw new NotImplementedException();
    }

    public List<FsCatalogue> GetAllFsCatalogue()
    {
      var models = new List<FsCatalogue>();
      using(IDbConnection db = new MySqlConnection(conString))
      {
        var sqlQuery = "SELECT * FROM ucep.FsCatalogues;";
        models = db.Query<FsCatalogue>(sqlQuery).ToList();
      }

      return models;
    }

    public FsCatalogue GetFsCatalogue(string FSCodeHos)
    {
      var query = "SELECT * FROM ucep.FsCatalogues where FSCodeHos = @FSCodeHos ;";

      using (IDbConnection db = new MySqlConnection(conString))
      {
        return db.Query<FsCatalogue>(query, new { FSCodeHos = FSCodeHos }).SingleOrDefault();
      }
        
    }

    public FsCatalogue GetFsCatalogue(int id)
    {
      throw new NotImplementedException();
    }
  }
}
