using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;
using UCEP.Models;

namespace UCEP.DataAccess
{
  public interface IDataConnection
  {
    bool AddFsCatalogues(List<FsCatalogue> models);
    FsCatalogue GetFsCatalogue(string FSCodeHos);
    List<FsCatalogue> GetAllFsCatalogue();
    FsCatalogue GetFsCatalogue(int id);
    void CreateFsCatalogue(FsCatalogue model);
    FsCatalogue EditFsCatalogue(int id);
    void EditFsCatalogue(FsCatalogue model);
    FsCatalogue DeleteFsCatalogue(int id);
    void DeleteFsCatalogue(int id, FormCollection collection);
  }
}
