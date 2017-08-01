using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace UCEP.Common
{
    public class Constants
    {
        public static string Chache89 = ConfigurationManager.ConnectionStrings["Chache89"].ToString();
    }
}