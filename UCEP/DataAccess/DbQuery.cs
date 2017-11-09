using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace UCEP.DataAccess
{
  public class DbQuery
  {
    public static string InsertToFsCatalogue()
    {
      const string sqlQuery = "INSERT INTO FsCatalogues (HospitalCode, FSCodeNIEMS, FSCodeHos, Category, Meaning, Unit, Price, EffectiveDate, Status, ApprovalDate) VALUES(@HospitalCode, @FSCodeNIEMS, @FSCodeHos, @Category, @Meaning, @Unit, @Price, @EffectiveDate, @Status, @ApprovalDate)";

      return sqlQuery;
    }

    public static string InsertToDrugCatalogue()
    {
      const string sqlQuery = @"

        INSERT INTO `ucep`.`DrugCatalogues`
        (`﻿HOSPDRUGCODE`,`PRODUCTCAT`,`TMTID`,`SPECPREP`,
        `GENERICNAME`,`TRADENAME`,`DFSCODE`,`DOSAGEFORM`,
        `STRENGTH`,`CONTENT`,`UNITPRICE`,`DISTRIBUTOR`,
        `MANUFACTURER`,`ISED`,`NDC24`,`PACKSIZE`,
        `PACKPRICE`,`UPDATEFLAG`,`DATECHANGE`,`DATEUPDATE`,
        `DATEEFFECTIVE`,`ISED_APPROVED`,`NDC24_APPROVED`,
        `DATE_APPROVED`,`ISED_STATUS`)
        VALUES
        (@HOSPDRUGCODE,@PRODUCTCAT,@TMTID,@SPECPREP,@GENERICNAME,
        @TRADENAME,@DFSCODE,@DOSAGEFORM,@STRENGTH,@CONTENT,@UNITPRICE,
        @DISTRIBUTOR,@MANUFACTURER,@ISED,@NDC24,@PACKSIZE,@PACKPRICE,
        @UPDATEFLAG,@DATECHANGE,@DATEUPDATE,@DATEEFFECTIVE,@ISED_APPROVED,
        @NDC24_APPROVED,@DATE_APPROVED,@ISED_STATUS);

      ";

      return sqlQuery;
    }

    public static string DeleteFromFsCatalogue()
    {
      const string sqlQuery = "Delete From FsCatalogues WHERE Id = @Id";

      return sqlQuery;
    }

    public static string GetFsCatalogue()
    {
      const string sqlQuery = "Select * From FsCatalogues WHERE Id = @Id";

      return sqlQuery;
    }

    public static string EditFsCatalogue()
    {
      const string sqlQuery = @"Update FsCatalogues Set HospitalCode = @HospitalCode, FSCodeNIEMS = @FSCodeNIEMS, FSCodeHos = @FSCodeHos, Category = @Category, Meaning = @Meaning
                                    , Unit = @Unit, Price = @Price, EffectiveDate = @EffectiveDate, Status = @Status, ApprovalDate = @ApprovalDate Where Id = @Id";

      return sqlQuery;
    }

    public static string GetAllFsCatalogueByHospitalCode()
    {
      const string sqlQuery = "SELECT * FROM ucep.FsCatalogues where HospitalCode = @HospitalCode";

      return sqlQuery;
    }

    public static string GetAllDrugCatalogue()
    {
      const string sqlQuery = "SELECT * FROM ucep.DrugCatalogues;";

      return sqlQuery;
    }
  }
}
