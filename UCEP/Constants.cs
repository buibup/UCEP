using System.Configuration;

namespace UCEP
{
    public class Constants
    {
        public static string UCEPDB = ConfigurationManager.ConnectionStrings["UCEPDBConnectionString"].ToString();
        public static string Chache89 = ConfigurationManager.ConnectionStrings["Chache89"].ToString();
    }
}