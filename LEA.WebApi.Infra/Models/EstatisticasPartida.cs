namespace LEA.WebApi.Infra.Models
{
    public class EstatisticasPartida
    {
        public int GolsPrimeiroTempo { get; set; }
        public int GolsSegundoTempo { get; set; }
        public int GolsTotais => GolsPrimeiroTempo + GolsSegundoTempo;
        public int ChutesTotais { get; set; }
        public int ChutesNoAlvo { get; set; }
        public int Escanteios { get; set; }
        public int Faltas { get; set; }
        public int CartoesAmarelos { get; set; }
        public int CartoesVermelhos { get; set; }
        public string Adversario { get; set; }
        public bool JogoEmCasa { get; set; }
    }
}
