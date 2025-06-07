using Microsoft.EntityFrameworkCore;
using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;
using ShelterAnimalBackend.Infrastructure.Data;

namespace ShelterAnimalBackend.Infrastructure.Repositories;

public class AdoptionRepository : IAdoptionRepository
{
    private readonly AnimalShelterDbContext _context;

    public AdoptionRepository(AnimalShelterDbContext context)
    {
        _context = context;
    }

    public async Task<Adoption?> GetByIdAsync(int id)
    {
        return await _context.Adoption
            .Include(a => a.User)
            .Include(a => a.Animal)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Adoption>> GetAllAsync()
    {
        return await _context.Adoption
            .Include(a => a.User)
            .Include(a => a.Animal)
            .ToListAsync();
    }

    public async Task<List<Adoption>> GetByUserIdAsync(int userId)
    {
        return await _context.Adoption
            .Include(a => a.User)
            .Include(a => a.Animal)
            .Where(a => a.UserId == userId)
            .ToListAsync();
    }

    

    public async Task UpdateAsync(Adoption adoption)
    {
        _context.Adoption.Update(adoption);
        await _context.SaveChangesAsync();
    }

    
}