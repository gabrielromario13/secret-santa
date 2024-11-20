namespace SecretSanta.API.Domain.Models;

public class Participant(string name, string email, string password, int groupId) : BaseEntity
{
    public string Name { get; private set; } = name;
    public string Email { get; private set; } = email;
    public string Password { get; private set; } = password;
    public int GroupId { get; private set; } = groupId;

    public virtual Group Group { get; private set; } = null!;
}