using ShelterAnimalBackend.Core.Dtos.Responses;
using ShelterAnimalBackend.Core.Entities;

namespace ShelterAnimalBackend.Core.Mappings;

public static class UserMappings
{
    public static UserResponse ToResponse(this User user)
    {
        return new UserResponse(
            user.Id,
            user.Surname,
            user.Name,
            user.Patronymic,
            user.Email,
            user.Phone,
            user.Login,
            user.RoleId,
            user.Role?.Name ?? "Unknown");
    }
}