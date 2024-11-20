using System.Linq.Expressions;
using SecretSanta.API.Data.Context;
using SecretSanta.API.Data.Repositories.Interfaces;
using SecretSanta.API.Domain.Models;
using SecretSanta.API.Domain.Responses;
using SecretSanta.API.Services.Interfaces;

namespace SecretSanta.API.Services;

public class MatchService(
    ApplicationContext applicationContext,
    IMatchRepository matchRepository,
    IGroupRepository groupRepository,
    IParticipantRepository participantRepository) : IMatchService
{
    public async Task<string> GenerateMatchesByGroupIdAsync(int groupId)
    {
        var includeProps = new List<Expression<Func<Group, object>>>()
        {
            x => x.Participants,
            x => x.Matches
        };
        
        var group = await groupRepository.GetSingle(x => x.Id == groupId, includeProps);

        if (group is null)
            return string.Empty;

        if (group.Participants.Count < 3 || group.Matches.Count is not 0)
            return string.Empty;

        try
        {
            await GenerateMatches(group.Id, group.Participants.Select(x => x.Id).ToList());
            await applicationContext.SaveChangesAsync();
        }
        catch (Exception e)
        {
            return e.Message;
        }

        return "Matches added successfully";
    }

    public async Task<MatchResponse?> GetMatchAsync(int giverId)
    {
        var match = await matchRepository.GetSingle(x => x.GiverId == giverId);

        if (match is null)
            return default;
        
        return new MatchResponse
        {
            GiverName = (await participantRepository.GetById(match.GiverId))!.Name,
            ReceiverName = (await participantRepository.GetById(match.ReceiverId))!.Name
        };
    }

    private Task GenerateMatches(int groupId, List<int> participantIds)
    {
        var receiversIds = new List<int>(participantIds);
        var random = new Random();

        foreach (var giverId in participantIds)
        {
            var validReceivers = receiversIds.Where(r => r != giverId).ToList();

            if (validReceivers.Count <= 0)
                return GenerateMatches(groupId, participantIds);

            var receiverId = validReceivers[random.Next(validReceivers.Count)];
            
            applicationContext.Add(new Match(giverId, receiverId, groupId));

            receiversIds.Remove(receiverId);
        }

        return Task.CompletedTask;
    }
}