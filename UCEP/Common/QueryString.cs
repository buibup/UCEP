using System;
using System.Collections.Generic;

namespace UCEP.Common
{
    public class QueryString
    {
        public static Tuple<string, Dictionary<string, string>> GetPatientOrderFS(string hn, DateTime dtmFrom, DateTime dtmTo)
        {
            string result = @"
            
             SELECT ""Vw_BillDetailAc"".""OEORI_SttDat"", 
                    ""Vw_BillDetailAc"".""OEORI_SttTim"",
 		            ""Vw_BillDetailAc"".""ARCIM_Code"" ""HospitalCode"",
 		            ""Vw_BillDetailAc"".""ARCIM_Abbrev"" ""Mean"",
 		            ""Vw_BillDetailAc"".""OEORI_PhQtyOrd"" ""Unit"",
 		            ISNULL(""Vw_BillDetailAc"".""ITM_InsCompanyShare"", 0) + ISNULL(""Vw_BillDetailAc"".""ITM_PatientShare"", 0) + ISNULL(""Vw_BillDetailAc"".""ITM_SpecialistSurcharge"", 0) ""PriceTotal""
 		            ,""Vw_BillDetailAc"".""ITM_LineTotal""
             FROM   ""SQLUser"".""Vw_BillDetailAc"" ""Vw_BillDetailAc"" INNER JOIN ""SQLUser"".""ARC_ItmMast"" ""ARC_ItmMast"" ON ""Vw_BillDetailAc"".""ARCIM_RowId"" = ""ARC_ItmMast"".""ARCIM_RowId""
             WHERE  ""Vw_BillDetailAc"".""PAPMI_No"" = ?
                    AND ""Vw_BillDetailAc"".""OEORI_Billed"" in ('B', 'I', 'P')
                    AND ARPBL_BillNo IS NOT NULL
                    AND ""Vw_BillDetailAc"".""ARCBG_Code"" <> '00100000'
                    AND 	
                    (
		                TO_TIMESTAMP(
		                    TO_CHAR(OEORI_SttDat,'YYYY-MM-DD') || ' ' || TO_CHAR(OEORI_SttTim,'HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS'
	                    ) >= TO_TIMESTAMP( ? ,'YYYY-MM-DD HH24:MI:SS')
	                    AND 
	                    TO_TIMESTAMP(
	                        TO_CHAR(OEORI_SttDat,'YYYY-MM-DD') || ' ' || TO_CHAR(OEORI_SttTim,'HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS'
	                    ) <= TO_TIMESTAMP( ? ,'YYYY-MM-DD HH24:MI:SS')
	                )

            ";

            var dFrom = $"{dtmFrom.Year}-{dtmFrom.ToString("MM-dd HH:mm:ss")}"; //dtmFrom.ToString("yyyy-MM-dd HH:mm:ss");
            var dTo = $"{dtmTo.Year}-{dtmTo.ToString("MM-dd HH:mm:ss")}"; //dtmTo.ToString("yyyy-MM-dd HH:mm:ss");

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("PAPMI_NO", hn);
            dic.Add("DtmFrom", dFrom);
            dic.Add("DtmTo", dTo);

            return new Tuple<string, Dictionary<string, string>>(result, dic); ;
        }

        public static Tuple<string, Dictionary<string, string>> GetPatientOrderDrug(string hn, DateTime dtmFrom, DateTime dtmTo)
        {
            string result = @"
            
             SELECT ""Vw_BillDetailAc"".""OEORI_SttDat"", 
                    ""Vw_BillDetailAc"".""OEORI_SttTim"",
 		            ""Vw_BillDetailAc"".""ARCIM_Code"" ""HospitalCode"",
 		            ""Vw_BillDetailAc"".""ARCIM_Abbrev"" ""Mean"",
 		            ""Vw_BillDetailAc"".""OEORI_PhQtyOrd"" ""Unit"",
 		            ISNULL(""Vw_BillDetailAc"".""ITM_InsCompanyShare"", 0) + ISNULL(""Vw_BillDetailAc"".""ITM_PatientShare"", 0) + ISNULL(""Vw_BillDetailAc"".""ITM_SpecialistSurcharge"", 0) ""PriceTotal""
 		            ,""Vw_BillDetailAc"".""ITM_LineTotal""
             FROM   ""SQLUser"".""Vw_BillDetailAc"" ""Vw_BillDetailAc"" INNER JOIN ""SQLUser"".""ARC_ItmMast"" ""ARC_ItmMast"" ON ""Vw_BillDetailAc"".""ARCIM_RowId"" = ""ARC_ItmMast"".""ARCIM_RowId""
             WHERE  ""Vw_BillDetailAc"".""PAPMI_No"" = ?
                    AND ""Vw_BillDetailAc"".""OEORI_Billed"" in ('B', 'I', 'P')
                    AND ARPBL_BillNo IS NOT NULL
                    AND ""Vw_BillDetailAc"".""ARCBG_Code"" = '00100000'
                    AND 	
                    (
		                TO_TIMESTAMP(
		                    TO_CHAR(OEORI_SttDat,'YYYY-MM-DD') || ' ' || TO_CHAR(OEORI_SttTim,'HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS'
	                    ) >= TO_TIMESTAMP( ? ,'YYYY-MM-DD HH24:MI:SS')
	                    AND 
	                    TO_TIMESTAMP(
	                        TO_CHAR(OEORI_SttDat,'YYYY-MM-DD') || ' ' || TO_CHAR(OEORI_SttTim,'HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS'
	                    ) <= TO_TIMESTAMP( ? ,'YYYY-MM-DD HH24:MI:SS')
	                )

            ";

            var dFrom = $"{dtmFrom.Year}-{dtmFrom.ToString("MM-dd HH:mm:ss")}"; //dtmFrom.ToString("yyyy-MM-dd HH:mm:ss");
            var dTo = $"{dtmTo.Year}-{dtmTo.ToString("MM-dd HH:mm:ss")}"; //dtmTo.ToString("yyyy-MM-dd HH:mm:ss");

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("PAPMI_NO", hn);
            dic.Add("DtmFrom", dFrom);
            dic.Add("DtmTo", dTo);

            return new Tuple<string, Dictionary<string, string>>(result, dic); ;
        }

        public static Tuple<string, Dictionary<string, string>> GetPatientOrderFSDrug(string hn, DateTime dtmFrom, DateTime dtmTo)
        {
            string result = @"
            
             SELECT ""Vw_BillDetailAc"".""OEORI_SttDat"", 
                    ""Vw_BillDetailAc"".""OEORI_SttTim"",
 		            ""Vw_BillDetailAc"".""ARCIM_Code"" ""HospitalCode"",
 		            ""Vw_BillDetailAc"".""ARCIM_Abbrev"" ""Mean"",
 		            sum(""Vw_BillDetailAc"".""OEORI_PhQtyOrd"") ""Unit"",
 		            sum(""Vw_BillDetailAc"".""ITM_LineTotal"") PriceTotal
                    ,Vw_BillDetailAc.ARCBG_Code
             FROM   ""SQLUser"".""Vw_BillDetailAc"" ""Vw_BillDetailAc"" INNER JOIN ""SQLUser"".""ARC_ItmMast"" ""ARC_ItmMast"" ON ""Vw_BillDetailAc"".""ARCIM_RowId"" = ""ARC_ItmMast"".""ARCIM_RowId""
             WHERE  ""Vw_BillDetailAc"".""PAPMI_No"" = ?
                    AND ""Vw_BillDetailAc"".""OEORI_Billed"" in ('B', 'I', 'P')
                    AND ARPBL_BillNo IS NOT NULL
                    AND 	
                    (
		                TO_TIMESTAMP(
		                    TO_CHAR(OEORI_SttDat,'YYYY-MM-DD') || ' ' || TO_CHAR(OEORI_SttTim,'HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS'
	                    ) >= TO_TIMESTAMP( ? ,'YYYY-MM-DD HH24:MI:SS')
	                    AND 
	                    TO_TIMESTAMP(
	                        TO_CHAR(OEORI_SttDat,'YYYY-MM-DD') || ' ' || TO_CHAR(OEORI_SttTim,'HH24:MI:SS'),'YYYY-MM-DD HH24:MI:SS'
	                    ) <= TO_TIMESTAMP( ? ,'YYYY-MM-DD HH24:MI:SS')
	                )

              Group by  ""Vw_BillDetailAc"".""OEORI_SttDat"",
                        ""Vw_BillDetailAc"".""OEORI_SttTim"",
                        ""Vw_BillDetailAc"".""ARCIM_Code"",
                        ""Vw_BillDetailAc"".""ARCIM_Abbrev"",
                        ""Vw_BillDetailAc"".""OEORI_PhQtyOrd"",
                        ""Vw_BillDetailAc"".""ARCBG_Code""
            ";

            var dFrom = $"{dtmFrom.Year}-{dtmFrom.ToString("MM-dd HH:mm:ss")}"; 
            var dTo = $"{dtmTo.Year}-{dtmTo.ToString("MM-dd HH:mm:ss")}";

            Dictionary<string, string> dic = new Dictionary<string, string>();
            dic.Add("PAPMI_NO", hn);
            dic.Add("DtmFrom", dFrom);
            dic.Add("DtmTo", dTo);

            return new Tuple<string, Dictionary<string, string>>(result, dic); ;
        }
    }
}
