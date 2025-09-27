namespace LEA.WebApi.Service.ViewModel
{
    public record DadosPrevisaoPartidaViewModel(
    string HomeTeamName,
    string AwayTeamName,
    int HomeTeamId,
    int AwayTeamId,
    int MatchCount);
}
