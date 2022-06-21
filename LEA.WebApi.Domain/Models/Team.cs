using LEA.WebApi.Domain.Entites;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LEA.WebApi.Domain.Models
{
    [Table("Teams")]
    public class Team : Entity
    {
        
        [StringLength(150)]
        public string Name { get; set; }
        [StringLength(200)]
        public string Shield { get; set; }
        public League League { get; set; }
        public int LeagueId { get; set; }
        public virtual List<Match> HomeMatches  { get; set; }
        public virtual List<Match> AwayMatches { get; set; }
    }
}
