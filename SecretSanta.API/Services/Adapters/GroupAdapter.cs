using SecretSanta.API.Domain.Models;
using SecretSanta.API.Domain.Requests;
using SecretSanta.API.Domain.Responses;

namespace SecretSanta.API.Services.Adapters
{
    public static class GroupAdapter
    {
        public static Group ToDomain(GroupRequest request) => new(request.Name);

        public static GroupResponse FromDomain(Group group, List<MatchResponse?> matches)
            => new()
            {
                Id = group.Id,
                Name = group.Name,
                ParticipantsNames = group.Participants.Select(x => x.Name).ToList(),
                Matches = matches
            };
    }
}