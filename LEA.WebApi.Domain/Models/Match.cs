using LEA.WebApi.Domain.Entites;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LEA.WebApi.Domain.Models
{
    [Table("Matches")]
    public class Match : Entity
    {
        [DataType(DataType.Date)]
        public DateTime Schedule { get; set; }
        public virtual MatchStatistics Home { get; set; }
        public virtual MatchStatistics Away { get; set; }
        public Referee Referee { get; set; }
        public int? HomeId { get; set; }
        public int? AwayId { get; set; }
        public int RefereeId { get; set; }
    }
}
