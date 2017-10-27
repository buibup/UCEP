using System.Data.Entity;

namespace UCEP.Models
{
  public class UCEPDbContext:DbContext
    {
        public UCEPDbContext():base("UCEPDBConnectionString")
        {

        }
        public DbSet<FsCatalogue> FsCatalogues { get; set; }
    }
}
