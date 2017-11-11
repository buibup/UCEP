using System;
using System.ComponentModel.DataAnnotations;

namespace UCEP.Models
{
    public enum DrugCatalogueEnum
    {
        HospDrugCode
        , PRODUCTCAT
        , TMTID
        , SPECPREP
        , GENERICNAME
        , TRADENAME
        , DFSCODE
        , DOSAGEFORM
        , STRENGTH
        , CONTENT
        , UNITPRICE
        , DISTRIBUTOR
        , MANUFACTURER
        , ISED
        , NDC24
        , PACKSIZE
        , PACKPRICE
        , UPDATEFLAG
        , DATECHANGE
        , DATEUPDATE
        , DATEEFFECTIVE
        , ISED_APPROVED
        , NDC24_APPROVED
        , DATE_APPROVED
        , ISED_STATUS
    }

    public class DrugCatalogue
    {
        public string HospDrugCode { get; set; }
        public int PRODUCTCAT { get; set; }
        public string TMTID { get; set; }
        public string SPECPREP { get; set; }
        public string GENERICNAME { get; set; }
        public string TRADENAME { get; set; }
        public string DFSCODE { get; set; }
        public string DOSAGEFORM { get; set; }
        public string STRENGTH { get; set; }
        public string CONTENT { get; set; }
        public decimal UNITPRICE { get; set; }
        public string DISTRIBUTOR { get; set; }
        public string MANUFACTURER { get; set; }
        public string ISED { get; set; }
        public string NDC24 { get; set; }
        public string PACKSIZE { get; set; }
        public string PACKPRICE { get; set; }
        public string UPDATEFLAG { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DATECHANGE { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DATEUPDATE { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DATEEFFECTIVE { get; set; }
        public string ISED_APPROVED { get; set; }
        public string NDC24_APPROVED { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? DATE_APPROVED { get; set; }
        public string ISED_STATUS { get; set; }
    }
}
