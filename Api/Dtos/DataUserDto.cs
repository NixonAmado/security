using System.Text.Json.Serialization;

namespace Api.Dtos;

public class DataUserDto
{
    public string Message { get; set; }
    public bool IsAuthenticated { get; set; }
    public string Username { get; set; }
    public string Email { get; set; }
    public List<string> Roles { get; set; }
    public string Token { get; set; }

    [JsonIgnore]
    public string RefreshToken { get; set; }
    public DateTime RefreshTokenExpiration { get; set; }
}
