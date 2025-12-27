using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEA.WebApi.Domain.Models
{
    public class TeamStrengthProfile
    {
        public Strength Home { get; set; }
        public Strength Away { get; set; }
    }

    public class Strength
    {
        public double Attack { get; set; }
        public double Defense { get; set; }
    }
}
