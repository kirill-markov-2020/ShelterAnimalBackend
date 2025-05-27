using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ShelterAnimalBackend.Core;
using ShelterAnimalBackend.Core.Dtos.Requests;
using ShelterAnimalBackend.Core.Dtos.Responses;
using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

public class AuthService
{
    private readonly IUserRepository _userRepository;
    private readonly ILogger<AuthService> _logger;
    private readonly JwtSettings _jwtSettings;

    public AuthService(
        IUserRepository userRepository,
        IOptions<JwtSettings> jwtSettings,
        ILogger<AuthService> logger)
    {
        _userRepository = userRepository;
        _jwtSettings = jwtSettings.Value;
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
                Encoding.UTF8.GetBytes(_jwtSettings.SecurityKey));

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
                issuer: _jwtSettings.ValidIssuer,
                audience: _jwtSettings.ValidAudience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_jwtSettings.ExpiryInMinutes),
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
