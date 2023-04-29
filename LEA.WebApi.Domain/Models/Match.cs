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
        public virtual Team HomeTeam { get; set; }
        public virtual Team AwayTeam { get; set; }
        public virtual MatchStatistics HomeStatistics { get; set; }
        public virtual MatchStatistics AwayStatistics { get; set; }
        public virtual League League { get; set; }

        public virtual Referee Referee { get; set; }
        [DataType(DataType.Date)]
        public DateTime Schedule { get; set; }
        public int HomeTeamId { get; set; }
        public int AwayTeamId { get; set; }
        public int HomeStatisticsId { get; set; }
        public int AwayStatisticsId { get; set; }
        public int LeagueId { get; set; }
        public int? RefereeId { get; set; }
    }
}
