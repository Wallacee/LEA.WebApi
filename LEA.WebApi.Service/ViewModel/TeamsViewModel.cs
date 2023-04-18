using LEA.WebApi.Domain.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LEA.WebApi.Service.ViewModel
{
    public class TeamsViewModel
    {
        public TeamsViewModel() { }
        public List<TeamViewModel> Teams { get; set; }
    }

    public class TeamViewModel
    {
        public TeamViewModel()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string ShieldUrl { get; set; }
        public int LeagueId { get; set; }

    }
}
