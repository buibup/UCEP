using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using UCEP.DataAccess;
using UCEP.Models;
using static UCEP.Enums;

namespace UCEP
{
  public class GlobalConfig
  {
    public static Tuple<int, string> Hospital { get; set; } = new Tuple<int, string>(0, "");
    public static List<FsTemplate> FsTemplateList { get; set; } = new List<FsTemplate>();
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
  }
}
