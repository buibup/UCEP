using System;
using System.Collections.Generic;
using System.Configuration;
using UCEP.DataAccess;
using UCEP.Models;
using static UCEP.Enums;

namespace UCEP
{
  public class GlobalConfig
  {
    public static string HN { get; set; }
    public static Tuple<int, string> Hospital { get; set; } = new Tuple<int, string>(0, "");
    public static string Catalogue { get; set; }
    public static List<FsDrugTemplate> FsTemplateList { get; set; } = new List<FsDrugTemplate>();
    public static List<FsCatalogue> FsCatalogueList { get; set; } = new List<FsCatalogue>();
    public static List<DrugCatalogue> DrugCatalogueList { get; set; } = new List<DrugCatalogue>();
    public static List<FsCatalogue> FsCatalogueListUpLoad { get; set; } = new List<FsCatalogue>();
    public static List<DrugCatalogue> DrugCatalogueListUpLoad { get; set; } = new List<DrugCatalogue>();
    public static IDataConnection Connection { get; private set; }

    public static void InitializeConnections(DatabaseType db)
    {
      if (db == DatabaseType.Sql)
      {
        // TODO - Set up the SQL Connector properly
        SqlConnector sql = new SqlConnector();
        Connection = sql;
      }
      else if (db == DatabaseType.MySql)
      {
        // TODO - Create the MySql Connection
        MySqlConnector mySql = new MySqlConnector();
        Connection = mySql;
      }
    }

    public static string CnnString(string name)
    {
      return ConfigurationManager.ConnectionStrings[name].ConnectionString;
    }

    public static void Clear()
    {
      FsTemplateList = new List<FsDrugTemplate>();
      FsCatalogueList = new List<FsCatalogue>();
      DrugCatalogueList = new List<DrugCatalogue>();
      Catalogue = "";
    }
  }
}
