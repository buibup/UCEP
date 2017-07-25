using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UCEP
{
    public class Constants
    {
        public static string UCEPDB = ConfigurationManager.ConnectionStrings["UCEPDBConnectionString"].ToString();
        public static string Chache89 = ConfigurationManager.ConnectionStrings["Chache89"].ToString();
    }
}