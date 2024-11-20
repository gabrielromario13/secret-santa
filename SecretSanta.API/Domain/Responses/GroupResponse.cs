using SecretSanta.API.Domain.Models;

namespace SecretSanta.API.Domain.Responses;

public class GroupResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public List<string> ParticipantsNames { get; set; } = [];
    public List<MatchResponse?> Matches { get; set; } = [];
}