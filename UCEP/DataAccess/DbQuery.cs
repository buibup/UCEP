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
    }
}
