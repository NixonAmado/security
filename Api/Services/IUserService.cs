using Api.Dtos;

namespace Api.Services
{
    public interface IUserService
    {
        Task<string> RegisterAsync(RegisterDto registerDto);
        Task<DataUserDto> GetTokenAsync(LoginDto loginDto);
        Task<string> AddRoleAsync(AddRoleDto addRoleDto);
        Task<DataUserDto> RefreshTokenAsync(string refreshToken);
        string EncryptCookie(string cookie);
        string DecryptCookie(string encryptedCookie);
    }
}
