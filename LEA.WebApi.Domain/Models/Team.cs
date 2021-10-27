using LEA.WebApi.Domain.Entites;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LEA.WebApi.Domain.Models
{
    public class Team : Entity
    {
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Shield { get; set; }
        public League League { get; set; }
        public int LeagueId { get; set; }
        public List<MatchStats> MatchStatsList { get; set; }
    }
}
