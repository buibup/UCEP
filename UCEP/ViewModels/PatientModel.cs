using System.ComponentModel.DataAnnotations;
using static UCEP.Enums;

namespace UCEP.ViewModels
{
    public class PatientModel
    {
        public string HN { get; set; }
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public string Date { get; set; }
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss}", ApplyFormatInEditMode = true)]
        public string Time { get; set; }
        public Hospital PatientHospital { get; set; }
        public bool MatchAll { get; set; }
    }
}
