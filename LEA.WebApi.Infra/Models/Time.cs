using System.Collections.Generic;

namespace LEA.WebApi.Infra.Models
{
    public class Time
    {
        public string Nome { get; set; }
        public List<EstatisticasPartida> UltimosJogosCasa { get; set; }
        public List<EstatisticasPartida> UltimosJogosFora { get; set; }

        public Time()
        {
            
        }

    }
}
