using System.ComponentModel.DataAnnotations;

namespace UCEP.Models
{
    public class FsDrugTemplate
    {
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd HH:mm:ss}")]
        public string UseDate { get; set; }
        [MaxLength(50)]
        public string FSCodeOrTMTCode { get; set; }
        [MaxLength(50)]
        public string HospitalCode { get; set; }
        [MaxLength(10)]
        public string Category { get; set; }
        public string Mean { get; set; }
        [MaxLength(50)]
        public string Unit { get; set; }
        public decimal PriceTotal { get; set; }
    }


}