namespace LEA.WebApi.Domain.Models
{
    public class ExpectedStat
    {
        public double Home { get; set; }
        public double Away { get; set; }
        public double Total => Home + Away;
    }
}
