
using Infrastructure.Repositories;

namespace MusicApplication.Controllers;

using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using MusicApplication.Controllers.Dto;
using Domain.Entities;
using Infrastructure;
// using Infrastructure.Repositories;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

[Route("auth")]
public class AuthController : ControllerBase
{
    private readonly IPasswordManager _passwordManager;
    private readonly IUserRepository _userRepository;

    public AuthController(IPasswordManager passwordManager, IUserRepository userRepository)
    {
        _passwordManager = passwordManager;
        _userRepository = userRepository;
    }
    
    [HttpPost("registration")]
    public IActionResult Registration([FromBody] RegistrationDto registrationDto)
    {
        if (!registrationDto.IsValid())
        {
            return BadRequest(registrationDto.Errors);
        }

        User user = new User{Username = registrationDto.Username, Email = registrationDto.Email};

        if (_userRepository.IsUserAlreadyExist(user))
        {
            return Conflict();
        }

        byte[] salt;
        user.PasswordHash = _passwordManager.GetPasswordHash(registrationDto.Password, out salt);
        user.PasswordSalt = salt;

        _userRepository.CreateAsync(user);
        return Ok();
    }
    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginDto loginDto)
    {
        if (!loginDto.IsValid())
        {
            Console.WriteLine("login dto problem");
            return BadRequest(loginDto.Errors);
        }
        User? user = _userRepository.TryGetByEmailAsync(loginDto.Email!).Result;
        if (user is null)
        {
            return NotFound(new { User = "not found" });
        }
        if (!_passwordManager.VerifyPassword(loginDto.Password!, user!.PasswordHash, user.PasswordSalt))
        {
            return BadRequest(new {Password = "incorrect"} );
        }

        user.LastLoginDate = new DateTime();
        user.LastLoginDate = DateTime.Now;

        List<Claim> userClaims = new List<Claim>
        {
            new(ClaimTypes.Email, user.Email),
            new(ClaimTypes.Role, user.Role.ToString())
        };
        
        var jwt = new JwtSecurityToken(
        			issuer: Constants.Authentication.Issuer,
        			audience: Constants.Authentication.Audience,
        			claims: userClaims,
        			expires: DateTime.UtcNow.AddDays(1),
        			signingCredentials: new SigningCredentials(
        				new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Constants.Authentication.SecretKey)),
        				SecurityAlgorithms.HmacSha256));

        
         
        return Ok( new LoginResponseDto {AccessToken = new JwtSecurityTokenHandler().WriteToken(jwt)} );
    }
    

}