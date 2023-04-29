using System.Collections.Generic;

namespace LEA.WebApi.Service.ViewModel
{
    public class GeneralDataViewModel
    {
        public GeneralDataViewModel() { }
        public List<string> ScheduleHome { get; set; }
        public List<string> ScheduleAway { get; set; }
        public List<string> NameTeamAgainstHome { get; set; }
        public List<string> NameTeamAgainstAway { get; set; }
        public List<int> PositionHome { get; set; }
        public List<int> PositionAway { get; set; }
        public List<int> PositionAgainstHome  { get; set; }
        public List<int> PositionAgainstAway { get; set; }
    }
}
