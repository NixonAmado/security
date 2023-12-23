using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Api.Dtos;
using Api.Helpers;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Api.Services
{
    public class UserService : IUserService
    {
        private readonly JWT _jwt;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IDataProtectionProvider _dataProtectionProvider;

        public UserService(
            IUnitOfWork unitOfWork,
            IOptions<JWT> jwt,
            IPasswordHasher<User> passwordHasher,
            IDataProtectionProvider dataProtectionProvider
        )
        {
            _jwt = jwt.Value;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _dataProtectionProvider = dataProtectionProvider;
        }

        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            var user = new User { Username = registerDto.Username, Email = registerDto.Email };

            user.Password = _passwordHasher.HashPassword(user, registerDto.Password);

            var existingUser = _unitOfWork
                .Users
                .Find(u => u.Username.ToLower() == registerDto.Username.ToLower())
                .FirstOrDefault();

            if (existingUser == null)
            {
                var rolDefault = _unitOfWork
                    .Roles
                    .Find(u => u.Name == Authorization.rol_default.ToString())
                    .First();
                try
                {
                    user.Rols.Add(rolDefault);
                    _unitOfWork.Users.Add(user);
                    await _unitOfWork.SaveAsync();

                    return $"User {registerDto.Username} has been registered successfully";
                }
                catch (Exception ex)
                {
                    var message = ex.Message;
                    return $"Error: {message}";
                }
            }
            else
            {
                return $"User {registerDto.Username} already registered.";
            }
        }

        public async Task<DataUserDto> GetTokenAsync(LoginDto loginDto)
        {
            DataUserDto dataUserDto = new DataUserDto();
            var user = await _unitOfWork.Users.GetByUsernameAsync(loginDto.Username);

            if (user == null)
            {
                dataUserDto.IsAuthenticated = false;
                dataUserDto.Message = $"User does not exist with Name {loginDto.Username}.";
                return dataUserDto;
            }

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                loginDto.Password
            );

            if (result == PasswordVerificationResult.Success)
            {
                dataUserDto.Message = $"User {user.Username} is authenticated.";
                dataUserDto.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = CreateJwtToken(user);
                dataUserDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                dataUserDto.Username = user.Username;
                dataUserDto.Email = user.Email;
                dataUserDto.Roles = user.Rols.Select(u => u.Name).ToList();

                if (user.RefreshTokens.Any(a => a.IsActive))
                {
                    var activeRefreshToken = user.RefreshTokens
                        .Where(a => a.IsActive == true)
                        .FirstOrDefault();
                    dataUserDto.RefreshToken = activeRefreshToken.Token;
                    dataUserDto.RefreshTokenExpiration = activeRefreshToken.Expires;
                }
                else
                {
                    var refreshToken = CreateRefreshToken();
                    dataUserDto.RefreshToken = refreshToken.Token;
                    dataUserDto.RefreshTokenExpiration = refreshToken.Expires;
                    user.RefreshTokens.Add(refreshToken);
                    _unitOfWork.Users.Update(user);
                    await _unitOfWork.SaveAsync();
                }

                return dataUserDto;
            }
            dataUserDto.IsAuthenticated = false;
            dataUserDto.Message = $"Credentials incorrect for the user {user.Username}.";
            return dataUserDto;
        }

        public async Task<string> AddRoleAsync(AddRoleDto addRoleDto)
        {
            var user = await _unitOfWork.Users.GetByUsernameAsync(addRoleDto.Username);
            if (user == null)
            {
                return $"User {addRoleDto.Username} does not exists.";
            }

            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                addRoleDto.Password
            );

            if (result == PasswordVerificationResult.Success)
            {
                var rolExists = _unitOfWork
                    .Roles
                    .Find(u => u.Name.ToLower() == addRoleDto.Role.ToLower())
                    .FirstOrDefault();

                if (rolExists != null)
                {
                    var userHasRole = user.Rols.Any(u => u.Id == rolExists.Id);

                    if (userHasRole == false)
                    {
                        user.Rols.Add(rolExists);
                        _unitOfWork.Users.Update(user);
                        await _unitOfWork.SaveAsync();
                    }

                    return $"Role {addRoleDto.Role} added to user {addRoleDto.Username} successfully.";
                }

                return $"Role {addRoleDto.Role} was not found.";
            }
            return $"Invalid Credentials";
        }

        public async Task<DataUserDto> RefreshTokenAsync(string refreshToken)
        {
            var dataUserDto = new DataUserDto();

            var User = await _unitOfWork.Users.GetByRefreshTokenAsync(refreshToken);

            if (User == null)
            {
                dataUserDto.IsAuthenticated = false;
                dataUserDto.Message = $"Token is not assigned to any user.";
                return dataUserDto;
            }

            var refreshTokenBd = User.RefreshTokens.Single(x => x.Token == refreshToken);

            if (!refreshTokenBd.IsActive)
            {
                dataUserDto.IsAuthenticated = false;
                dataUserDto.Message = $"Token is not active.";
                return dataUserDto;
            }
            refreshTokenBd.Revoked = DateTime.UtcNow;
            var newRefreshToken = CreateRefreshToken();
            User.RefreshTokens.Add(newRefreshToken);
            _unitOfWork.Users.Update(User);
            await _unitOfWork.SaveAsync();
            dataUserDto.Message = $"User {User.Username} is authenticated.";
            dataUserDto.IsAuthenticated = true;
            JwtSecurityToken jwtSecurityToken = CreateJwtToken(User);
            dataUserDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
            dataUserDto.Username = User.Username;
            dataUserDto.Email = User.Email;
            dataUserDto.Roles = User.Rols.Select(u => u.Name).ToList();
            dataUserDto.RefreshToken = newRefreshToken.Token;
            dataUserDto.RefreshTokenExpiration = newRefreshToken.Expires;
            return dataUserDto;
        }

        private RefreshToken CreateRefreshToken()
        {
            var randomNumber = new byte[32];
            using (var generator = RandomNumberGenerator.Create())
            {
                generator.GetBytes(randomNumber);
                return new RefreshToken
                {
                    Token = Convert.ToBase64String(randomNumber),
                    Expires = DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutesRefreshToken),
                    Created = DateTime.UtcNow
                };
            }
        }

        private JwtSecurityToken CreateJwtToken(User User)
        {
            var roles = User.Rols;
            var roleClaims = new List<Claim>();
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role.Name));
            }
            var claims = new[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, User.Username),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, User.Email),
                new Claim("uid", User.Id.ToString())
            }.Union(roleClaims);
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(
                symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256
            );
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials
            );
            return jwtSecurityToken;
        }

        public string EncryptCookie(string cookie)
        {
            var protector = _dataProtectionProvider.CreateProtector("SecurityCookie");
            return protector.Protect(cookie);
        }

        public string DecryptCookie(string encryptedCookie)
        {
            var protector = _dataProtectionProvider.CreateProtector("SecurityCookie");
            return protector.Unprotect(encryptedCookie);
        }
    }
}
