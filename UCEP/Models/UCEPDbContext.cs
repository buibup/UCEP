using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace UCEP.Models
{
    public class UCEPDbContext:DbContext
    {
        public UCEPDbContext():base("UCEPDBConnectionString")
        {

        }
        public  DbSet<FsTemplate> FsTemplate { get; set; }
        public DbSet<FsCatalogue> FsCatalogue { get; set; }
    }
}