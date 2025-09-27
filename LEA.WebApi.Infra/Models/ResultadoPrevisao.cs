using System.Collections.Generic;

namespace LEA.WebApi.Infra.Models
{
    public class ResultadoPrevisao
    {
        public string Estatistica { get; set; }
        public Dictionary<string, double> ProbabilidadesTimeCasa { get; set; }
        public Dictionary<string, double> ProbabilidadesTimeVisitante { get; set; }
        public Dictionary<string, double> ProbabilidadesAgregado { get; set; }
        public string StatusTimeCasa { get; set; }
        public string StatusTimeVisitante { get; set; }
        public string StatusAgregado { get; set; }
        public Dictionary<string, double> ValoresEsperadosTimeCasa { get; set; }
        public Dictionary<string, double> ValoresEsperadosTimeVisitante { get; set; }
        public Dictionary<string, double> ValoresEsperadosAgregado { get; set; }

        public ResultadoPrevisao(string estatistica)
        {
            Estatistica = estatistica;
            ProbabilidadesTimeCasa = new Dictionary<string, double>();
            ProbabilidadesTimeVisitante = new Dictionary<string, double>();
            ProbabilidadesAgregado = new Dictionary<string, double>();
            ValoresEsperadosTimeCasa = new Dictionary<string, double>();
            ValoresEsperadosTimeVisitante = new Dictionary<string, double>();
            ValoresEsperadosAgregado = new Dictionary<string, double>();
        }
    }
}
