using SecretSanta.API.Data.Context;
using SecretSanta.API.Data.Repositories.Interfaces;
using SecretSanta.API.Domain.Models;

namespace SecretSanta.API.Data.Repositories;

public class GroupRepository(ApplicationContext context) : BaseRepository<Group>(context), IGroupRepository;