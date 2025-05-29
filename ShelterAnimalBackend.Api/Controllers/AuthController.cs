using Microsoft.AspNetCore.Mvc;
using ShelterAnimalBackend.Application.Services;
using ShelterAnimalBackend.Core.Dtos.Requests;
using Microsoft.Extensions.Logging;

namespace ShelterAnimalBackend.Api.Controllers;

[Route("api/auth")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly ILogger<AuthController> _logger;
    private readonly object _configuration;

    public AuthController(AuthService authService, ILogger<AuthController> logger)
    {
        _authService = authService;
        _logger = logger;
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new { message = "Некорректные данные" });
            }

            var result = await _authService.Login(request);

            if (!result.IsSuccess)
            {
                return BadRequest(new { message = result.ErrorMessage });
            }

            return Ok(new { token = result.Token, role = result.Role });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "AuthController error");
            return StatusCode(500, new { message = "Внутренняя ошибка сервера" });
        }
    }


}