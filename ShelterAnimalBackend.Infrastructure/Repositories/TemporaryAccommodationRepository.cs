using Microsoft.EntityFrameworkCore;
using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;
using ShelterAnimalBackend.Infrastructure.Data;

namespace ShelterAnimalBackend.Infrastructure.Repositories;

public class TemporaryAccommodationRepository : ITemporaryAccommodationRepository
{
    private readonly AnimalShelterDbContext _context;

    public TemporaryAccommodationRepository(AnimalShelterDbContext context)
    {
        _context = context;
    }

    public async Task<TemporaryAccommodation?> GetByIdAsync(int id)
    {
        return await _context.TemporaryAccommodation
            .Include(t => t.User)
            .Include(t => t.Animal)
            .FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task<List<TemporaryAccommodation>> GetAllAsync()
    {
        return await _context.TemporaryAccommodation
            .Include(t => t.User)
            .Include(t => t.Animal)
            .ToListAsync();
    }

    public async Task<List<TemporaryAccommodation>> GetByVolunteerIdAsync(int volunteerId)
    {
        return await _context.TemporaryAccommodation
            .Include(t => t.User)
            .Include(t => t.Animal)
            .Where(t => t.VolunteerId == volunteerId)
            .ToListAsync();
    }

    public async Task AddAsync(TemporaryAccommodation accommodation)
    {
        await _context.TemporaryAccommodation.AddAsync(accommodation);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(TemporaryAccommodation accommodation)
    {
        _context.TemporaryAccommodation.Update(accommodation);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var accommodation = await GetByIdAsync(id);
        if (accommodation != null)
        {
            _context.TemporaryAccommodation.Remove(accommodation);
            await _context.SaveChangesAsync();
        }
    }
}