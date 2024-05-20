using System.Security.Claims;
using Domain.Entities;
using Infrastructure.Repositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MusicApplication.Controllers.Dto;

namespace MusicApplication.Controllers;


[Route("user")]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;

    public UserController(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    [HttpGet("profile")]
    [Authorize]
    public async Task<IActionResult> GetProfile()
    {
        string email = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)!.Value;
        var user = await _userRepository.TryGetByEmailAsync(email);
        if (user == null)
        {
            return NotFound();
        }
        return Ok(new ProfileResponseDto {Username = user.Username, Email = user.Email});
    }
}
