namespace LEA.WebApi.Domain.Models
{
    public class PredictionPair
    {
        public double Home { get; }
        public double Away { get; }
        public double Total => Home + Away;

        public PredictionPair(double home, double away)
        {
            Home = home;
            Away = away;
        }
    }
}
