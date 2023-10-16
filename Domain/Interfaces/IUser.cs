using Domain.Entities;
using Domain.Interfaces;

namespace Domain.Interfaces
{
    public interface IUser : IGeneric<User>
    {
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByRefreshTokenAsync(string refreshToken);
    }
}
