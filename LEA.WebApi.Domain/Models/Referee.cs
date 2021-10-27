using LEA.WebApi.Domain.Entites;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace LEA.WebApi.Domain.Models
{
    public class Referee : Entity
    {
        [StringLength(150)]
        public string Name { get; set; }
        public List<Match> Matches { get; set; }
    }
}
