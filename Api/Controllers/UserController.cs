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

public class UserController : BaseApiController
{
    private readonly IUserService _userService;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    private readonly JWT _jwt;

    public UserController(
        IUserService UserService,
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IOptions<JWT> jwt
    )
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
        _userService = UserService;
        _jwt = jwt.Value;
    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<IEnumerable<UserDto>>> Get()
    {
        var users = await _unitOfWork.Users.GetAllAsync();
        return _mapper.Map<List<UserDto>>(users);
    }

    [HttpGet("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Get(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        return _mapper.Map<UserDto>(user);
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterAsync(RegisterDto registerDto)
    {
        var result = await _userService.RegisterAsync(registerDto);
        return Ok(result);
    }

    [HttpPost("token")]
    public async Task<IActionResult> GetTokenAsync(LoginDto loginDto)
    {
        var result = await _userService.GetTokenAsync(loginDto);
        SetRefreshTokenInCookie(result.RefreshToken);
        return Ok(result);
    }

    [HttpPost("addrole")]
    [Authorize(Roles = "Manager")]
    public async Task<IActionResult> AddRoleAsync(AddRoleDto addRoleDto)
    {
        var result = await _userService.AddRoleAsync(addRoleDto);
        return Ok(result);
    }

    [HttpPost("refreshToken")]
    public async Task<IActionResult> RefreshToken()
    {
        var encryptedRefreshToken = Request.Cookies["refreshToken"];
        var refreshToken = _userService.DecryptCookie(encryptedRefreshToken);
        var response = await _userService.RefreshTokenAsync(refreshToken);
        if (!string.IsNullOrEmpty(response.RefreshToken))
        {
            var reEncryptedRefreshToken = _userService.EncryptCookie(response.RefreshToken);
            SetRefreshTokenInCookie(reEncryptedRefreshToken);
        }
        return Ok(response);
    }

    [HttpPut("{id}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<UserDto>> Put(int id, [FromBody] UserDto userDto)
    {
        if (userDto == null)
        {
            return NotFound();
        }
        var user = _mapper.Map<User>(userDto);
        _unitOfWork.Users.Update(user);
        await _unitOfWork.SaveAsync();
        return userDto;
    }

    [HttpDelete("{id}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Delete(int id)
    {
        var user = await _unitOfWork.Users.GetByIdAsync(id);
        if (user == null)
        {
            return NotFound();
        }
        _unitOfWork.Users.Remove(user);
        await _unitOfWork.SaveAsync();
        return NoContent();
    }

    private void SetRefreshTokenInCookie(string refreshToken)
    {
        var encryptedRefreshToken = _userService.EncryptCookie(refreshToken);
        var cookieOptions = new CookieOptions
        {
            HttpOnly = true,
            Expires = DateTime.UtcNow.AddMinutes(_jwt.DurationInMinutesCookie),
        };
        Response.Cookies.Append("refreshToken", encryptedRefreshToken, cookieOptions);
    }
}
