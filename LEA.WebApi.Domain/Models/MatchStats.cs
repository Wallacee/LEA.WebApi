using LEA.WebApi.Domain.Entites;
using LEA.WebApi.Domain.Enuns;

namespace LEA.WebApi.Domain.Models
{
    public class MatchStats : Entity
    {
        public Team Team { get; set; }
        public int TeamId { get; set; }
        public Scoreboard ResultFullTime { get; set; }
        public Scoreboard ResultHalfTime { get; set; }
        public short GoalsFullTime { get; set; }
        public short GoalsHalfTime { get; set; }
        public short Shots { get; set; }
        public short ShotsOnTarget { get; set; }
        public short Woodwork { get; set; }
        public short Corners { get; set; }
        public short Fouls { get; set; }
        public short Offside { get; set; }
        public short Yellow { get; set; }
        public short Red { get; set; }
    }
}
