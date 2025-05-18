using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;

namespace ShelterAnimalBackend.Application.Services;

public class TemporaryAccommodationService
{
    private readonly ITemporaryAccommodationRepository _accommodationRepository;

    public TemporaryAccommodationService(ITemporaryAccommodationRepository accommodationRepository)
    {
        _accommodationRepository = accommodationRepository;
    }

    public async Task<TemporaryAccommodation?> GetByIdAsync(int id)
    {
        return await _accommodationRepository.GetByIdAsync(id);
    }

    public async Task<List<TemporaryAccommodation>> GetAllAsync()
    {
        return await _accommodationRepository.GetAllAsync();
    }

    public async Task<List<TemporaryAccommodation>> GetByVolunteerIdAsync(int volunteerId)
    {
        return await _accommodationRepository.GetByVolunteerIdAsync(volunteerId);
    }

    public async Task AddAsync(TemporaryAccommodation accommodation)
    {
        await _accommodationRepository.AddAsync(accommodation);
    }

    public async Task UpdateAsync(TemporaryAccommodation accommodation)
    {
        await _accommodationRepository.UpdateAsync(accommodation);
    }

    public async Task DeleteAsync(int id)
    {
        await _accommodationRepository.DeleteAsync(id);
    }
}