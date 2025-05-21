using ShelterAnimalBackend.Core.Dtos.Requests;
using ShelterAnimalBackend.Core.Dtos.Responses;
using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;
using ShelterAnimalBackend.Core.Mappings;

namespace ShelterAnimalBackend.Application.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserResponse?> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        return user?.ToResponse();
    }

    public async Task<List<UserResponse>> GetAllAsync()
    {
        var users = await _userRepository.GetAllAsync();
        return users.Select(u => u.ToResponse()).ToList();
    }

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
            Password = request.Password,
            RoleId = request.RoleId
        };

        await _userRepository.AddAsync(user);
        return user.ToResponse();
    }

    public async Task<UserResponse?> UpdateAsync(UpdateUserRequest request)
    {
        var user = await _userRepository.GetByIdAsync(request.Id);
        if (user == null) return null;

        user.Surname = request.Surname;
        user.Name = request.Name;
        user.Patronymic = request.Patronymic;
        user.Email = request.Email;
        user.Phone = request.Phone;
        user.Login = request.Login;
        user.RoleId = request.RoleId;

        await _userRepository.UpdateAsync(user);
        return user.ToResponse();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);
        if (user == null) return false;

        await _userRepository.DeleteAsync(id);
        return true;
    }
}