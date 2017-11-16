using InterSystems.Data.CacheClient;
using PatientEmpathy.Common;
using System;
using System.Collections.Generic;
using System.Data;
using UCEP;
using UCEP.Models;
using UCEP.ViewModels;

namespace CRMWebApi.DA
{
  public class InterSystemsDA
  {
    public static DataTable DTBindDataCommandWihDictionary(string cmdString, string conString, Dictionary<string, string> dics)
    {
      DataTable dt = new DataTable();
      using (var con = new CacheConnection(conString))
      {
        con.Open();

        using (var cmd = new CacheCommand(cmdString, con))
        {
          foreach (KeyValuePair<string, string> pair in dics)
          {
            cmd.AddInputParameters(new { key = pair.Value });
          }
          using (var reader = cmd.ExecuteReader())
          {
            dt.Load(reader);
          }
        }
        con.Close();
      }

      return dt;
    }

    public static List<FsDrugTemplate> BindDataFsDrugTemplateList(string cmdString, string conString, Dictionary<string, string> dics, string hosCode)
    {
      var data = new List<FsDrugTemplate>();
      using (var con = new CacheConnection(conString))
      {
        con.Open();

        using (var cmd = new CacheCommand(cmdString, con))
        {
          foreach (KeyValuePair<string, string> pair in dics)
          {
            cmd.AddInputParameters(new { key = pair.Value });
          }
          using (var reader = cmd.ExecuteReader())
          {
            while (reader.Read())
            {
              var fsCat = new FsCatalogue();
              var drugCat = new DrugCatalogue();

              string orderItemCode = reader["HospitalCode"].ToString();
              decimal price = 0;

              if (orderItemCode == "02C004")
              {
                var test = reader["ARCBG_Code"].ToString().Trim();
              }

              var d = Convert.ToDateTime(reader["OEORI_SttDat"].ToString());
              var t = Convert.ToDateTime(reader["OEORI_SttTim"].ToString());

              var useDate = $"{d.Year}-{d.ToString("MM-dd")}";
              var useTime = $"{t.ToString("HH:mm:ss")}";

              string CodeNIEMS = string.Empty;
              string Category = string.Empty;
              string Unit = string.Empty;

              if (reader["ARCBG_Code"].ToString().Trim() == "00100000")
              {
                // Drug
                drugCat = GlobalConfig.Connection.GetDrugCatalogue(orderItemCode, hosCode);
                if (drugCat != null)
                {
                  CodeNIEMS = drugCat.TMTID;
                  Category = "3";
                }

                FsDrugTemplate fsTemplate = new FsDrugTemplate()
                {

                  UseDate = $"{useDate} {useTime}",
                  FSCodeOrTMTCode = CodeNIEMS,
                  HospitalCode = orderItemCode,
                  Category = Category,
                  Mean = reader["Mean"].ToString(),
                  Unit = reader["Unit"].ToString(),
                  PriceTotal = decimal.TryParse(reader["PriceTotal"].ToString(), out price) ? price : 0
                };
                data.Add(fsTemplate);
              }
              else
              {
                // FS
                fsCat = GlobalConfig.Connection.GetFsCatalogue(orderItemCode, hosCode);
                if (fsCat != null)
                {
                  CodeNIEMS = fsCat.FSCodeNIEMS;
                  Category = fsCat.Category;
                }

                FsDrugTemplate fsTemplate = new FsDrugTemplate()
                {

                  UseDate = $"{useDate} {useTime}",
                  FSCodeOrTMTCode = CodeNIEMS,
                  HospitalCode = orderItemCode,
                  Category = Category,
                  Mean = reader["Mean"].ToString(),
                  Unit = reader["Unit"].ToString(),
                  PriceTotal = decimal.TryParse(reader["PriceTotal"].ToString(), out price) ? price : 0
                };
                data.Add(fsTemplate);
              }
            }
          }
        }
        con.Close();
      }

      return data;
    }


    public static DataTable DTBindDataCommand(string cmdString, string conString)
    {
      DataTable dt = new DataTable();

      using (var con = new CacheConnection(conString))
      {
        using (var adp = new CacheDataAdapter(cmdString, con))
        {
          adp.Fill(dt);
        }
      }

      return dt;
    }

    public static DataSet DSBindDataCommand(string cmdString, string conString)
    {
      DataSet ds = new DataSet();

      using (var con = new CacheConnection(conString))
      {
        using (var adp = new CacheDataAdapter(cmdString, conString))
        {
          adp.Fill(ds);
        }
      }

      return ds;
    }

    public static string BindDataCommand(string cmdString, string conString)
    {
      string result = string.Empty;

      using (var con = new CacheConnection(conString))
      {
        con.Open();
        using (var cmd = new CacheCommand(cmdString, con))
        {
          try
          {
            result = cmd.ExecuteScalar().ToString();
          }
          catch (Exception)
          {

            return result;
          }

        }
      }

      return result;
    }

    public static DataTable DataTableExecuteProcedure(string cmdString, Dictionary<string, object> paras, string conString)
    {
      DataTable dt = new DataTable();
      using (var con = new CacheConnection(conString))
      {
        con.Open();
        using (var cmd = new CacheCommand(cmdString, con))
        {
          cmd.CommandType = CommandType.StoredProcedure;
          if (paras != null)
          {
            foreach (KeyValuePair<string, object> kvp in paras)
              cmd.Parameters.Add(new CacheParameter(kvp.Key, kvp.Value));
            using (CacheDataReader dr = cmd.ExecuteReader())
            {
              dt.Load(dr);
              return dt;
            }
          }
        }
      }
      return dt;
    }
  }
}
