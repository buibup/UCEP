using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UCEP.Models;

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

    public FsCatalogue GetFsCatalogue(string FSCodeHos)
    {
      var query = "SELECT * FROM ucep.FsCatalogues where FSCodeHos = @FSCodeHos ;";

      using (IDbConnection db = new MySqlConnection(conString))
      {
        return db.Query<FsCatalogue>(query, new { FSCodeHos = FSCodeHos }).SingleOrDefault();
      }
        
    }
  }
}
