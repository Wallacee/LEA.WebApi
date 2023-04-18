using LEA.WebApi.Domain.Models;
using System.Collections.Generic;

namespace LEA.WebApi.Service.ViewModel
{
    public class LeaguesViewModel
    {
        public LeaguesViewModel() { }
        public List<LeagueViewModel> Leagues { get; set; }
    }

    public class LeagueViewModel
    {
        public LeagueViewModel()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        //public int Country { get; set; }
        public short Division { get; set; }
        public string ShieldUrl { get; set; }
    }

}
