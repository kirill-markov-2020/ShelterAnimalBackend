using ShelterAnimalBackend.Core.Entities;

namespace ShelterAnimalBackend.Core.Interfaces;

public interface ITypeAnimalRepository
{
    Task<List<TypeAnimal>> GetAllAsync();
    Task AddAsync(TypeAnimal typeAnimal);
}
