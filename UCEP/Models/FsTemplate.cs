using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace UCEP.Models
{
    public class FsTemplate:BaseEntity
    {
        public DateTime UseDate { get; set; }
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