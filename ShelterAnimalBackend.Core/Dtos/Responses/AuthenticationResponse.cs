namespace ShelterAnimalBackend.Core.Dtos.Responses;

public record AuthResponse(bool IsSuccess, string Token, string ErrorMessage = null, string Role = null);
