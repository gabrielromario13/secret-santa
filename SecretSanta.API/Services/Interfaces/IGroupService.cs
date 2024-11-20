using SecretSanta.API.Domain.Requests;
using SecretSanta.API.Domain.Responses;

namespace SecretSanta.API.Services.Interfaces;

public interface IGroupService
{
    Task<int?> CreateGroupAsync(GroupRequest request);
    Task<GroupResponse?> GetById(int id);
}