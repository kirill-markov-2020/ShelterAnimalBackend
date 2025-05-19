using ShelterAnimalBackend.Core.Entities;

namespace ShelterAnimalBackend.Core.Interfaces;

public interface IAdoptionRepository
{
    Task<Adoption?> GetByIdAsync(int id);
    Task<List<Adoption>> GetAllAsync();
    Task<List<Adoption>> GetByUserIdAsync(int userId);
    Task UpdateAsync(Adoption adoption);
}