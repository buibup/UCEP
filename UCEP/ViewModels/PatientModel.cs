using static UCEP.Enums;

namespace UCEP.ViewModels
{
    public class PatientModel
  {
    public string HN { get; set; }
    public string Date { get; set; }
    public string Time { get; set; }
    public Hospital PatientHospital { get; set; }
    public bool MatchAll { get; set; }
  }
}
