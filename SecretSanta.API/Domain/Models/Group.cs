using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecretSanta.API.Domain.Models;

public class Group(string name) : BaseEntity
{
    public string Name { get; private set; } = name;
    
    public virtual ICollection<Participant> Participants { get; private set; } = [];
    public virtual ICollection<Match> Matches { get; private set; } = [];
}