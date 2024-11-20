using System.Text.Json.Serialization;

namespace SecretSanta.API.Domain.Requests;

public class ParticipantRequest
{
    [JsonIgnore]
    public int GroupId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string ConfirmPassword { get; set; } = string.Empty;
}