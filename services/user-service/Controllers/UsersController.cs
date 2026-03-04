using Microsoft.AspNetCore.Mvc;
using user_service.Dtos;
using user_service.Models;
using user_service.Services;

namespace user_service.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly UsersService _service;

    public UsersController(UsersService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserProfile>>> GetAll()
    {
        var users = await _service.GetAllAsync();
        return Ok(users);
    }

    [HttpPost]
    public async Task<ActionResult<UserProfile>> Create([FromBody] CreateUserRequest req)
    {
        if (string.IsNullOrWhiteSpace(req.DisplayName) || string.IsNullOrWhiteSpace(req.Email))
            return BadRequest("DisplayName and Email are required.");

        var user = new UserProfile
        {
            DisplayName = req.DisplayName.Trim(),
            Email = req.Email.Trim()
        };

        var created = await _service.CreateAsync(user);
        return Ok(created);
    }
}