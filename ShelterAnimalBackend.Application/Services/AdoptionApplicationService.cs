using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;

namespace ShelterAnimalBackend.Application.Services;

public class AdoptionApplicationService
{
    private readonly IAdoptionApplicationRepository _applicationRepository;

    public AdoptionApplicationService(IAdoptionApplicationRepository applicationRepository)
    {
        _applicationRepository = applicationRepository;
    }

    public async Task<AdoptionApplication?> GetByIdAsync(int id)
    {
        return await _applicationRepository.GetByIdAsync(id);
    }

    public async Task<List<AdoptionApplication>> GetAllAsync()
    {
        return await _applicationRepository.GetAllAsync();
    }

    public async Task<List<AdoptionApplication>> GetByStatusIdAsync(int statusId)
    {
        return await _applicationRepository.GetByStatusIdAsync(statusId);
    }

    public async Task AddAsync(AdoptionApplication application)
    {
        await _applicationRepository.AddAsync(application);
    }

    public async Task UpdateAsync(AdoptionApplication application)
    {
        await _applicationRepository.UpdateAsync(application);
    }

    public async Task DeleteAsync(int id)
    {
        await _applicationRepository.DeleteAsync(id);
    }
}