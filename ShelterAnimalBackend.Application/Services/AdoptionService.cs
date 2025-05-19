using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;

namespace ShelterAnimalBackend.Application.Services;

public class AdoptionService
{
    private readonly IAdoptionRepository _adoptionRepository;

    public AdoptionService(IAdoptionRepository adoptionRepository)
    {
        _adoptionRepository = adoptionRepository;
    }

    public async Task<Adoption?> GetByIdAsync(int id)
    {
        return await _adoptionRepository.GetByIdAsync(id);
    }

    public async Task<List<Adoption>> GetAllAsync()
    {
        return await _adoptionRepository.GetAllAsync();
    }

    public async Task<List<Adoption>> GetByUserIdAsync(int userId)
    {
        return await _adoptionRepository.GetByUserIdAsync(userId);
    }

   

    public async Task UpdateAsync(Adoption adoption)
    {
        await _adoptionRepository.UpdateAsync(adoption);
    }

   
}