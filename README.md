# JWT (Json Web Token)


## Depencencias para el uso de JWT
   
    <PackageReference Include="Microsoft.AspNetCore.Authentication.JwtBearer" Version="8.0.1" />
    <PackageReference Include="AutoMapper.Extensions.Microsoft.DependencyInjection" Version="12.0.1" />
    <PackageReference Include="Microsoft.EntityFrameworkCore.Design" Version="8.0.1">
    <PackageReference Include="System.IdentityModel.Tokens.Jwt" Version="7.2.0" />

## Configuración inmutable de Jwt
Para esta acción debemos dirigirnos al archivo de configuración del proyecto __(que separa la información sensible y que NO debe ser expuesta)__, dependiendo del entorno en el que estemos  utilizaremos  appSettings.json ó appsettings.Development.json 

Estando en el archivo de configuración pondremos la configuración  relacionada con JWT, con la que después podremos acceder mediante Iconfiguration
```
  "JWT": {
    // clave privada que se usará para generar la firma que validará el token 
    "Key": "njMCY^UbEskeAFL6eDzHuqY!s^x6Qrwe", 
    
    // entidad que genera y firma el token
    "Issuer": "MyStoreApi", 
    
    // entidad que debe recibir y validar el token.
    "Audience": "MyStoreApiUser",
    
    // duración del token en minutos 
    "DurationInMinutes": 30, 
    
    // duración del refresh token en minutos
    "DurationInMinutesRefreshToken": 10080, 
    
    // duración de la cookie en minutos
    "DurationInMinutesCookie": 10079
  }
```

## Creación del la entidad JWT dentro de Helpers
Esta entidad se encargará de representar los datos previamente almacenados en el archivo de configuración
```
namespace Api.Helpers
{
    public class JWT
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }
        public double DurationInMinutesRefreshToken { get; set; }
        public double DurationInMinutesCookie { get; set; }
    }
}

```

## Creación del metodo de configuración del JWT
Para la creación debemos dirigirnos a la carpeta Extensions > ApplicationServiceExtension __( que contiene la configuración de la mayoria de metodos asociados al servicio: cors, versioning, ratelimit )__ y aplicar la configuracion del JWT

__IConfiguration (configuration):__  Contiene la sección JWT archivo appsettings.json que se configuró previamente

```
    public static void AddJwt(this IServiceCollection services, IConfiguration configuration)
    {
        //asigna los valores del json a la instancia de clase helper JWT
        services.Configure<JWT>(configuration.GetSection("JWT"));

        // Configuración de autenticación usando JWT (Json Web Token)
        services.AddAuthentication(options =>
        {
            // Selecciona el esquema de autenticación por defecto como JwtBearer
            options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
            options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        // Configuración del proveedor de autenticación JwtBearer
        .AddJwtBearer(o =>
        {
            // Desactiva la validación de HTTPS para simplificar en entornos locales o sin SSL
            o.RequireHttpsMetadata = false;

            // No almacena el token en la propiedad HttpContext.User.Identity
            o.SaveToken = false;

            // Configuración de parámetros de validación del token
            o.TokenValidationParameters = new TokenValidationParameters
            {
                // Habilita la validación de la clave de firma del emisor
                ValidateIssuerSigningKey = true,

                // Habilita la validación del emisor (issuer) del token
                ValidateIssuer = true,

                // Habilita la validación del destinatario (audience) del token
                ValidateAudience = true,

                // Habilita la validación del tiempo de vida del token
                ValidateLifetime = true,

                // Sin margen de tiempo adicional para la expiración del token
                ClockSkew = TimeSpan.Zero,

                // Configuración de valores válidos para el emisor y destinatario del token
                ValidIssuer = configuration["JWT:Issuer"],
                ValidAudience = configuration["JWT:Audience"],

                // Clave de firma simétrica obtenida desde la configuración
                IssuerSigningKey = new SymmetricSecurityKey(
                    Encoding.UTF8.GetBytes(configuration["JWT:Key"])
                )
            };
        });
    }
```




## Registrar el servicio en la aplicación
Para agregar la configuración previa a la aplicacion, nos dirigimos al archivo de inyección de dependencias __( Program.cs )__
```
builder.Services.AddJwt(builder.Configuration);
```
 es una forma de agregar la autenticación basada en tokens JWT (JSON Web Tokens) a la aplicación web ASP.NET Core

 NOTA: Es importante recalcar que todas los servicios o conque queramos añadir al inyector de dependecias deben estan por encima de var __app = builder.Build();__ para evitar problemas de hoisting



## Creacion de Dtos para casos de uso especificos

#### AddRoleDto
```
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    // Definición del espacio de nombres y declaración del objeto AddRoleDto
    public class AddRoleDto
    {
        // Atributo requerido para el nombre de usuario
        [Required]
        public string Username { get; set; }

        // Atributo requerido para la contraseña
        [Required]
        public string Password { get; set; }

        // Atributo requerido para el rol
        [Required]
        public string Role { get; set; }
    }
}
```
#### DataUserDto
```
using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Api.Dtos
{
    // Definición del espacio de nombres y declaración del objeto DataUserDto
    public class DataUserDto
    {
        // Propiedad para almacenar mensajes relacionados con la autenticación
        public string Message { get; set; }

        // Propiedad que indica si el usuario está autenticado
        public bool IsAuthenticated { get; set; }

        // Propiedad para almacenar el nombre de usuario
        public string Username { get; set; }

        // Propiedad para almacenar la dirección de correo electrónico
        public string Email { get; set; }

        // Propiedad para almacenar la lista de roles del usuario
        public List<string> Roles { get; set; }

        // Propiedad para almacenar el token de autenticación
        public string Token { get; set; }

        // Propiedad que representa el token de actualización, ignorada en la serialización JSON
        [JsonIgnore]
        public string RefreshToken { get; set; }

        // Propiedad para almacenar la fecha de expiración del token de actualización
        public DateTime RefreshTokenExpiration { get; set; }
    }
}

```
#### Login Dto
```
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    // Definición del espacio de nombres y declaración del objeto LoginDto
    public class LoginDto
    {
        // Atributo requerido para el nombre de usuario
        [Required]
        public string Username { get; set; }

        // Atributo requerido para la contraseña
        [Required]
        public string Password { get; set; }
    }
}

```

#### Register Dto
```
using System.ComponentModel.DataAnnotations;

namespace Api.Dtos
{
    // Definición del espacio de nombres y declaración del objeto RegisterDto
    public class RegisterDto
    {
        // Atributo requerido para la dirección de correo electrónico
        [Required]
        public string Email { get; set; }

        // Atributo requerido para el nombre de usuario
        [Required]
        public string Username { get; set; }

        // Atributo requerido para la contraseña
        [Required]
        public string Password { get; set; }
    }
}

```


## Creacion del microservicio UserService
Dentro de la carpeta services, creamos un archivo IUserService,
que será el contrato que UserService.cs tendrá que cumplir.

__Archivo IUserService.cs__
```
using Api.Dtos;

namespace Api.Services
{
    // Definición de la interfaz IUserService, que proporciona operaciones relacionadas con la gestión de usuarios.
    public interface IUserService
    {
        // Método para registrar un nuevo usuario
        Task<string> RegisterAsync(RegisterDto registerDto);

        // Método para obtener un token de autenticación
        Task<DataUserDto> GetTokenAsync(LoginDto loginDto);

        // Método para agregar un rol a un usuario
        Task<string> AddRoleAsync(AddRoleDto addRoleDto);

        // Método para refrescar un token de autenticación
        Task<DataUserDto> RefreshTokenAsync(string refreshToken);

        // Método para cifrar una cookie
        string EncryptCookie(string cookie);

        // Método para descifrar una cookie cifrada
        string DecryptCookie(string encryptedCookie);
    }
}

```

En el archivo UserService.cs es donde encontraremos la lógica de negocio relacionada con la declaración de metodos del contrato IUserService.cs

__Archivo UserService.cs__
#### Dependencias relacionadas
```
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
```
#### Implementación de variables, contructor y su inicialización
En esta sección se inicializan las variables necesarias a nivel de clase 
```
namespace Api.Services
{
    // Implementación concreta de la interfaz IUserService
    public class UserService : IUserService
    {
        // Campos privados utilizados para la configuración, unidad de trabajo y otros servicios necesarios
        private readonly JWT _jwt;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IPasswordHasher<User> _passwordHasher;
        private readonly IDataProtectionProvider _dataProtectionProvider;

        // Constructor que recibe las dependencias necesarias a través de la inyección de dependencias
        public UserService(
            IUnitOfWork unitOfWork,
            IOptions<JWT> jwt,
            IPasswordHasher<User> passwordHasher,
            IDataProtectionProvider dataProtectionProvider
        )
        {
            // Inicialización de campos con los valores proporcionados
            _jwt = jwt.Value;
            _unitOfWork = unitOfWork;
            _passwordHasher = passwordHasher;
            _dataProtectionProvider = dataProtectionProvider;
        }

```
Implementación del método RegisterAsync para registrar un nuevo usuario
```
        
        public async Task<string> RegisterAsync(RegisterDto registerDto)
        {
            // Creación de una instancia de usuario con los datos proporcionados
            var user = new User { Username = registerDto.Username, Email = registerDto.Email };

            // Hash de la contraseña utilizando el servicio de hash de contraseñas
            user.Password = _passwordHasher.HashPassword(user, registerDto.Password);

            // Verificación si ya existe un usuario con el mismo nombre de usuario
            var existingUser = _unitOfWork
                .Users
                .Find(u => u.Username.ToLower() == registerDto.Username.ToLower())
                .FirstOrDefault();

            // Lógica de registro de usuario y asignación de rol predeterminado
            // Manejo de excepciones en caso de errores
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
```
#### Implementación del método GetTokenAsync para obtener un token de autenticación
```
        public async Task<DataUserDto> GetTokenAsync(LoginDto loginDto)
        {
            // Creación de un objeto DataUserDto para almacenar la información del usuario y el token
            DataUserDto dataUserDto = new DataUserDto();

            // Búsqueda del usuario por nombre de usuario en la base de datos
            var user = await _unitOfWork.Users.GetByUsernameAsync(loginDto.Username);

            // Lógica para manejar el caso cuando el usuario no existe
            if (user == null)
            {
                dataUserDto.IsAuthenticated = false;
                dataUserDto.Message = $"User does not exist with Name {loginDto.Username}.";
                return dataUserDto;
            }

            // Verificación de la contraseña utilizando el servicio de hash de contraseñas
            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                loginDto.Password
            );

            // Lógica para manejar la autenticación exitosa y generación del token
            if (result == PasswordVerificationResult.Success)
            {
                dataUserDto.Message = $"User {user.Username} is authenticated.";
                dataUserDto.IsAuthenticated = true;
                JwtSecurityToken jwtSecurityToken = CreateJwtToken(user);
                dataUserDto.Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);
                dataUserDto.Username = user.Username;
                dataUserDto.Email = user.Email;
                dataUserDto.Roles = user.Rols.Select(u => u.Name).ToList();

                // Lógica para manejar el token de actualización
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

            // Lógica para manejar la autenticación fallida
            dataUserDto.IsAuthenticated = false;
            dataUserDto.Message = $"Credentials incorrect for the user {user.Username}.";
            return dataUserDto;
        }
```
#### Implementación del método AddRoleAsync para agregar un rol a un usuario
```
        public async Task<string> AddRoleAsync(AddRoleDto addRoleDto)
        {
            // Búsqueda del usuario por nombre de usuario en la base de datos
            var user = await _unitOfWork.Users.GetByUsernameAsync(addRoleDto.Username);

            // Lógica para manejar el caso cuando el usuario no existe
            if (user == null)
            {
                return $"User {addRoleDto.Username} does not exists.";
            }

            // Verificación de la contraseña utilizando el servicio de hash de contraseñas
            var result = _passwordHasher.VerifyHashedPassword(
                user,
                user.Password,
                addRoleDto.Password
            );

            // Lógica para manejar la autenticación exitosa y asignación de rol
            if (result == PasswordVerificationResult.Success)
            {
                // Búsqueda del rol en la base de datos
                var rolExists = _unitOfWork
                    .Roles
                    .Find(u => u.Name.ToLower() == addRoleDto.Role.ToLower())
                    .FirstOrDefault();

                // Lógica para manejar la existencia del rol y asignación al usuario
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

            // Lógica para manejar la autenticación fallida
            return $"Invalid Credentials";
        }
```
#### Implementación del método RefreshTokenAsync para refrescar un token de autenticación
```
        public async Task<DataUserDto> RefreshTokenAsync(string refreshToken)
        {
            // Creación de un objeto DataUserDto para almacenar la información del usuario y el nuevo token
            var dataUserDto = new DataUserDto();

            // Búsqueda del usuario por token de actualización en la base de datos
            var User = await _unitOfWork.Users.GetByRefreshTokenAsync(refreshToken);

            // Lógica para manejar el caso cuando el usuario no está asociado al token
            if (User == null)
            {
                dataUserDto.IsAuthenticated = false;
                dataUserDto.Message = $"Token is not assigned to any user.";
                return dataUserDto;
            }

            // Obtención del objeto RefreshToken de la base de datos
            var refreshTokenBd = User.RefreshTokens.Single(x => x.Token == refreshToken);

            // Lógica para manejar el caso cuando el token de actualización no está activo
            if (!refreshTokenBd.IsActive)
            {
                dataUserDto.IsAuthenticated = false;
                dataUserDto.Message = $"Token is not active.";
                return dataUserDto;
            }

            // Revocación del token de actualización anterior y generación de uno nuevo
            refreshTokenBd.Revoked = DateTime.UtcNow;
            var newRefreshToken = CreateRefreshToken();
            User.RefreshTokens.Add(newRefreshToken);
            _unitOfWork.Users.Update(User);
            await _unitOfWork.SaveAsync();

            // Actualización del objeto DataUserDto con la información del nuevo token
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
```
#### Método privado para generar un nuevo token de actualización
``` 
        
       
        //Genera un nuevo token de actualización.
        private RefreshToken CreateRefreshToken()
        {
            // Crear un array de bytes para almacenar números aleatorios
            var randomNumber = new byte[32];

            // Utilizar el generador de números aleatorios seguro
            using (var generator = RandomNumberGenerator.Create())
            {
                // Llenar el array con números aleatorios
                generator.GetBytes(randomNumber);

                // Crear y devolver un objeto RefreshToken con los valores generados y calculados
                return new RefreshToken
                {
                    //Un objeto RefreshToken con un nuevo token generado aleatoriamente,
                    Token = Convert.ToBase64String(randomNumber),

                    //una fecha de expiración calculada en función de la configuración JWT,
                    Expires = DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutesRefreshToken),

                    //fecha de creación establecida en el momento actual en UTC.
                    Created = DateTime.UtcNow
                };
            }
        }
```
#### Método privado para crear un token JWT a partir de un objeto User
```

        private JwtSecurityToken CreateJwtToken(User User)
        {
            var roles = User.Rols;
            var roleClaims = new List<Claim>();
            
            // Creación de claims para cada rol del usuario
            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role.Name));
            }

            // Definición de reclamaciones para el token JWT
            var claims = new[]
            {
                // Reclamación estándar que representa el nombre de usuario del usuario
                new Claim(JwtRegisteredClaimNames.Sub, User.Username),

                // Reclamación estándar que proporciona un identificador único para la solicitud del token
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),

                // Reclamación estándar que representa la dirección de correo electrónico del usuario
                new Claim(JwtRegisteredClaimNames.Email, User.Email),

                // Reclamación personalizada que almacena el identificador único del usuario
                new Claim("uid", User.Id.ToString())
            }.Union(roleClaims);

         // Configuración de la clave de seguridad y credenciales de firma
            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(
                symmetricSecurityKey,
                SecurityAlgorithms.HmacSha256
            );

            // Creación del token JWT con la información proporcionada
            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutes),
                signingCredentials: signingCredentials
            );

            return jwtSecurityToken;
        }
```
        // Método para cifrar una cookie utilizando el servicio de protección de datos
        public string EncryptCookie(string cookie)
        {
            var protector = _dataProtectionProvider.CreateProtector("SecurityCookie");
            return protector.Protect(cookie);
        }

        // Método para descifrar una cookie cifrada utilizando el servicio de protección de datos
        public string DecryptCookie(string encryptedCookie)
        {
            var protector = _dataProtectionProvider.CreateProtector("SecurityCookie");
            return protector.Unprotect(encryptedCookie);
        }
    }
}

## Creación del controlador del usuario
Para la creación del usuario que contendrá la implementación del JWT, nos dirigimos a nuestra sección de controllers dentro de API
#### dependecias a usar y namespace
 ```
using Api.Dtos;
using Api.Helpers;
using Api.Services;
using AutoMapper;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
namespace Api.Controllers;
```
#### Creación de la clase
hereda de nuestro controlador base, se inicializan las variables a nivel de clase de solo lectura
- **IUserService _userService =**  Es el microservicio que contendrá la lógica de negocio asociada al JWT
- **IUnitOfWork _unitOfWork =** Hace parte del patrón de diseño unitOfWork 
- **IMapper _mapper =** Es una tecnica usada por nuestro ORM para mapear entidades a Dtos y viceversa (según la configuración)
- **IOptions<JWT> =**  Inyección de la configuración de JWT 
```
public class UserController : BaseApiController
{
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly JWT _jwt;

    public UserController( IUserService UserService, IUnitOfWork unitOfWork,
     IMapper mapper, IOptions<JWT> jwt)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userService = UserService;
        _jwt = jwt.Value;
    }
```
#### Controlador registro de Usuario
 - Registra un nuevo usuario mediante una solicitud HTTP POST a la ruta "register".

 - registerDto DTO (Data Transfer Object) que contiene los datos del usuario a registrar
 
 - __ActionResult__ que indica el resultado de la operación de registro.

```
    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(RegisterDto registerDto)
    {
        // Invoca el método de servicio para realizar el registro del usuario
        var result = await _userService.RegisterAsync(registerDto);

        // Retorna un Ok con el resultado del registro
        return Ok(result);
    }
```

#### Controlador token
 - Obtiene un token de autenticación mediante una solicitud HTTP POST a la ruta "token".
 - loginDto DTO (Data Transfer Object) que contiene las credenciales de inicio de sesión.
 - IActionResult que indica el resultado de la operación de obtención del token.
 - Si la obtención es exitosa, retorna un Ok con el token y establece el token de actualización en una cookie.
```
[HttpPost("token")]
public async Task<IActionResult> GetTokenAsync(LoginDto loginDto)
{
    // Invoca el método de servicio para obtener el token de autenticación
    var result = await _userService.GetTokenAsync(loginDto);

    // Establece el token de actualización en una cookie
    SetRefreshTokenInCookie(result.RefreshToken);

    // Retorna un Ok con el resultado y el token de autenticación
    return Ok(result);
}
```
#### Controlador Rol de usuario
- Agrega un rol a un usuario mediante una solicitud HTTP POST a la ruta "addrole".

- addRoleDto" DTO (Data Transfer Object) que contiene la información para agregar el rol.

- IActionResult que indica el resultado de la operación de agregar el rol.
- Si la operación es exitosa y el usuario tiene el rol "Manager", retorna un Ok con el resultado.
```
[HttpPost("addrole")]
[Authorize(Roles = "Manager")]
public async Task<IActionResult> AddRoleAsync(AddRoleDto addRoleDto)
{
    // Invoca el método de servicio para agregar un rol al usuario
    var result = await _userService.AddRoleAsync(addRoleDto);

    // Retorna un Ok con el resultado de la operación
    return Ok(result);
}
```
#### Controlador Refresh Token


 - Refresca un token de autenticación mediante una solicitud HTTP POST a la ruta "refreshToken".
 - IActionResult que indica el resultado de la operación de refrescar el token.
 - Si la operación es exitosa, retorna un Ok con el nuevo token y actualiza la cookie del token de actualización.

```
[HttpPost("refreshToken")]
public async Task<IActionResult> RefreshToken()
{
    // Obtiene el token de actualización cifrado almacenado en la cookie
    var encryptedRefreshToken = Request.Cookies["refreshToken"];

    // Descifra el token de actualización
    var refreshToken = _userService.DecryptCookie(encryptedRefreshToken);

    // Invoca el método de servicio para refrescar el token de autenticación
    var response = await _userService.RefreshTokenAsync(refreshToken);

    // Si se genera un nuevo token de actualización, actualiza la cookie
    if (!string.IsNullOrEmpty(response.RefreshToken))
    {
        var reEncryptedRefreshToken = _userService.EncryptCookie(response.RefreshToken);
        SetRefreshTokenInCookie(reEncryptedRefreshToken);
    }

    // Retorna un Ok con el resultado de la operación
    return Ok(response);
}

 ```