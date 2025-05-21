using Microsoft.EntityFrameworkCore;
using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;
using ShelterAnimalBackend.Infrastructure.Data;

namespace ShelterAnimalBackend.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly AnimalShelterDbContext _context;

    public UserRepository(AnimalShelterDbContext context)
    {
        _context = context;
    }

    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.User
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Id == id);
    }

    

    public async Task<List<User>> GetAllAsync()
    {
        return await _context.User
            .Include(u => u.Role)
            .ToListAsync();
    }

    public async Task AddAsync(User user)
    {
        await _context.User.AddAsync(user);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(User user)
    {
        _context.User.Update(user);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var user = await GetByIdAsync(id);
        if (user != null)
        {
            _context.User.Remove(user);
            await _context.SaveChangesAsync();
        }
    }
    public async Task<bool> EmailExistsAsync(string email)
    {
        return await _context.User.AnyAsync(u => u.Email == email);
    }

    public async Task<bool> LoginExistsAsync(string login)
    {
        return await _context.User.AnyAsync(u => u.Login == login);
    }
}