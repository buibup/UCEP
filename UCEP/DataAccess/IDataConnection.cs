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
    bool AddDrugCatalogues(List<DrugCatalogue> models);
    FsCatalogue GetFsCatalogue(string FSCodeHos);
    FsCatalogue GetFsCatalogueFromGlobalConfig(string FSCodeHos);
    FsCatalogue GetFsCatalogue(string FSCodeHos, string HospitalCode);
    DrugCatalogue GetDrugCatalogue(string DrugCodeHos, string HospitalCode);
    List<FsCatalogue> GetAllFsCatalogue();
    List<DrugCatalogue> GetAllDrugCatalogue();
    List<FsCatalogue> GetAllFsCatalogueByHospitalCode(int Code);
    FsCatalogue GetFsCatalogue(int id);
    void CreateFsCatalogue(FsCatalogue model);
    FsCatalogue EditFsCatalogue(int id);
    void EditFsCatalogue(FsCatalogue model);
    FsCatalogue DeleteFsCatalogue(int id);
    void DeleteFsCatalogue(int id, FormCollection collection);
  }
}
