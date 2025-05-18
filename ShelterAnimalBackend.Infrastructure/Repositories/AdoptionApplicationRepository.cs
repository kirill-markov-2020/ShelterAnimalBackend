using Microsoft.EntityFrameworkCore;
using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;
using ShelterAnimalBackend.Infrastructure.Data;

namespace ShelterAnimalBackend.Infrastructure.Repositories;

public class AdoptionApplicationRepository : IAdoptionApplicationRepository
{
    private readonly AnimalShelterDbContext _context;

    public AdoptionApplicationRepository(AnimalShelterDbContext context)
    {
        _context = context;
    }

    public async Task<AdoptionApplication?> GetByIdAsync(int id)
    {
        return await _context.AdoptionApplication
            .Include(a => a.User)
            .Include(a => a.Animal)
            .Include(a => a.StatusAdoption)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<AdoptionApplication>> GetAllAsync()
    {
        return await _context.AdoptionApplication
            .Include(a => a.User)
            .Include(a => a.Animal)
            .Include(a => a.StatusAdoption)
            .ToListAsync();
    }

    public async Task<List<AdoptionApplication>> GetByStatusIdAsync(int statusId)
    {
        return await _context.AdoptionApplication
            .Include(a => a.User)
            .Include(a => a.Animal)
            .Include(a => a.StatusAdoption)
            .Where(a => a.StatusAdoptionId == statusId)
            .ToListAsync();
    }

    public async Task AddAsync(AdoptionApplication application)
    {
        await _context.AdoptionApplication.AddAsync(application);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(AdoptionApplication application)
    {
        _context.AdoptionApplication.Update(application);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var application = await GetByIdAsync(id);
        if (application != null)
        {
            _context.AdoptionApplication.Remove(application);
            await _context.SaveChangesAsync();
        }
    }
}