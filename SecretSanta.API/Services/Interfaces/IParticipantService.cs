using SecretSanta.API.Domain.Requests;

namespace SecretSanta.API.Services.Interfaces;

public interface IParticipantService
{
    Task<int?> CreateParticipantAsync(int groupId, ParticipantRequest request);
}