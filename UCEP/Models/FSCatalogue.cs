using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace UCEP.Models
{
    public enum FsCatalogueEnum
    {
        HospitalCode,
        FSCodeNIEMS,
        FSCodeHos,
        Category,
        Meaning,
        Unit,
        Price,
        EffectiveDate,
        Status,
        ApprovalDate
    }
    public class FsCatalogue:BaseEntity
    {
        [Required]
        [MaxLength(20)]
        public string HospitalCode { get; set; }
        [Required]
        [MaxLength(50)]
        public string FSCodeNIEMS { get; set; }
        [Required]
        [MaxLength(50)]
        public string FSCodeHos { get; set; }
        [Required]
        [MaxLength(10)]
        public string Category { get; set; }
        [Required]
        public string Meaning { get; set; }
        [Required]
        [MaxLength(50)]
        public string Unit { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public DateTime? EffectiveDate { get; set; }
        [Required]
        [MaxLength(50)]
        public string Status { get; set; }
        [Required]
        public DateTime? ApprovalDate { get; set; }
    }
}
