using Microsoft.EntityFrameworkCore;
using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;
using ShelterAnimalBackend.Infrastructure.Data;

namespace ShelterAnimalBackend.Application.Services;

public class AnimalService
{
    private readonly IAnimalRepository _animalRepository;
    private readonly AnimalShelterDbContext _context;

    public AnimalService(IAnimalRepository animalRepository, AnimalShelterDbContext context)
    {
        _animalRepository = animalRepository;
        _context = context;
    }

    public async Task<Animal?> GetByIdAsync(int id)
    {
        return await _animalRepository.GetByIdAsync(id);
    }

    public async Task<List<Animal>> GetAllAsync()
    {
        return await _animalRepository.GetAllAsync();
    }

    public async Task AddAsync(Animal animal)
    {
        var typeExists = await _context.TypeAnimal.AnyAsync(t => t.Id == animal.TypeAnimalId);
        var statusExists = await _context.AnimalStatus.AnyAsync(s => s.Id == animal.AnimalStatusId);

        if (!typeExists || !statusExists)
        {
            throw new ArgumentException("Invalid TypeAnimalId or AnimalStatusId");
        }

        await _animalRepository.AddAsync(animal);
    }

    public async Task UpdateAsync(Animal animal)
    {
        await _animalRepository.UpdateAsync(animal);
    }

    public async Task DeleteAsync(int id)
    {
        await _animalRepository.DeleteAsync(id);
    }
}