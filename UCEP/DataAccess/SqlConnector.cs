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
            throw new NotImplementedException();
        }
    }
}