using LEA.WebApi.Domain.Entites;
using LEA.WebApi.Domain.Enuns;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LEA.WebApi.Domain.Models
{
    [Table("Leagues")]
    public class League : Entity
    {
        [StringLength(150)]
        public string Name { get; set; }
        public Country Coutry { get; set; }
        public short Division { get; set; }
        [StringLength(200)]
        public string Shield { get; set; }
        public virtual List<Team> Teams { get; set; }
        public virtual List<Match> Matches { get; set; }
    }
}
