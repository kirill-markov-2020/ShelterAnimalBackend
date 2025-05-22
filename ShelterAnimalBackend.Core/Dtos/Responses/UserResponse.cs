using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelterAnimalBackend.Core.Dtos.Responses;

public record UserResponse(
    int Id,
    string Surname,
    string Name,
    string? Patronymic,
    string Email,
    string Phone,
    string Login,
    int RoleId,
    string RoleName);
