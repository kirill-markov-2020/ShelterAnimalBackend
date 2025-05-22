using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using ShelterAnimalBackend.Core.Dtos.Requests;
using ShelterAnimalBackend.Core.Dtos.Responses;
using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ShelterAnimalBackend.Application.Services;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _configuration;
    private readonly ILogger<AuthService> _logger;

    public AuthService(
        IUserRepository userRepository,
        IConfiguration configuration,
        ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _configuration = configuration;
        _logger = logger;
    }

    public async Task<AuthResponse> Login(LoginRequest request)
    {
        try
        {
            var user = await _userRepository.GetByLoginAsync(request.Login);
            if (user == null)
            {
                return new AuthResponse(false, null, "Неверный логин или пароль");
            }

            if (user.Password != request.Password)
            {
                return new AuthResponse(false, null, "Неверный логин или пароль");
            }

            if (user.Role == null)
            {
                user = await _userRepository.GetByIdAsync(user.Id);
                if (user.Role == null)
                {
                    throw new Exception("Роль пользователя не найдена");
                }
            }

            var token = GenerateJwtToken(user);
            return new AuthResponse(true, token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при аутентификации");
            return new AuthResponse(false, null, "Ошибка генерации токена: " + ex.Message);
        }
    }

    private string GenerateJwtToken(User user)
    {
        try
        {
            var securityKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(_configuration["JwtSettings:securityKey"]));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            var claims = new List<Claim>
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Login),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(ClaimTypes.NameIdentifier, user.Id.ToString())
        };

            if (user.Role != null)
            {
                claims.Add(new Claim(ClaimTypes.Role, user.Role.Name));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["JwtSettings:validIssuer"],
                audience: _configuration["JwtSettings:validAudience"],
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToDouble(
                    _configuration["JwtSettings:expiryInMinutes"])),
                signingCredentials: credentials
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка генерации JWT токена");
            throw new Exception("Не удалось сгенерировать токен", ex);
        }
    }
}