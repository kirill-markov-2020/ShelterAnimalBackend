using Microsoft.EntityFrameworkCore;
using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;
using ShelterAnimalBackend.Infrastructure.Data;

namespace ShelterAnimalBackend.Infrastructure.Repositories;

public class TypeAnimalRepository : ITypeAnimalRepository
{
    private readonly AnimalShelterDbContext _context;

    public TypeAnimalRepository(AnimalShelterDbContext context)
    {
        _context = context;
    }

    public async Task<List<TypeAnimal>> GetAllAsync()
    {
        return await _context.TypeAnimal.ToListAsync();
    }

    public async Task AddAsync(TypeAnimal typeAnimal)
    {
        await _context.TypeAnimal.AddAsync(typeAnimal);
        await _context.SaveChangesAsync();
    }
}
