using SecretSanta.API.Data.Repositories.Interfaces;
using SecretSanta.API.Domain.Models;
using SecretSanta.API.Domain.Requests;
using SecretSanta.API.Domain.Responses;
using SecretSanta.API.Services.Adapters;
using SecretSanta.API.Services.Interfaces;
using System.Linq.Expressions;

namespace SecretSanta.API.Services;

public class GroupService(
    IGroupRepository groupRepository,
    IMatchService matchService) : IGroupService
{
    public async Task<int?> CreateGroupAsync(GroupRequest request)
    {
        var group = GroupAdapter.ToDomain(request);

        return await groupRepository.Add(group);
    }
    
    public async Task<GroupResponse?> GetById(int id)
    {
        var includeProps = new List<Expression<Func<Group, object>>>()
        {
            x => x.Participants,
            x => x.Matches
        };
        
        var group = await groupRepository.GetById(id, includeProps);

        if (group is null)
            return default;

        var matches = group.Matches.Select(x => matchService.GetMatchAsync(x.GiverId).Result).ToList();

        return GroupAdapter.FromDomain(group, matches);
    }
}