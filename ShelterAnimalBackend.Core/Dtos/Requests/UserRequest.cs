using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelterAnimalBackend.Core.Dtos.Requests;

public record CreateUserRequest(
    string Surname,
    string Name,
    string? Patronymic,
    string Email,
    string Phone,
    string Login,
    string Password,
    int RoleId = 3);

public record UpdateUserRequest(
    int Id,
    string Surname,
    string Name,
    string? Patronymic,
    string Email,
    string Phone,
    string Login,
    int RoleId);
