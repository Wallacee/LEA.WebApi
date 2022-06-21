using LEA.WebApi.Domain.Entites;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LEA.WebApi.Domain.Models
{
    [Table("Referees")]
    public class Referee : Entity
    {
        [StringLength(150)]
        public string Name { get; set; }
        public virtual List<Match> Matches { get; set; }
    }
}
