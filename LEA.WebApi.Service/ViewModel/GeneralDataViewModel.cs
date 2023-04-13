using System.Collections.Generic;

namespace LEA.WebApi.Service.ViewModel
{
    public class GeneralDataViewModel
    {
        public GeneralDataViewModel() { }
        public List<string> ScheduleHome { get; set; }
        public List<string> ScheduleAwyay { get; set; }
        public List<string> NameTeamAgainstHome { get; set; }
        public List<string> NameTeamAgainstAway { get; set; }
    }
}
