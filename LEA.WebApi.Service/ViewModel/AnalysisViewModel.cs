using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEA.WebApi.Service.ViewModel
{
    public class AnalysisViewModel
    {
        public AnalysisViewModel() { }
        public List<short> MadeByHome { get; set; }
        public List<short> TakenByHome { get; set; }
        public List<short> MadeByAway { get; set; }
        public List<short> TakenByAway { get; set; }
        public Domain.Enuns.StatisticKind StatisticKind { get; set; }
    }
}
