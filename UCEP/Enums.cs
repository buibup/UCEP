using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCEP
{
  public class Enums
  {
    public enum DatabaseType
    {
      Sql,
      MySql
    }

    public enum Hospital
    {
      SVH = 11811,
      SNH = 11704
    }

    public enum Catalogue
    {
      DrugCatalogue,
      FSCatalogue
    }
  }
}
