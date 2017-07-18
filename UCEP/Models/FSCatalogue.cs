using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UCEP.Models
{
    public class FsCatalogue:BaseEntity
    {
        [MaxLength(20)]
        public string HospitalCode { get; set; }
        [MaxLength(50)]
        public string FSCodeNIEMS { get; set; }
        [MaxLength(50)]
        public string FSCodeHos { get; set; }
        [MaxLength(10)]
        public string Category { get; set; }
        public string Meaning { get; set; }
        [MaxLength(50)]
        public string Unit { get; set; }
        public decimal Price { get; set; }
        public DateTime EffectiveDate { get; set; }
        [MaxLength(50)]
        public string Status { get; set; }
        public DateTime ApprovalDate { get; set; }
    }
}
