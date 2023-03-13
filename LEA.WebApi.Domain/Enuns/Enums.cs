namespace LEA.WebApi.Domain.Enuns
{
    public enum Country
    {
        England = 1,
        Germany = 2,
        Italy = 3,
        Netherlands = 4,
        Spain = 5,
        France = 6,
        Portugal,
        Undefined = -1
    }
    public enum Scoreboard
    {
        Win = 1,
        Lost = 2,
        Draw = 3
    }

    public enum StatisticKind
    {
        GoalsFullTime = 0,
        GoalsHalfTime = 1,
        Shots=  2,
        ShotsOnTarget = 3,
        Corners = 4,
        Fols=5,
        Yellow= 6,
        Red= 7
    }
}
