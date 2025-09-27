using LEA.WebApi.Infra.Models;
using System.Collections.Generic;

namespace LEA.WebApi.Infra.Interfaces
{
    public interface IPreditorFutebolAvancado
    {
        void CalcularMedias(MediasCampeonato mediasCampeonatoCasa, MediasCampeonato mediasCampeonatoFora, MediasCampeonato mediasCampeonatoTotal);
        List<ResultadoPrevisao> PreverTodasEstatisticas(Time casa, Time visitante, int macthCount);
        List<ResultadoPrevisao> PreverResultadoFinal(Time casa, Time visitante);
        ResultadoPrevisao PreverGolsTotais(Time casa, Time visitante);
        ResultadoPrevisao PreverGolsPrimeiroTempo(Time casa, Time visitante);
        ResultadoPrevisao PreverChutesTotais(Time casa, Time visitante);
        ResultadoPrevisao PreverEscanteios(Time casa, Time visitante);
        ResultadoPrevisao PreverFaltas(Time casa, Time visitante);
        ResultadoPrevisao PreverCartoesAmarelos(Time casa, Time visitante);
        ResultadoPrevisao PreverCartoesVermelhos(Time casa, Time visitante);
    }
}