using SecretSanta.API.Data.Context;
using SecretSanta.API.Data.Repositories.Interfaces;
using SecretSanta.API.Domain.Models;

namespace SecretSanta.API.Data.Repositories;

public class ParticipantRepository(ApplicationContext context) : BaseRepository<Participant>(context), IParticipantRepository;