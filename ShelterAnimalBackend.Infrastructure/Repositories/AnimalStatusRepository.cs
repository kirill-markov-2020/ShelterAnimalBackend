using Microsoft.EntityFrameworkCore;
using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;
using ShelterAnimalBackend.Infrastructure.Data;

namespace ShelterAnimalBackend.Infrastructure.Repositories;

public class AnimalStatusRepository : IAnimalStatusRepository
{
    private readonly AnimalShelterDbContext _context;

    public AnimalStatusRepository(AnimalShelterDbContext context)
    {
        _context = context;
    }

    public async Task<List<AnimalStatus>> GetAllAsync()
    {
        return await _context.AnimalStatus.ToListAsync();
    }

    public async Task AddAsync(AnimalStatus animalStatus)
    {
        await _context.AnimalStatus.AddAsync(animalStatus);
        await _context.SaveChangesAsync();
    }
}
