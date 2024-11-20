using SecretSanta.API.Domain.Requests;
using SecretSanta.API.Domain.Responses;

namespace SecretSanta.API.Services.Interfaces;

public interface IMatchService
{
    Task<string> GenerateMatchesByGroupIdAsync(int groupId);
    Task<MatchResponse?> GetMatchAsync(int giverId);
}