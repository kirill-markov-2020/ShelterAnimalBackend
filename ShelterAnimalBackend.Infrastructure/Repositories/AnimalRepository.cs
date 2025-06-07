using Microsoft.EntityFrameworkCore;
using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;
using ShelterAnimalBackend.Infrastructure.Data;

namespace ShelterAnimalBackend.Infrastructure.Repositories;

public class AnimalRepository : IAnimalRepository
{
    private readonly AnimalShelterDbContext _context;

    public AnimalRepository(AnimalShelterDbContext context)
    {
        _context = context;
    }

    public async Task<Animal?> GetByIdAsync(int id)
    {
        return await _context.Animal
            .Include(a => a.TypeAnimal)
            .Include(a => a.AnimalStatus)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task<List<Animal>> GetAllAsync()
    {
        return await _context.Animal
            .Include(a => a.TypeAnimal)
            .Include(a => a.AnimalStatus)
            .ToListAsync();
    }

    public async Task AddAsync(Animal animal)
    {
        animal.TypeAnimal = null;
        animal.AnimalStatus = null;

        await _context.Animal.AddAsync(animal);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Animal animal)
    {
        _context.Animal.Update(animal);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var animal = await GetByIdAsync(id);
        if (animal != null)
        {
            _context.Animal.Remove(animal);
            await _context.SaveChangesAsync();
        }
    }
}