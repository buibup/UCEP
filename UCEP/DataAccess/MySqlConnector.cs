using Dapper;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using UCEP.Models;
using System.Web.Mvc;

namespace UCEP.DataAccess
{
    public class MySqlConnector : IDataConnection
    {
        private string conString = GlobalConfig.CnnString("UCEPMySqlDB");
        public bool AddFsCatalogues(List<FsCatalogue> models)
        {
            using (IDbConnection db = new MySqlConnection(conString))
            {
                db.Execute("truncate table FsCatalogues");
                db.Execute(DbQuery.InsertToFsCatalogue(), models);
            }

            return true;
        }

        public void CreateFsCatalogue(FsCatalogue model)
        {
            using (IDbConnection db = new MySqlConnection(conString))
            {
                db.Execute(DbQuery.InsertToFsCatalogue(), model);
            }
        }

        public FsCatalogue DeleteFsCatalogue(int id)
        {
            var model = new FsCatalogue();

            using(IDbConnection db = new MySqlConnection(conString))
            {
                model = db.Query<FsCatalogue>(DbQuery.GetFsCatalogue(), new { Id = id }).SingleOrDefault();
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
                model = db.Query<FsCatalogue>(DbQuery.GetFsCatalogue(), new { Id = id }).SingleOrDefault();
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

        public FsCatalogue GetFsCatalogue(string FSCodeHos)
        {
            var query = "SELECT * FROM ucep.FsCatalogues where FSCodeHos = @FSCodeHos ;";

            using (IDbConnection db = new MySqlConnection(conString))
            {
                return db.Query<FsCatalogue>(query, new { FSCodeHos = FSCodeHos }).SingleOrDefault();
            }

        }

        public FsCatalogue GetFsCatalogue(int id)
        {
            var model = new FsCatalogue();

            using (IDbConnection db = new MySqlConnection(conString))
            {
                model = db.Query<FsCatalogue>(DbQuery.GetFsCatalogue(), new { Id = id }).SingleOrDefault();
            }

            return model;
        }
    }
}
