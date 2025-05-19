using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;

namespace ShelterAnimalBackend.Application.Services;

public class AnimalService
{
    private readonly IAnimalRepository _animalRepository;

    public AnimalService(IAnimalRepository animalRepository)
    {
        _animalRepository = animalRepository;
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