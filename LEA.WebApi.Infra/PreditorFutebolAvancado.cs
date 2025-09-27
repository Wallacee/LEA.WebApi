using LEA.WebApi.Infra.Interfaces;
using LEA.WebApi.Infra.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LEA.WebApi.Infra
{
    public class PreditorFutebolAvancado: IPreditorFutebolAvancado
    {
        private int numeroJogosAnalisar;
        private MediasCampeonato mediasCampeonatoCasa;
        private MediasCampeonato mediasCampeonatoFora;
        private MediasCampeonato mediasCampeonatoTotal;
        private double fatorVantagemCasa;
        private ForcaTime forcaOfensivaCasa;
        private ForcaTime forcaDefensivaCasa;
        private ForcaTime forcaOfensivaVisitante;
        private ForcaTime forcaDefensivaVisitante;


        public int NumeroJogosAnalisar { get => numeroJogosAnalisar; set => numeroJogosAnalisar = value; }

        public MediasCampeonato MediasCampeonatoTotal { get => mediasCampeonatoTotal; set => mediasCampeonatoTotal = value; }
        public MediasCampeonato MediasCampeonatoCasa { get => mediasCampeonatoCasa; set => mediasCampeonatoCasa = value; }
        public MediasCampeonato MediasCampeonatoFora { get => mediasCampeonatoFora; set => mediasCampeonatoFora = value; }


        private double FatorVantagemCasa { get => fatorVantagemCasa; set => fatorVantagemCasa = value; }

        private ForcaTime ForcaOfensivaCasa { get => forcaOfensivaCasa; set => forcaOfensivaCasa = value; }

        private ForcaTime ForcaDefensivaCasa { get => forcaDefensivaCasa; set => forcaDefensivaCasa = value; }

        private ForcaTime ForcaOfensivaVisitante { get => forcaOfensivaVisitante; set => forcaOfensivaVisitante = value; }

        private ForcaTime ForcaDefensivaVisitante { get => forcaDefensivaVisitante; set => forcaDefensivaVisitante = value; }


        public PreditorFutebolAvancado(){}
        
        private void CalcularForcaTime(Time casa, Time visitante)
        {
            ForcaOfensivaCasa = CalcularForcaOfensivaPonderada(casa.UltimosJogosCasa);
            ForcaDefensivaCasa = CalcularForcaDefensivaPonderada(casa.UltimosJogosCasa);
            ForcaOfensivaVisitante = CalcularForcaOfensivaPonderada(visitante.UltimosJogosFora);
            ForcaDefensivaVisitante = CalcularForcaDefensivaPonderada(visitante.UltimosJogosFora);
        }

        public void CalcularMedias(MediasCampeonato mediasCampeonatoCasa, MediasCampeonato mediasCampeonatoFora, MediasCampeonato mediasCampeonatoTotal)
        {
            MediasCampeonatoCasa = mediasCampeonatoCasa;
            MediasCampeonatoFora = mediasCampeonatoFora;
            MediasCampeonatoTotal = mediasCampeonatoTotal;
        }


        public List<ResultadoPrevisao> PreverTodasEstatisticas(Time casa, Time visitante, int mmatchCount)
        {
            NumeroJogosAnalisar = mmatchCount;
            CalcularForcaTime(casa, visitante);
            var resultados = new List<ResultadoPrevisao>
            {
                PreverResultadoFinal(ForcaOfensivaCasa, ForcaDefensivaCasa, ForcaOfensivaVisitante, ForcaDefensivaVisitante),
                PreverGolsTotais(ForcaOfensivaCasa, ForcaOfensivaVisitante, ForcaDefensivaCasa, ForcaDefensivaVisitante),
                PreverGolsPrimeiroTempo(ForcaOfensivaCasa, ForcaOfensivaVisitante, ForcaDefensivaCasa, ForcaDefensivaVisitante),
                PreverChutesTotais(ForcaOfensivaCasa, ForcaOfensivaVisitante),
                PreverChutesNoAlvo(ForcaOfensivaCasa, ForcaOfensivaVisitante),
                PreverEscanteios(ForcaOfensivaCasa, ForcaOfensivaVisitante),
                PreverFaltas(casa.UltimosJogosCasa, visitante.UltimosJogosFora),
                PreverCartoesAmarelos(casa.UltimosJogosCasa, visitante.UltimosJogosFora),
                PreverCartoesVermelhos(casa.UltimosJogosCasa, visitante.UltimosJogosFora)
            };

            return resultados;
        }

        public List<ResultadoPrevisao> PreverResultadoFinal(Time casa, Time visitante)
        {
            CalcularForcaTime(casa, visitante);
            var resultados = new List<ResultadoPrevisao>();
            {
                PreverResultadoFinal(ForcaOfensivaCasa, ForcaDefensivaCasa, ForcaOfensivaVisitante, ForcaDefensivaVisitante);
            }
            return resultados;
        }

        public ResultadoPrevisao PreverGolsTotais(Time casa, Time visitante)
        {
            CalcularForcaTime(casa, visitante);
            return PreverGolsTotais(ForcaOfensivaCasa, ForcaDefensivaCasa, ForcaOfensivaVisitante, ForcaDefensivaVisitante);
        }

        public ResultadoPrevisao PreverGolsPrimeiroTempo(Time casa, Time visitante)
        {
            CalcularForcaTime(casa, visitante);
            return PreverGolsPrimeiroTempo(ForcaOfensivaCasa, ForcaDefensivaCasa, ForcaOfensivaVisitante, ForcaDefensivaVisitante);
        }

        public ResultadoPrevisao PreverChutesTotais(Time casa, Time visitante)
        {
            CalcularForcaTime(casa, visitante);
            return PreverChutesTotais(ForcaOfensivaCasa, ForcaOfensivaVisitante);
        }

        public ResultadoPrevisao PreverEscanteios(Time casa, Time visitante)
        {
            CalcularForcaTime(casa, visitante);
            return PreverEscanteios(ForcaOfensivaCasa, ForcaOfensivaVisitante);
        }

        public ResultadoPrevisao PreverFaltas(Time casa, Time visitante)
        {
            CalcularForcaTime(casa, visitante);
            return PreverFaltas(casa.UltimosJogosCasa, visitante.UltimosJogosFora);
        }

        public ResultadoPrevisao PreverCartoesAmarelos(Time casa, Time visitante)
        {
            CalcularForcaTime(casa, visitante);
            return PreverCartoesAmarelos(casa.UltimosJogosCasa, visitante.UltimosJogosFora);
        }

        public ResultadoPrevisao PreverCartoesVermelhos(Time casa, Time visitante)
        {
            CalcularForcaTime(casa, visitante);
            return PreverCartoesVermelhos(casa.UltimosJogosCasa, visitante.UltimosJogosFora);
        }

        private ResultadoPrevisao PreverResultadoFinal(ForcaTime forcaOfensivaCasa, ForcaTime forcaDefensivaCasa,
                                                     ForcaTime forcaOfensivaVisitante, ForcaTime forcaDefensivaVisitante)
        {
            var resultado = new ResultadoPrevisao("Resultado Final");

            double probabilidadeVitoriaCasa = CalcularProbabilidadeLogistica(
                forcaOfensivaCasa.Gols * 0.4 +
                forcaDefensivaVisitante.Gols * 0.3 +
                forcaOfensivaCasa.Chutes * 0.1 +
                forcaOfensivaCasa.ChutesNoAlvo * 0.2,
                0.5
            );

            double probabilidadeVitoriaVisitante = CalcularProbabilidadeLogistica(
                forcaOfensivaVisitante.Gols * 0.4 +
                forcaDefensivaCasa.Gols * 0.3 +
                forcaOfensivaVisitante.Chutes * 0.1 +
                forcaOfensivaVisitante.ChutesNoAlvo * 0.2,
                -0.3
            );

            double probabilidadeEmpate = 1 - probabilidadeVitoriaCasa - probabilidadeVitoriaVisitante;

            probabilidadeVitoriaCasa *= FatorVantagemCasa;
            probabilidadeVitoriaVisitante /= FatorVantagemCasa;

            double total = probabilidadeVitoriaCasa + probabilidadeEmpate + probabilidadeVitoriaVisitante;
            probabilidadeVitoriaCasa = probabilidadeVitoriaCasa / total * 100;
            probabilidadeEmpate = probabilidadeEmpate / total * 100;
            probabilidadeVitoriaVisitante = probabilidadeVitoriaVisitante / total * 100;

            resultado.ProbabilidadesAgregado.Add("Probabilidade de vitória do time da casa", Math.Round(probabilidadeVitoriaCasa, 2));
            resultado.ProbabilidadesAgregado.Add("Probabilidade de empate", Math.Round(probabilidadeEmpate, 2));
            resultado.ProbabilidadesAgregado.Add("Probabilidade de vitória do time visitante", Math.Round(probabilidadeVitoriaVisitante, 2));

            double golsEsperadosCasa = (forcaOfensivaCasa.Gols + forcaDefensivaVisitante.Gols) / 2 * FatorVantagemCasa;
            double golsEsperadosVisitante = (forcaOfensivaVisitante.Gols + forcaDefensivaCasa.Gols) / 2 / FatorVantagemCasa;

            resultado.ValoresEsperadosTimeCasa.Add("Gols esperados", Math.Round(golsEsperadosCasa, 2));
            resultado.ValoresEsperadosTimeVisitante.Add("Gols esperados", Math.Round(golsEsperadosVisitante, 2));
            resultado.ValoresEsperadosAgregado.Add("Gols totais esperados", Math.Round(golsEsperadosCasa + golsEsperadosVisitante, 2));

            double totalGolsEsperados = golsEsperadosCasa + golsEsperadosVisitante;
            string statusGols = totalGolsEsperados > MediasCampeonatoTotal.MediaGolsPorPartida * 1.2 ? "acima da média" :
                               totalGolsEsperados < MediasCampeonatoTotal.MediaGolsPorPartida * 0.8 ? "abaixo da média" : "na média";

            if (probabilidadeVitoriaCasa > 55) resultado.StatusAgregado = $"Forte probabilidade de vitória da casa ({statusGols})";
            else if (probabilidadeVitoriaCasa > 45) resultado.StatusAgregado = $"Ligeira vantagem para o time da casa ({statusGols})";
            else if (probabilidadeVitoriaVisitante > 55) resultado.StatusAgregado = $"Forte probabilidade de vitória visitante ({statusGols})";
            else if (probabilidadeVitoriaVisitante > 45) resultado.StatusAgregado = $"Ligeira vantagem para the time visitante ({statusGols})";
            else if (probabilidadeEmpate > 40) resultado.StatusAgregado = $"Jogo equilibrado com alta probabilidade de empate ({statusGols})";
            else resultado.StatusAgregado = $"Resultado imprevisível - jogo muito equilibrado ({statusGols})";

            return resultado;
        }

        private ResultadoPrevisao PreverGolsTotais(ForcaTime forcaOfensivaCasa, ForcaTime forcaOfensivaVisitante,
                                                 ForcaTime forcaDefensivaCasa, ForcaTime forcaDefensivaVisitante)
        {
            var resultado = new ResultadoPrevisao("Gols Totais");

            double lambdaCasa = (forcaOfensivaCasa.Gols + forcaDefensivaVisitante.Gols) / 2 * FatorVantagemCasa;
            double lambdaVisitante = (forcaOfensivaVisitante.Gols + forcaDefensivaCasa.Gols) / 2 / FatorVantagemCasa;

            double golsEsperadosCasa = lambdaCasa;
            double golsEsperadosVisitante = lambdaVisitante;
            double golsEsperadosAgregado = lambdaCasa + lambdaVisitante;

            resultado.ValoresEsperadosTimeCasa.Add("Gols esperados", Math.Round(golsEsperadosCasa, 2));
            resultado.ValoresEsperadosTimeVisitante.Add("Gols esperados", Math.Round(golsEsperadosVisitante, 2));
            resultado.ValoresEsperadosAgregado.Add("Gols totais esperados", Math.Round(golsEsperadosAgregado, 2));

            // Calcular probabilidades acumulativas para time da casa
            for (int i = 0; i <= 10; i++)
            {
                double probabilidade = CalcularProbabilidadePoissonAcumulativa(golsEsperadosCasa, i, int.MaxValue) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesTimeCasa.Add($"Probabilidade de pelo menos {i} gols", Math.Round(probabilidade, 2));
                }
            }

            // Calcular probabilidades acumulativas para time visitante
            for (int i = 0; i <= 10; i++)
            {
                double probabilidade = CalcularProbabilidadePoissonAcumulativa(golsEsperadosVisitante, i, int.MaxValue) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesTimeVisitante.Add($"Probabilidade de pelo menos {i} gols", Math.Round(probabilidade, 2));
                }
            }

            // Calcular probabilidades acumulativas para agregado
            for (int i = 0; i <= 15; i++)
            {
                double probabilidade = CalcularProbabilidadePoissonAcumulativa(golsEsperadosAgregado, i, int.MaxValue) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesAgregado.Add($"Probabilidade de pelo menos {i} gols", Math.Round(probabilidade, 2));
                }
            }

            // Status para time da casa
            if (golsEsperadosCasa > MediasCampeonatoCasa.MediaGolsPorPartida * 1.2)
                resultado.StatusTimeCasa = "Time da casa com muitos gols esperados (acima da média do campeonato)";
            else if (golsEsperadosCasa > MediasCampeonatoCasa.MediaGolsPorPartida * 0.8)
                resultado.StatusTimeCasa = "Time da casa com número médio de gols esperado (na média do campeonato)";
            else
                resultado.StatusTimeCasa = "Time da casa com poucos gols esperados (abaixo da média do campeonato)";

            // Status para time visitante
            if (golsEsperadosVisitante > MediasCampeonatoFora.MediaGolsPorPartida * 1.2)
                resultado.StatusTimeVisitante = "Time visitante com muitos gols esperados (acima da média do campeonato)";
            else if (golsEsperadosVisitante > MediasCampeonatoFora.MediaGolsPorPartida * 0.8)
                resultado.StatusTimeVisitante = "Time visitante com número médio de gols esperado (na média do campeonato)";
            else
                resultado.StatusTimeVisitante = "Time visitante com poucos gols esperados (abaixo da média do campeonato)";

            // Status para agregado
            if (golsEsperadosAgregado > MediasCampeonatoTotal.MediaGolsPorPartida * 1.2)
                resultado.StatusAgregado = "Jogo com muitos gols esperados (acima da média do campeonato)";
            else if (golsEsperadosAgregado > MediasCampeonatoTotal.MediaGolsPorPartida * 0.8)
                resultado.StatusAgregado = "Jogo com número médio de gols esperado (na média do campeonato)";
            else
                resultado.StatusAgregado = "Jogo com poucos gols esperados (abaixo da média do campeonato)";

            return resultado;
        }

        private ResultadoPrevisao PreverGolsPrimeiroTempo(ForcaTime forcaOfensivaCasa, ForcaTime forcaOfensivaVisitante,
                                                ForcaTime forcaDefensivaCasa, ForcaTime forcaDefensivaVisitante)
        {
            var resultado = new ResultadoPrevisao("Gols Primeiro Tempo");

            double lambdaCasa = (forcaOfensivaCasa.GolsPrimeiroTempo + forcaDefensivaVisitante.GolsPrimeiroTempo) / 2 * FatorVantagemCasa;
            double lambdaVisitante = (forcaOfensivaVisitante.GolsPrimeiroTempo + forcaDefensivaCasa.GolsPrimeiroTempo) / 2 / FatorVantagemCasa;

            double golsPrimeiroTempoEsperadosCasa = lambdaCasa;
            double golsPrimeiroTempoEsperadosVisitante = lambdaVisitante;
            double golsPrimeiroTempoEsperadosAgregado = lambdaCasa + lambdaVisitante;

            resultado.ValoresEsperadosTimeCasa.Add("Gols primeiro tempo esperados", Math.Round(golsPrimeiroTempoEsperadosCasa, 2));
            resultado.ValoresEsperadosTimeVisitante.Add("Gols primeiro tempo esperados", Math.Round(golsPrimeiroTempoEsperadosVisitante, 2));
            resultado.ValoresEsperadosAgregado.Add("Gols primeiro tempo totais esperados", Math.Round(golsPrimeiroTempoEsperadosAgregado, 2));

            // Calcular probabilidades acumulativas para time da casa
            for (int i = 0; i <= 6; i++)
            {
                double probabilidade = CalcularProbabilidadePoissonAcumulativa(golsPrimeiroTempoEsperadosCasa, i, int.MaxValue) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesTimeCasa.Add($"Probabilidade de pelo menos {i} gols", Math.Round(probabilidade, 2));
                }
            }

            // Calcular probabilidades acumulativas para time visitante
            for (int i = 0; i <= 6; i++)
            {
                double probabilidade = CalcularProbabilidadePoissonAcumulativa(golsPrimeiroTempoEsperadosVisitante, i, int.MaxValue) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesTimeVisitante.Add($"Probabilidade de pelo menos {i} gols", Math.Round(probabilidade, 2));
                }
            }

            // Calcular probabilidades acumulativas para agregado
            for (int i = 0; i <= 8; i++)
            {
                double probabilidade = CalcularProbabilidadePoissonAcumulativa(golsPrimeiroTempoEsperadosAgregado, i, int.MaxValue) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesAgregado.Add($"Probabilidade de pelo menos {i} gols", Math.Round(probabilidade, 2));
                }
            }

            // Status para time da casa
            if (golsPrimeiroTempoEsperadosCasa > MediasCampeonatoCasa.MediaGolsPrimeiroTempoPorPartida * 1.2)
                resultado.StatusTimeCasa = "Time da casa com muitos gols no primeiro tempo (acima da média do campeonato)";
            else if (golsPrimeiroTempoEsperadosCasa > MediasCampeonatoCasa.MediaGolsPrimeiroTempoPorPartida * 0.8)
                resultado.StatusTimeCasa = "Time da casa com número médio de gols no primeiro tempo (na média do campeonato)";
            else
                resultado.StatusTimeCasa = "Time da casa com poucos gols no primeiro tempo (abaixo da média do campeonato)";

            // Status para time visitante
            if (golsPrimeiroTempoEsperadosVisitante > MediasCampeonatoFora.MediaGolsPrimeiroTempoPorPartida * 1.2)
                resultado.StatusTimeVisitante = "Time visitante com muitos gols no primeiro tempo (acima da média do campeonato)";
            else if (golsPrimeiroTempoEsperadosVisitante > MediasCampeonatoFora.MediaGolsPrimeiroTempoPorPartida * 0.8)
                resultado.StatusTimeVisitante = "Time visitante com número médio de gols no primeiro tempo (na média do campeonato)";
            else
                resultado.StatusTimeVisitante = "Time visitante com poucos gols no primeiro tempo (abaixo da média do campeonato)";

            // Status para agregado
            if (golsPrimeiroTempoEsperadosAgregado > MediasCampeonatoTotal.MediaGolsPrimeiroTempoPorPartida * 1.2)
                resultado.StatusAgregado = "Primeiro tempo com muitos gols esperados (acima da média do campeonato)";
            else if (golsPrimeiroTempoEsperadosAgregado > MediasCampeonatoTotal.MediaGolsPrimeiroTempoPorPartida * 0.8)
                resultado.StatusAgregado = "Primeiro tempo com número normal de gols (na média do campeonato)";
            else
                resultado.StatusAgregado = "Primeiro tempo com poucos gols esperados (abaixo da média do campeonato)";

            return resultado;
        }

        private ResultadoPrevisao PreverChutesTotais(ForcaTime forcaOfensivaCasa, ForcaTime forcaOfensivaVisitante)
        {
            var resultado = new ResultadoPrevisao("Chutes Totais");

            double chutesEsperadosCasa = forcaOfensivaCasa.Chutes * FatorVantagemCasa;
            double chutesEsperadosVisitante = forcaOfensivaVisitante.Chutes / FatorVantagemCasa;
            double chutesTotaisEsperados = chutesEsperadosCasa + chutesEsperadosVisitante;

            resultado.ValoresEsperadosTimeCasa.Add("Chutes esperados", Math.Round(chutesEsperadosCasa, 2));
            resultado.ValoresEsperadosTimeVisitante.Add("Chutes esperados", Math.Round(chutesEsperadosVisitante, 2));
            resultado.ValoresEsperadosAgregado.Add("Chutes totais esperados", Math.Round(chutesTotaisEsperados, 2));

            // Calcular probabilidades acumulativas usando distribuição normal
            double desvioPadrao = 4.0;

            // Para time da casa
            for (int i = 0; i <= 30; i++)
            {
                double probabilidade = CalcularProbabilidadeNormalAcumulativa(chutesEsperadosCasa, i, int.MaxValue, desvioPadrao) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesTimeCasa.Add($"Probabilidade de pelo menos {i} chutes", Math.Round(probabilidade, 2));
                }
            }

            // Para time visitante
            for (int i = 0; i <= 30; i++)
            {
                double probabilidade = CalcularProbabilidadeNormalAcumulativa(chutesEsperadosVisitante, i, int.MaxValue, desvioPadrao) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesTimeVisitante.Add($"Probabilidade de pelo menos {i} chutes", Math.Round(probabilidade, 2));
                }
            }

            // Para agregado
            for (int i = 0; i <= 40; i++)
            {
                double probabilidade = CalcularProbabilidadeNormalAcumulativa(chutesTotaisEsperados, i, int.MaxValue, desvioPadrao) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesAgregado.Add($"Probabilidade de pelo menos {i} chutes", Math.Round(probabilidade, 2));
                }
            }

            // Status para time da casa
            if (chutesEsperadosCasa > MediasCampeonatoCasa.MediaChutesPorPartida * 1.2)
                resultado.StatusTimeCasa = "Time da casa com muitas finalizações esperadas (acima da média do campeonato)";
            else if (chutesEsperadosCasa > MediasCampeonatoCasa.MediaChutesPorPartida * 0.8)
                resultado.StatusTimeCasa = "Time da casa com número médio de finalizações esperado (na média do campeonato)";
            else
                resultado.StatusTimeCasa = "Time da casa com poucas finalizações esperadas (abaixo da média do campeonato)";

            // Status para time visitante
            if (chutesEsperadosVisitante > MediasCampeonatoFora.MediaChutesPorPartida * 1.2)
                resultado.StatusTimeVisitante = "Time visitante com muitas finalizações esperadas (acima da média do campeonato)";
            else if (chutesEsperadosVisitante > MediasCampeonatoFora.MediaChutesPorPartida * 0.8)
                resultado.StatusTimeVisitante = "Time visitante com número médio de finalizações esperado (na média do campeonato)";
            else
                resultado.StatusTimeVisitante = "Time visitante com poucas finalizações esperadas (abaixo da média do campeonato)";

            // Status para agregado
            if (chutesTotaisEsperados > MediasCampeonatoTotal.MediaChutesPorPartida * 1.2)
                resultado.StatusAgregado = "Jogo com muitas finalizações esperadas (acima da média do campeonato)";
            else if (chutesTotaisEsperados > MediasCampeonatoTotal.MediaChutesPorPartida * 0.8)
                resultado.StatusAgregado = "Número médio de finalizações esperado (na média do campeonato)";
            else
                resultado.StatusAgregado = "Jogo com poucas finalizações esperadas (abaixo da média do campeonato)";

            return resultado;
        }

        private ResultadoPrevisao PreverChutesNoAlvo(ForcaTime forcaOfensivaCasa, ForcaTime forcaOfensivaVisitante)
        {
            var resultado = new ResultadoPrevisao("Chutes no Alvo");

            double precisaoCasa = forcaOfensivaCasa.Chutes > 0 ? forcaOfensivaCasa.ChutesNoAlvo / forcaOfensivaCasa.Chutes : 0.35;
            double precisaoVisitante = forcaOfensivaVisitante.Chutes > 0 ? forcaOfensivaVisitante.ChutesNoAlvo / forcaOfensivaVisitante.Chutes : 0.35;

            double chutesNoAlvoEsperadosCasa = forcaOfensivaCasa.Chutes * precisaoCasa * FatorVantagemCasa;
            double chutesNoAlvoEsperadosVisitante = forcaOfensivaVisitante.Chutes * precisaoVisitante / FatorVantagemCasa;
            double chutesNoAlvoTotaisEsperados = chutesNoAlvoEsperadosCasa + chutesNoAlvoEsperadosVisitante;

            resultado.ValoresEsperadosTimeCasa.Add("Chutes no alvo esperados", Math.Round(chutesNoAlvoEsperadosCasa, 2));
            resultado.ValoresEsperadosTimeVisitante.Add("Chutes no alvo esperados", Math.Round(chutesNoAlvoEsperadosVisitante, 2));
            resultado.ValoresEsperadosAgregado.Add("Chutes no alvo totais esperados", Math.Round(chutesNoAlvoTotaisEsperados, 2));

            // Calcular probabilidades acumulativas usando distribuição normal
            double desvioPadrao = 2.5;

            // Para time da casa
            for (int i = 0; i <= 20; i++)
            {
                double probabilidade = CalcularProbabilidadeNormalAcumulativa(chutesNoAlvoEsperadosCasa, i, int.MaxValue, desvioPadrao) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesTimeCasa.Add($"Probabilidade de pelo menos {i} chutes no alvo", Math.Round(probabilidade, 2));
                }
            }

            // Para time visitante
            for (int i = 0; i <= 20; i++)
            {
                double probabilidade = CalcularProbabilidadeNormalAcumulativa(chutesNoAlvoEsperadosVisitante, i, int.MaxValue, desvioPadrao) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesTimeVisitante.Add($"Probabilidade de pelo menos {i} chutes no alvo", Math.Round(probabilidade, 2));
                }
            }

            // Para agregado
            for (int i = 0; i <= 25; i++)
            {
                double probabilidade = CalcularProbabilidadeNormalAcumulativa(chutesNoAlvoTotaisEsperados, i, int.MaxValue, desvioPadrao) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesAgregado.Add($"Probabilidade de pelo menos {i} chutes no alvo", Math.Round(probabilidade, 2));
                }
            }

            // Status para time da casa
            if (chutesNoAlvoEsperadosCasa > MediasCampeonatoCasa.MediaChutesNoAlvoPorPartida * 1.2)
                resultado.StatusTimeCasa = "Time da casa com muitas finalizações perigosas (acima da média do campeonato)";
            else if (chutesNoAlvoEsperadosCasa > MediasCampeonatoCasa.MediaChutesNoAlvoPorPartida * 0.8)
                resultado.StatusTimeCasa = "Time da casa com número médio de finalizações perigosas (na média do campeonato)";
            else
                resultado.StatusTimeCasa = "Time da casa com poucas finalizações perigosas (abaixo da média do campeonato)";

            // Status para time visitante
            if (chutesNoAlvoEsperadosVisitante > MediasCampeonatoFora.MediaChutesNoAlvoPorPartida * 1.2)
                resultado.StatusTimeVisitante = "Time visitante com muitas finalizações perigosas (acima da média do campeonato)";
            else if (chutesNoAlvoEsperadosVisitante > MediasCampeonatoFora.MediaChutesNoAlvoPorPartida * 0.8)
                resultado.StatusTimeVisitante = "Time visitante com número médio de finalizações perigosas (na média do campeonato)";
            else
                resultado.StatusTimeVisitante = "Time visitante com poucas finalizações perigosas (abaixo da média do campeonato)";

            // Status para agregado
            if (chutesNoAlvoTotaisEsperados > MediasCampeonatoTotal.MediaChutesNoAlvoPorPartida * 1.2)
                resultado.StatusAgregado = "Muitas finalizações perigosas esperadas (acima da média do campeonato)";
            else if (chutesNoAlvoTotaisEsperados > MediasCampeonatoTotal.MediaChutesNoAlvoPorPartida * 0.8)
                resultado.StatusAgregado = "Número médio de finalizações perigosas (na média do campeonato)";
            else
                resultado.StatusAgregado = "Poucas finalizações perigosas esperadas (abaixo da média do campeonato)";

            return resultado;
        }

        private ResultadoPrevisao PreverEscanteios(ForcaTime forcaOfensivaCasa, ForcaTime forcaOfensivaVisitante)
        {
            var resultado = new ResultadoPrevisao("Escanteios");

            double taxaEscanteiosCasa = forcaOfensivaCasa.Chutes > 0 ? forcaOfensivaCasa.Escanteios / forcaOfensivaCasa.Chutes : 0.3;
            double taxaEscanteiosVisitante = forcaOfensivaVisitante.Chutes > 0 ? forcaOfensivaVisitante.Escanteios / forcaOfensivaVisitante.Chutes : 0.3;

            double escanteiosEsperadosCasa = forcaOfensivaCasa.Chutes * taxaEscanteiosCasa * FatorVantagemCasa;
            double escanteiosEsperadosVisitante = forcaOfensivaVisitante.Chutes * taxaEscanteiosVisitante / FatorVantagemCasa;
            double escanteiosTotaisEsperados = escanteiosEsperadosCasa + escanteiosEsperadosVisitante;

            resultado.ValoresEsperadosTimeCasa.Add("Escanteios esperados", Math.Round(escanteiosEsperadosCasa, 2));
            resultado.ValoresEsperadosTimeVisitante.Add("Escanteios esperados", Math.Round(escanteiosEsperadosVisitante, 2));
            resultado.ValoresEsperadosAgregado.Add("Escanteios totais esperados", Math.Round(escanteiosTotaisEsperados, 2));

            // Calcular probabilidades acumulativas usando Poisson
            // Para time da casa
            for (int i = 0; i <= 15; i++)
            {
                double probabilidade = CalcularProbabilidadePoissonAcumulativa(escanteiosEsperadosCasa, i, int.MaxValue) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesTimeCasa.Add($"Probabilidade de pelo menos {i} escanteios", Math.Round(probabilidade, 2));
                }
            }

            // Para time visitante
            for (int i = 0; i <= 15; i++)
            {
                double probabilidade = CalcularProbabilidadePoissonAcumulativa(escanteiosEsperadosVisitante, i, int.MaxValue) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesTimeVisitante.Add($"Probabilidade de pelo menos {i} escanteios", Math.Round(probabilidade, 2));
                }
            }

            // Para agregado
            for (int i = 0; i <= 20; i++)
            {
                double probabilidade = CalcularProbabilidadePoissonAcumulativa(escanteiosTotaisEsperados, i, int.MaxValue) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesAgregado.Add($"Probabilidade de pelo menos {i} escanteios", Math.Round(probabilidade, 2));
                }
            }

            // Status para time da casa
            if (escanteiosEsperadosCasa > MediasCampeonatoCasa.MediaEscanteiosPorPartida * 1.2)
                resultado.StatusTimeCasa = "Time da casa com muitos escanteios esperados (acima da média do campeonato)";
            else if (escanteiosEsperadosCasa > MediasCampeonatoCasa.MediaEscanteiosPorPartida * 0.8)
                resultado.StatusTimeCasa = "Time da casa com número médio de escanteios esperado (na média do campeonato)";
            else
                resultado.StatusTimeCasa = "Time da casa com poucos escanteios esperados (abaixo da média do campeonato)";

            // Status para time visitante
            if (escanteiosEsperadosVisitante > MediasCampeonatoFora.MediaEscanteiosPorPartida * 1.2)
                resultado.StatusTimeVisitante = "Time visitante com muitos escanteios esperados (acima da média do campeonato)";
            else if (escanteiosEsperadosVisitante > MediasCampeonatoFora.MediaEscanteiosPorPartida * 0.8)
                resultado.StatusTimeVisitante = "Time visitante com número médio de escanteios esperado (na média do campeonato)";
            else
                resultado.StatusTimeVisitante = "Time visitante com poucos escanteios esperados (abaixo da média do campeonato)";

            // Status para agregado
            if (escanteiosTotaisEsperados > MediasCampeonatoTotal.MediaEscanteiosPorPartida * 1.2)
                resultado.StatusAgregado = "Muitos escanteios esperados (acima da média do campeonato)";
            else if (escanteiosTotaisEsperados > MediasCampeonatoTotal.MediaEscanteiosPorPartida * 0.8)
                resultado.StatusAgregado = "Número médio de escanteios esperado (na média do campeonato)";
            else
                resultado.StatusAgregado = "Poucos escanteios esperados (abaixo da média do campeonato)";

            return resultado;
        }

        private ResultadoPrevisao PreverFaltas(List<EstatisticasPartida> jogosCasa, List<EstatisticasPartida> jogosFora)
        {
            var resultado = new ResultadoPrevisao("Faltas");

            double faltasCasa = CalcularMediaPonderadaExponencial(jogosCasa, estatistica => estatistica.Faltas);
            double faltasVisitante = CalcularMediaPonderadaExponencial(jogosFora, estatistica => estatistica.Faltas);

            double faltasTotaisEsperadas = faltasCasa + faltasVisitante;

            resultado.ValoresEsperadosTimeCasa.Add("Faltas esperadas", Math.Round(faltasCasa, 2));
            resultado.ValoresEsperadosTimeVisitante.Add("Faltas esperadas", Math.Round(faltasVisitante, 2));
            resultado.ValoresEsperadosAgregado.Add("Faltas totais esperadas", Math.Round(faltasTotaisEsperadas, 2));

            // Calcular probabilidades acumulativas usando distribuição normal
            double desvioPadrao = 5.0;

            // Para time da casa
            for (int i = 0; i <= 30; i++)
            {
                double probabilidade = CalcularProbabilidadeNormalAcumulativa(faltasCasa, i, int.MaxValue, desvioPadrao) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesTimeCasa.Add($"Probabilidade de pelo menos {i} faltas", Math.Round(probabilidade, 2));
                }
            }

            // Para time visitante
            for (int i = 0; i <= 30; i++)
            {
                double probabilidade = CalcularProbabilidadeNormalAcumulativa(faltasVisitante, i, int.MaxValue, desvioPadrao) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesTimeVisitante.Add($"Probabilidade de pelo menos {i} faltas", Math.Round(probabilidade, 2));
                }
            }

            // Para agregado
            for (int i = 0; i <= 50; i++)
            {
                double probabilidade = CalcularProbabilidadeNormalAcumulativa(faltasTotaisEsperadas, i, int.MaxValue, desvioPadrao) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesAgregado.Add($"Probabilidade de pelo menos {i} faltas", Math.Round(probabilidade, 2));
                }
            }

            // Status para time da casa
            if (faltasCasa > MediasCampeonatoCasa.MediaFaltasPorPartida * 1.2)
                resultado.StatusTimeCasa = "Time da casa com muitas faltas esperadas (acima da média do campeonato)";
            else if (faltasCasa > MediasCampeonatoCasa.MediaFaltasPorPartida * 0.8)
                resultado.StatusTimeCasa = "Time da casa com número médio de faltas esperado (na média do campeonato)";
            else
                resultado.StatusTimeCasa = "Time da casa com poucas faltas esperadas (abaixo da média do campeonato)";

            // Status para time visitante
            if (faltasVisitante > MediasCampeonatoFora.MediaFaltasPorPartida * 1.2)
                resultado.StatusTimeVisitante = "Time visitante com muitas faltas esperadas (acima da média do campeonato)";
            else if (faltasVisitante > MediasCampeonatoFora.MediaFaltasPorPartida * 0.8)
                resultado.StatusTimeVisitante = "Time visitante com número médio de faltas esperado (na média do campeonato)";
            else
                resultado.StatusTimeVisitante = "Time visitante com poucas faltas esperadas (abaixo da média do campeonato)";

            // Status para agregado
            if (faltasTotaisEsperadas > MediasCampeonatoTotal.MediaFaltasPorPartida * 1.2)
                resultado.StatusAgregado = "Jogo com muitas faltas esperadas (acima da média do campeonato)";
            else if (faltasTotaisEsperadas > MediasCampeonatoTotal.MediaFaltasPorPartida * 0.8)
                resultado.StatusAgregado = "Número médio de faltas esperado (na média do campeonato)";
            else
                resultado.StatusAgregado = "Jogo com poucas faltas esperadas (abaixo da média do campeonato)";

            return resultado;
        }

        private ResultadoPrevisao PreverCartoesAmarelos(List<EstatisticasPartida> jogosCasa, List<EstatisticasPartida> jogosFora)
        {
            var resultado = new ResultadoPrevisao("Cartões Amarelos");

            double agressividadeCasa = CalcularAgressividadeTime(jogosCasa);
            double agressividadeVisitante = CalcularAgressividadeTime(jogosFora);

            double lambdaCasa = agressividadeCasa * 1.2;
            double lambdaVisitante = agressividadeVisitante * 0.9;

            double cartoesAmarelosEsperadosCasa = lambdaCasa;
            double cartoesAmarelosEsperadosVisitante = lambdaVisitante;
            double cartoesAmarelosEsperadosAgregado = lambdaCasa + lambdaVisitante;

            resultado.ValoresEsperadosTimeCasa.Add("Cartões amarelos esperados", Math.Round(cartoesAmarelosEsperadosCasa, 2));
            resultado.ValoresEsperadosTimeVisitante.Add("Cartões amarelos esperados", Math.Round(cartoesAmarelosEsperadosVisitante, 2));
            resultado.ValoresEsperadosAgregado.Add("Cartões amarelos totais esperados", Math.Round(cartoesAmarelosEsperadosAgregado, 2));

            // Calcular probabilidades acumulativas usando Poisson
            // Para time da casa
            for (int i = 0; i <= 10; i++)
            {
                double probabilidade = CalcularProbabilidadePoissonAcumulativa(cartoesAmarelosEsperadosCasa, i, int.MaxValue) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesTimeCasa.Add($"Probabilidade de pelo menos {i} cartões amarelos", Math.Round(probabilidade, 2));
                }
            }

            // Para time visitante
            for (int i = 0; i <= 10; i++)
            {
                double probabilidade = CalcularProbabilidadePoissonAcumulativa(cartoesAmarelosEsperadosVisitante, i, int.MaxValue) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesTimeVisitante.Add($"Probabilidade de pelo menos {i} cartões amarelos", Math.Round(probabilidade, 2));
                }
            }

            // Para agregado
            for (int i = 0; i <= 15; i++)
            {
                double probabilidade = CalcularProbabilidadePoissonAcumulativa(cartoesAmarelosEsperadosAgregado, i, int.MaxValue) * 100;
                if (probabilidade >= 0.1)
                {
                    resultado.ProbabilidadesAgregado.Add($"Probabilidade de pelo menos {i} cartões amarelos", Math.Round(probabilidade, 2));
                }
            }

            // Status para time da casa
            if (cartoesAmarelosEsperadosCasa > MediasCampeonatoCasa.MediaCartoesAmarelosPorPartida * 1.2)
                resultado.StatusTimeCasa = "Time da casa com muitos cartões amarelos esperados (acima da média do campeonato)";
            else if (cartoesAmarelosEsperadosCasa > MediasCampeonatoCasa.MediaCartoesAmarelosPorPartida * 0.8)
                resultado.StatusTimeCasa = "Time da casa com alguns cartões amarelos esperados (na média do campeonato)";
            else
                resultado.StatusTimeCasa = "Time da casa com poucos cartões amarelos esperados (abaixo da média do campeonato)";

            // Status para time visitante
            if (cartoesAmarelosEsperadosVisitante > MediasCampeonatoFora.MediaCartoesAmarelosPorPartida * 1.2)
                resultado.StatusTimeVisitante = "Time visitante com muitos cartões amarelos esperados (acima da média do campeonato)";
            else if (cartoesAmarelosEsperadosVisitante > MediasCampeonatoFora.MediaCartoesAmarelosPorPartida * 0.8)
                resultado.StatusTimeVisitante = "Time visitante com alguns cartões amarelos esperados (na média do campeonato)";
            else
                resultado.StatusTimeVisitante = "Time visitante com poucos cartões amarelos esperados (abaixo da média do campeonato)";

            // Status para agregado
            if (cartoesAmarelosEsperadosAgregado > MediasCampeonatoTotal.MediaCartoesAmarelosPorPartida * 1.2)
                resultado.StatusAgregado = "Jogo com muitos cartões amarelos esperados (acima da média do campeonato)";
            else if (cartoesAmarelosEsperadosAgregado > MediasCampeonatoTotal.MediaCartoesAmarelosPorPartida * 0.8)
                resultado.StatusAgregado = "Alguns cartões amarelos esperados (na média do campeonato)";
            else
                resultado.StatusAgregado = "Poucos cartões amarelos esperados (abaixo da média do campeonato)";

            return resultado;
        }

        private ResultadoPrevisao PreverCartoesVermelhos(List<EstatisticasPartida> jogosCasa, List<EstatisticasPartida> jogosFora)
        {
            var resultado = new ResultadoPrevisao("Cartões Vermelhos");

            double taxaCartoesVermelhosCasa = CalcularMediaPonderadaExponencial(jogosCasa, estatistica => estatistica.CartoesVermelhos > 0 ? 1 : 0);
            double taxaCartoesVermelhosVisitante = CalcularMediaPonderadaExponencial(jogosFora, estatistica => estatistica.CartoesVermelhos > 0 ? 1 : 0);

            double agressividadeCasa = CalcularAgressividadeTime(jogosCasa);
            double agressividadeVisitante = CalcularAgressividadeTime(jogosFora);

            double probabilidadeCartaoVermelhoCasa = taxaCartoesVermelhosCasa * agressividadeCasa * 0.7;
            double probabilidadeCartaoVermelhoVisitante = taxaCartoesVermelhosVisitante * agressividadeVisitante * 0.5;

            double probabilidadeTotalCartaoVermelho = probabilidadeCartaoVermelhoCasa + probabilidadeCartaoVermelhoVisitante;

            // Calcular probabilidades para 0, 1, 2+ cartões vermelhos
            resultado.ProbabilidadesTimeCasa.Add("Probabilidade de 0 cartões vermelhos", Math.Round((1 - probabilidadeCartaoVermelhoCasa) * 100, 2));
            resultado.ProbabilidadesTimeCasa.Add("Probabilidade de pelo menos 1 cartão vermelho", Math.Round(probabilidadeCartaoVermelhoCasa * 100, 2));
            resultado.ProbabilidadesTimeCasa.Add("Probabilidade de 2 ou mais cartões vermelhos", Math.Round(probabilidadeCartaoVermelhoCasa * 100 * 0.2, 2));

            resultado.ProbabilidadesTimeVisitante.Add("Probabilidade de 0 cartões vermelhos", Math.Round((1 - probabilidadeCartaoVermelhoVisitante) * 100, 2));
            resultado.ProbabilidadesTimeVisitante.Add("Probabilidade de pelo menos 1 cartão vermelho", Math.Round(probabilidadeCartaoVermelhoVisitante * 100, 2));
            resultado.ProbabilidadesTimeVisitante.Add("Probabilidade de 2 ou mais cartões vermelhos", Math.Round(probabilidadeCartaoVermelhoVisitante * 100 * 0.2, 2));

            resultado.ProbabilidadesAgregado.Add("Probabilidade de 0 cartões vermelhos", Math.Round((1 - probabilidadeTotalCartaoVermelho) * 100, 2));
            resultado.ProbabilidadesAgregado.Add("Probabilidade de pelo menos 1 cartão vermelho", Math.Round(probabilidadeTotalCartaoVermelho * 100, 2));
            resultado.ProbabilidadesAgregado.Add("Probabilidade de 2 ou mais cartões vermelhos", Math.Round(probabilidadeTotalCartaoVermelho * 100 * 0.2, 2));

            resultado.ValoresEsperadosTimeCasa.Add("Probabilidade de cartão vermelho", Math.Round(probabilidadeCartaoVermelhoCasa * 100, 2));
            resultado.ValoresEsperadosTimeVisitante.Add("Probabilidade de cartão vermelho", Math.Round(probabilidadeCartaoVermelhoVisitante * 100, 2));
            resultado.ValoresEsperadosAgregado.Add("Probabilidade de cartão vermelho", Math.Round(probabilidadeTotalCartaoVermelho * 100, 2));

            // Status para time da casa
            if (probabilidadeCartaoVermelhoCasa > MediasCampeonatoCasa.MediaCartoesVermelhosPorPartida * 2)
                resultado.StatusTimeCasa = "Time da casa com alto risco de cartões vermelhos (acima da média do campeonato)";
            else if (probabilidadeCartaoVermelhoCasa > MediasCampeonatoCasa.MediaCartoesVermelhosPorPartida)
                resultado.StatusTimeCasa = "Time da casa com possibilidade de cartão vermelho (na média do campeonato)";
            else
                resultado.StatusTimeCasa = "Time da casa com baixa probabilidade de cartões vermelhos (abaixo da média do campeonato)";

            // Status para time visitante
            if (probabilidadeCartaoVermelhoVisitante > MediasCampeonatoFora.MediaCartoesVermelhosPorPartida * 2)
                resultado.StatusTimeVisitante = "Time visitante com alto risco de cartões vermelhos (acima da média do campeonato)";
            else if (probabilidadeCartaoVermelhoVisitante > MediasCampeonatoFora.MediaCartoesVermelhosPorPartida)
                resultado.StatusTimeVisitante = "Time visitante com possibilidade de cartão vermelho (na média do campeonato)";
            else
                resultado.StatusTimeVisitante = "Time visitante com baixa probabilidade de cartões vermelhos (abaixo da média do campeonato)";

            // Status para agregado
            if (probabilidadeTotalCartaoVermelho > MediasCampeonatoTotal.MediaCartoesVermelhosPorPartida * 2)
                resultado.StatusAgregado = "Alto risco de cartões vermelhos (acima da média do campeonato)";
            else if (probabilidadeTotalCartaoVermelho > MediasCampeonatoTotal.MediaCartoesVermelhosPorPartida)
                resultado.StatusAgregado = "Possibilidade de cartão vermelho (na média do campeonato)";
            else
                resultado.StatusAgregado = "Baixa probabilidade de cartões vermelhos (abaixo da média do campeonato)";

            return resultado;
        }

        #region Métodos Estatísticos Avançados

        private ForcaTime CalcularForcaOfensivaPonderada(List<EstatisticasPartida> partidas)
        {
            var partidasRecentes = partidas.Take(NumeroJogosAnalisar).ToList();
            if (!partidasRecentes.Any()) return new ForcaTime();

            double pesoTotal = 0;
            var forca = new ForcaTime();

            for (int i = 0; i < partidasRecentes.Count; i++)
            {
                double peso = Math.Pow(0.8, i);
                var partida = partidasRecentes[i];

                forca.Gols += partida.GolsTotais * peso;
                forca.GolsPrimeiroTempo += partida.GolsPrimeiroTempo * peso;
                forca.Chutes += partida.ChutesTotais * peso;
                forca.ChutesNoAlvo += partida.ChutesNoAlvo * peso;
                forca.Escanteios += partida.Escanteios * peso;
                forca.Faltas += partida.Faltas * peso;
                forca.CartoesAmarelos += partida.CartoesAmarelos * peso;
                forca.CartoesVermelhos += partida.CartoesVermelhos * peso;

                pesoTotal += peso;
            }

            forca.Gols /= pesoTotal;
            forca.GolsPrimeiroTempo /= pesoTotal;
            forca.Chutes /= pesoTotal;
            forca.ChutesNoAlvo /= pesoTotal;
            forca.Escanteios /= pesoTotal;
            forca.Faltas /= pesoTotal;
            forca.CartoesAmarelos /= pesoTotal;
            forca.CartoesVermelhos /= pesoTotal;

            return forca;
        }

        private ForcaTime CalcularForcaDefensivaPonderada(List<EstatisticasPartida> partidas)
        {
            var partidasRecentes = partidas.Take(NumeroJogosAnalisar).ToList();
            if (!partidasRecentes.Any()) return new ForcaTime();

            double pesoTotal = 0;
            var forca = new ForcaTime();

            for (int i = 0; i < partidasRecentes.Count; i++)
            {
                double peso = Math.Pow(0.8, i);
                var partida = partidasRecentes[i];

                forca.Gols += partida.GolsTotais * peso;
                forca.GolsPrimeiroTempo += partida.GolsPrimeiroTempo * peso;
                forca.Chutes += partida.ChutesTotais * peso;
                forca.ChutesNoAlvo += partida.ChutesNoAlvo * peso;

                pesoTotal += peso;
            }

            forca.Gols /= pesoTotal;
            forca.GolsPrimeiroTempo /= pesoTotal;
            forca.Chutes /= pesoTotal;
            forca.ChutesNoAlvo /= pesoTotal;

            return forca;
        }

        private double CalcularMediaPonderadaExponencial(List<EstatisticasPartida> partidas, Func<EstatisticasPartida, double> seletorValor)
        {
            var partidasRecentes = partidas.Take(NumeroJogosAnalisar).ToList();
            if (!partidasRecentes.Any()) return 0;

            double pesoTotal = 0;
            double somaPonderada = 0;

            for (int i = 0; i < partidasRecentes.Count; i++)
            {
                double peso = Math.Pow(0.7, i);
                somaPonderada += seletorValor(partidasRecentes[i]) * peso;
                pesoTotal += peso;
            }

            return somaPonderada / pesoTotal;
        }

        private double CalcularAgressividadeTime(List<EstatisticasPartida> partidas)
        {
            var partidasRecentes = partidas.Take(NumeroJogosAnalisar).ToList();
            if (!partidasRecentes.Any()) return 0.5;

            double faltas = CalcularMediaPonderadaExponencial(partidasRecentes, partida => partida.Faltas);
            double cartoesAmarelos = CalcularMediaPonderadaExponencial(partidasRecentes, partida => partida.CartoesAmarelos);
            double cartoesVermelhos = CalcularMediaPonderadaExponencial(partidasRecentes, partida => partida.CartoesVermelhos);

            return (faltas * 0.5 + cartoesAmarelos * 0.3 + cartoesVermelhos * 0.2) / 20;
        }

        private double CalcularProbabilidadeLogistica(double entrada, double vies)
        {
            return 1 / (1 + Math.Exp(-(entrada + vies)));
        }

        private double CalcularProbabilidadePoissonAcumulativa(double lambda, int inicio, int fim)
        {
            double probabilidade = 0;
            for (int k = inicio; k <= fim; k++)
            {
                probabilidade += (Math.Pow(lambda, k) * Math.Exp(-lambda)) / Fatorial(k);
            }
            return probabilidade;
        }

        private double CalcularProbabilidadeNormalAcumulativa(double media, int inicio, int fim, double desvioPadrao)
        {
            double zInicio = (inicio - 0.5 - media) / desvioPadrao;
            double zFim = (fim + 0.5 - media) / desvioPadrao;

            double probabilidadeInicio = 1.0 / (1.0 + Math.Exp(-1.7 * zInicio));
            double probabilidadeFim = 1.0 / (1.0 + Math.Exp(-1.7 * zFim));

            return Math.Abs(probabilidadeFim - probabilidadeInicio);
        }

        private int Fatorial(int n)
        {
            if (n <= 1) return 1;
            return n * Fatorial(n - 1);
        }

        #endregion
    }

    //class Program
    //{
    //    static void Main(string[] args)
    //    {
    //        // Exemplo de uso
    //        Time timeCasa = new("Time da Casa");
    //        Time timeVisitante = new("Time Visitante");

    //        // Definir número de jogos recentes a serem considerados
    //        int numeroJogosAnalisar = 5;

    //        // Definir médias do campeonato
    //        MediasCampeonato mediasCampeonato = new MediasCampeonato
    //        {
    //            MediaGolsPorPartida = 2.5,
    //            MediaGolsPrimeiroTempoPorPartida = 1.1,
    //            MediaChutesPorPartida = 20.0,
    //            MediaChutesNoAlvoPorPartida = 7.5,
    //            MediaEscanteiosPorPartida = 9.0,
    //            MediaFaltasPorPartida = 22.0,
    //            MediaCartoesAmarelosPorPartida = 3.2,
    //            MediaCartoesVermelhosPorPartida = 0.2
    //        };

    //        // Adicionar dados históricos dos últimos X jogos em casa para o time da casa
    //        for (int i = 0; i < numeroJogosAnalisar; i++)
    //        {
    //            timeCasa.UltimosJogosCasa.Add(new EstatisticasPartida
    //            {
    //                JogoEmCasa = true,
    //                GolsPrimeiroTempo = i < 3 ? 1 : 0,
    //                GolsSegundoTempo = i < 2 ? 1 : 2,
    //                ChutesTotais = 12 + i,
    //                ChutesNoAlvo = 5 + i / 2,
    //                Escanteios = 6 + i,
    //                Faltas = 15 + i,
    //                CartoesAmarelos = i < 4 ? 2 : 1,
    //                CartoesVermelhos = 0
    //            });
    //        }

    //        // Adicionar dados históricos dos últimos X jogos fora para o time visitante
    //        for (int i = 0; i < numeroJogosAnalisar; i++)
    //        {
    //            timeVisitante.UltimosJogosFora.Add(new EstatisticasPartida
    //            {
    //                JogoEmCasa = false,
    //                GolsPrimeiroTempo = i < 2 ? 1 : 0,
    //                GolsSegundoTempo = i < 3 ? 1 : 0,
    //                ChutesTotais = 10 - i,
    //                ChutesNoAlvo = 4 - i / 2,
    //                Escanteios = 4,
    //                Faltas = 18 + i,
    //                CartoesAmarelos = i < 3 ? 2 : 3,
    //                CartoesVermelhos = i == 0 ? 1 : 0
    //            });
    //        }

    //        PreditorFutebolAvancado preditor = new PreditorFutebolAvancado(timeCasa, timeVisitante, numeroJogosAnalisar, mediasCampeonato, 1.2);
    //        var previsoes = preditor.PreverTodasEstatisticas();

    //        Console.WriteLine("Previsões Avançadas para o jogo:");
    //        Console.WriteLine($"{timeCasa.Nome} vs {timeVisitante.Nome}");
    //        Console.WriteLine($"Considerando os últimos {numeroJogosAnalisar} jogos");
    //        Console.WriteLine("==========================================");

    //        foreach (var previsao in previsoes)
    //        {
    //            Console.WriteLine($"\n{previsao.Estatistica}:");

    //            if (previsao.ProbabilidadesTimeCasa.Any())
    //            {
    //                Console.WriteLine("\nTime da Casa:");
    //                foreach (var probabilidade in previsao.ProbabilidadesTimeCasa)
    //                {
    //                    Console.WriteLine($"{probabilidade.Key}: {probabilidade.Value}%");
    //                }
    //                foreach (var valorEsperado in previsao.ValoresEsperadosTimeCasa)
    //                {
    //                    Console.WriteLine($"{valorEsperado.Key}: {valorEsperado.Value}");
    //                }
    //                Console.WriteLine($"Status: {previsao.StatusTimeCasa}");
    //            }

    //            if (previsao.ProbabilidadesTimeVisitante.Any())
    //            {
    //                Console.WriteLine("\nTime Visitante:");
    //                foreach (var probabilidade in previsao.ProbabilidadesTimeVisitante)
    //                {
    //                    Console.WriteLine($"{probabilidade.Key}: {probabilidade.Value}%");
    //                }
    //                foreach (var valorEsperado in previsao.ValoresEsperadosTimeVisitante)
    //                {
    //                    Console.WriteLine($"{valorEsperado.Key}: {valorEsperado.Value}");
    //                }
    //                Console.WriteLine($"Status: {previsao.StatusTimeVisitante}");
    //            }

    //            if (previsao.ProbabilidadesAgregado.Any())
    //            {
    //                Console.WriteLine("\nAgregado:");
    //                foreach (var probabilidade in previsao.ProbabilidadesAgregado)
    //                {
    //                    Console.WriteLine($"{probabilidade.Key}: {probabilidade.Value}%");
    //                }
    //                foreach (var valorEsperado in previsao.ValoresEsperadosAgregado)
    //                {
    //                    Console.WriteLine($"{valorEsperado.Key}: {valorEsperado.Value}");
    //                }
    //                Console.WriteLine($"Status: {previsao.StatusAgregado}");
    //            }

    //            Console.WriteLine("\n" + new string('-', 50));
    //        }

    //        Console.WriteLine("\nAnálise Completa Finalizada!");
    //    }
    //}
}