using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SecretSanta.API.Domain.Models;

public class Match(int giverId, int receiverId, int groupId) : BaseEntity
{
    public int GiverId { get; private set; } = giverId;
    public int ReceiverId { get; private set; } = receiverId;
    public int GroupId { get; private set; } = groupId;

    public virtual Group Group { get; private set; } = null!;
}