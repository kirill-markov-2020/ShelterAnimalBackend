using Microsoft.AspNetCore.Mvc;
using ShelterAnimalBackend.Application.Services;
using ShelterAnimalBackend.Core.Dtos.Requests;
using ShelterAnimalBackend.Core.Dtos.Responses;
using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;
using ShelterAnimalBackend.Core.Mappings;
using ShelterAnimalBackend.Infrastructure.Repositories;

namespace ShelterAnimalBackend.Api.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly UserService _userService;
    private readonly ILogger<UsersController> _logger;

    public UsersController(UserService userService, ILogger<UsersController> logger, IUserRepository userRepository)
    {
        _userService = userService;
        _logger = logger;
        _userRepository = userRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<UserResponse>>> GetAllUsers()
    {
            var users = await _userService.GetAllAsync();
            return Ok(users);
    }

    [HttpGet("{id:int}")]
    public async Task<ActionResult<UserResponse>> GetUserById(int id)
    {
            var user = await _userService.GetByIdAsync(id);
            return user == null ? NotFound(new { Message = $"User with ID {id} not found" }) : Ok(user);        
    }

    [HttpPost]
    public async Task<UserResponse> CreateAsync(CreateUserRequest request)
    {
        if (await _userRepository.EmailExistsAsync(request.Email))
        {
            throw new ArgumentException("email_taken", "Email уже занят");
        }

        if (await _userRepository.LoginExistsAsync(request.Login))
        {
            throw new ArgumentException("login_taken", "Логин уже занят");
        }

        var user = new User
        {
            Surname = request.Surname,
            Name = request.Name,
            Patronymic = request.Patronymic,
            Email = request.Email,
            Phone = request.Phone,
            Login = request.Login,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password), 
            RoleId = request.RoleId
        };

        await _userRepository.AddAsync(user);
        return user.ToResponse();
    }



    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateUser(int id, [FromBody] UpdateUserRequest request)
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

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteUser(int id)
    {        
        var result = await _userService.DeleteAsync(id);
        return result ? NoContent() : NotFound(new { Message = $"User with ID {id} not found" });        
    }
}