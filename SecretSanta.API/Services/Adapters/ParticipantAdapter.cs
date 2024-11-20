using SecretSanta.API.Domain.Models;
using SecretSanta.API.Domain.Requests;

namespace SecretSanta.API.Services.Adapters
{
    public static class ParticipantAdapter
    {
        public static Participant ToDomain(ParticipantRequest param, int groupId)
            => new(
                param.Name,
                param.Email,
                param.Password,
                groupId);
    }
}