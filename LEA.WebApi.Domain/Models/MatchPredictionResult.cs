using LEA.WebApi.Domain.Enuns;

namespace LEA.WebApi.Domain.Models
{
    public class MatchPredictionResult
    {
        public PredictionPair Goals { get; set; }
        public PredictionPair Shots { get; set; }
        public PredictionPair ShotsOnTarget { get; set; }
        public PredictionPair Corners { get; set; }
        public PredictionPair Fouls { get; set; }
        public PredictionPair YellowCards { get; set; }
        public PredictionPair RedCards { get; set; }

        public double Over25Probability { get; set; }
        public double BttsProbability { get; set; }
    }
}
