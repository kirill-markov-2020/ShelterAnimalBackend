using Microsoft.AspNetCore.Mvc;
using ShelterAnimalBackend.Application.Services;
using ShelterAnimalBackend.Core.Dtos.Requests;
using ShelterAnimalBackend.Core.Dtos.Responses;

namespace ShelterAnimalBackend.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly UserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(UserService userService, ILogger<UsersController> logger)
    {
        _userService = userService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAllUsers()
    {
        try
        {
            var users = await _userService.GetAllAsync();
            return Ok(users);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while getting all users");
            return StatusCode(500, new { Message = "Internal server error" });
        }
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserResponse>> GetUserById(int id)
    {
        try
        {
            var user = await _userService.GetByIdAsync(id);
            return user == null ? NotFound(new { Message = $"User with ID {id} not found" }) : Ok(user);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error while getting user with ID {id}");
            return StatusCode(500, new { Message = "Internal server error" });
        }
    }

    [HttpPost]
    public async Task<ActionResult<UserResponse>> CreateUser([FromBody] CreateUserRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdUser = await _userService.CreateAsync(request);
            return CreatedAtAction(nameof(GetUserById), new { id = createdUser.Id }, createdUser);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error while creating user");
            return StatusCode(500, new { Message = "Internal server error" });
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
    {
        try
        {
            if (id != request.Id)
            {
                return BadRequest(new { Message = "ID in URL and request body do not match" });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var updatedUser = await _userService.UpdateAsync(request);
            return updatedUser == null
                ? NotFound(new { Message = $"User with ID {id} not found" })
                : NoContent();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error while updating user with ID {id}");
            return StatusCode(500, new { Message = "Internal server error" });
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        try
        {
            var result = await _userService.DeleteAsync(id);
            return result ? NoContent() : NotFound(new { Message = $"User with ID {id} not found" });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error while deleting user with ID {id}");
            return StatusCode(500, new { Message = "Internal server error" });
        }
    }
}