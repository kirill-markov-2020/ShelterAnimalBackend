using ShelterAnimalBackend.Core.Entities;

namespace ShelterAnimalBackend.Core.Interfaces;

public interface ITemporaryAccommodationRepository
{
    Task<TemporaryAccommodation?> GetByIdAsync(int id);
    Task<List<TemporaryAccommodation>> GetAllAsync();
    Task<List<TemporaryAccommodation>> GetByVolunteerIdAsync(int volunteerId);
    Task AddAsync(TemporaryAccommodation accommodation);
    Task UpdateAsync(TemporaryAccommodation accommodation);
    Task DeleteAsync(int id);
}