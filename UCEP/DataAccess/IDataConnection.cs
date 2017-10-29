using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UCEP.Models;

namespace UCEP.DataAccess
{
    public interface IDataConnection
    {
        bool AddFsCatalogues(List<FsCatalogue> models);
        FsCatalogue GetFsCatalogue(string FSCodeHos);
    }
}
