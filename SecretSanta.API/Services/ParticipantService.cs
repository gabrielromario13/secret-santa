using SecretSanta.API.Data.Repositories.Interfaces;
using SecretSanta.API.Domain.Requests;
using SecretSanta.API.Services.Adapters;
using SecretSanta.API.Services.Interfaces;

namespace SecretSanta.API.Services;

public class ParticipantService(
    IParticipantRepository participantRepository,
    IGroupRepository groupRepository) : IParticipantService
{
    public async Task<int?> CreateParticipantAsync(int groupId, ParticipantRequest request)
    {
        var emailExists = (await participantRepository.Get(p => p.Email == request.Email)).Any();
        if (emailExists)
            return null;
        
        var group = await groupRepository.GetById(groupId);
        if (group is null)
            return null;
        
        var participant = ParticipantAdapter.ToDomain(request, groupId);

        return await participantRepository.Add(participant);
    }
}