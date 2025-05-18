using ShelterAnimalBackend.Core.Entities;

namespace ShelterAnimalBackend.Core.Interfaces;

public interface IAdoptionApplicationRepository
{
    Task<AdoptionApplication?> GetByIdAsync(int id);
    Task<List<AdoptionApplication>> GetAllAsync();
    Task<List<AdoptionApplication>> GetByStatusIdAsync(int statusId);
    Task AddAsync(AdoptionApplication application);
    Task UpdateAsync(AdoptionApplication application);
    Task DeleteAsync(int id);
}