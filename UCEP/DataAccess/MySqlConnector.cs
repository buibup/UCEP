using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using UCEP.Models;
using System.Web.Mvc;

namespace UCEP.DataAccess
{
    public class MySqlConnector : IDataConnection
    {
        private string conString = GlobalConfig.CnnString("UCEPMySqlDB");

        public bool AddDrugCatalogues(List<DrugCatalogue> models)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(conString))
                {
                    //db.Execute("truncate table FsCatalogues");
                    db.Execute(DbQuery.InsertToDrugCatalogue(), models);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public bool AddFsCatalogues(List<FsCatalogue> models)
        {
            try
            {
                using (IDbConnection db = new MySqlConnection(conString))
                {
                    //db.Execute("truncate table FsCatalogues");
                    db.Execute(DbQuery.InsertToFsCatalogue(), models);
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public void CreateFsCatalogue(FsCatalogue model)
        {
            using (IDbConnection db = new MySqlConnection(conString))
            {
                int rowsAffected = db.Execute(DbQuery.InsertToFsCatalogue(), model);
            }
        }

        public FsCatalogue DeleteFsCatalogue(int id)
        {
            var model = new FsCatalogue();

            using (IDbConnection db = new MySqlConnection(conString))
            {
                model = db.Query<FsCatalogue>(DbQuery.GetFsCatalogue(), new { Id = id }).FirstOrDefault();
            }

            return model;
        }

        public void DeleteFsCatalogue(int id, FormCollection collection)
        {
            using (IDbConnection db = new MySqlConnection(conString))
            {
                int rowsAffected = db.Execute(DbQuery.DeleteFromFsCatalogue(), new { Id = id });
            }
        }

        public FsCatalogue EditFsCatalogue(int id)
        {
            var model = new FsCatalogue();

            using (IDbConnection db = new MySqlConnection(conString))
            {
                model = db.Query<FsCatalogue>(DbQuery.GetFsCatalogue(), new { Id = id }).FirstOrDefault();
            }

            return model;
        }

        public void EditFsCatalogue(FsCatalogue model)
        {
            using (IDbConnection db = new MySqlConnection(conString))
            {
                int rowsAffected = db.Execute(DbQuery.EditFsCatalogue(), model);
            }
        }

        public List<DrugCatalogue> GetAllDrugCatalogue()
        {
            var models = new List<DrugCatalogue>();
            using (IDbConnection db = new MySqlConnection(conString))
            {
                models = db.Query<DrugCatalogue>(DbQuery.GetAllDrugCatalogue()).ToList();
            }

            return models;
        }

        public List<FsCatalogue> GetAllFsCatalogue()
        {
            var models = new List<FsCatalogue>();
            using (IDbConnection db = new MySqlConnection(conString))
            {
                var sqlQuery = "SELECT * FROM ucep.FsCatalogues;";
                models = db.Query<FsCatalogue>(sqlQuery).ToList();
            }

            return models;
        }

        public List<FsCatalogue> GetAllFsCatalogueByHospitalCode(int code)
        {
            var models = new List<FsCatalogue>();
            using (IDbConnection db = new MySqlConnection(conString))
            {
                models = db.Query<FsCatalogue>(DbQuery.GetAllFsCatalogueByHospitalCode(), new { HospitalCode = code }).ToList();
            }

            return models;
        }

        public DrugCatalogue GetDrugCatalogue(string HospDrugCode, string HospitalCode)
        {
            var query = "SELECT * FROM `ucep`.`DrugCatalogues` WHERE HospDrugCode = @HospDrugCode ; ;";

            using (IDbConnection db = new MySqlConnection(conString))
            {
                return db.Query<DrugCatalogue>(query, new { HospDrugCode = HospDrugCode }).FirstOrDefault();
            }
        }

        public List<DrugCatalogue> GetDrugCatalogueFromGlobalConfig(string filter)
        {
            var data = new List<DrugCatalogue>();
            if (GlobalConfig.DrugCatalogueList.Count > 0)
            {
                data = GlobalConfig.DrugCatalogueList.Where(d => d.HospDrugCode == filter || d.TMTID == filter).ToList();
            }
            else
            {
                data = null;
            }

            return data;
        }

        public FsCatalogue GetFsCatalogue(string FSCodeHos)
        {
            var query = "SELECT * FROM ucep.FsCatalogues where FSCodeHos = @FSCodeHos ;";

            using (IDbConnection db = new MySqlConnection(conString))
            {
                return db.Query<FsCatalogue>(query, new { FSCodeHos = FSCodeHos }).FirstOrDefault();
            }
        }

        public FsCatalogue GetFsCatalogue(int id)
        {
            var model = new FsCatalogue();

            using (IDbConnection db = new MySqlConnection(conString))
            {
                model = db.Query<FsCatalogue>(DbQuery.GetFsCatalogue(), new { Id = id }).FirstOrDefault();
            }

            return model;
        }

        public FsCatalogue GetFsCatalogue(string FSCodeHos, string HospitalCode)
        {
            if (GlobalConfig.FsCatalogueList.Count > 0)
            {
                var data = GlobalConfig.FsCatalogueList.Find(d => d.FSCodeHos == FSCodeHos && d.HospitalCode == HospitalCode);
                return data;
            }
            else
            {
                var query = "SELECT * FROM ucep.FsCatalogues where FSCodeHos = @FSCodeHos and HospitalCode = @HospitalCode ;";

                using (IDbConnection db = new MySqlConnection(conString))
                {
                    return db.Query<FsCatalogue>(query, new { FSCodeHos = FSCodeHos, HospitalCode = HospitalCode }).FirstOrDefault();
                }
            }
        }

        public List<FsCatalogue> GetFsCatalogueFromGlobalConfig(string filter)
        {
            var data = new List<FsCatalogue>();
            if (GlobalConfig.FsCatalogueList.Count > 0)
            {
                data = GlobalConfig.FsCatalogueList.Where(d => d.FSCodeHos == filter || d.FSCodeNIEMS == filter).ToList();
            }
            else
            {
                data = null;
            }

            return data;
        }
    }
}
