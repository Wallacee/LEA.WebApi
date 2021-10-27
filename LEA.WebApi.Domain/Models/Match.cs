using LEA.WebApi.Domain.Entites;
using System;
using System.ComponentModel.DataAnnotations;

namespace LEA.WebApi.Domain.Models
{
    public class Match : Entity
    {
        [DataType(DataType.Date)]
        public DateTime Schedule { get; set; }
        public MatchStats Home { get; set; }
        public MatchStats Away { get; set; }
        public Referee Referee { get; set; }
        public int HomeId { get; set; }
        public int AwayId { get; set; }
        public int RefereeId { get; set; }
    }
}
