using ShelterAnimalBackend.Core.Entities;

namespace ShelterAnimalBackend.Core.Interfaces;

public interface IAnimalStatusRepository
{
    Task<List<AnimalStatus>> GetAllAsync();
    Task AddAsync(AnimalStatus animalStatus);
}
