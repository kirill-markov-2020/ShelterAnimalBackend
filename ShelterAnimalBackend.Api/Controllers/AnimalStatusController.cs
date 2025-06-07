using Microsoft.AspNetCore.Mvc;
using ShelterAnimalBackend.Application.Services;
using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;

namespace ShelterAnimalBackend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalStatusController : ControllerBase
{
    private readonly IAnimalStatusRepository _animalStatusRepository;

    public AnimalStatusController(IAnimalStatusRepository animalStatusRepository)
    {
        _animalStatusRepository = animalStatusRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<AnimalStatus>>> GetAllAnimalStatuses()
    {
        
        var animalStatuses = await _animalStatusRepository.GetAllAsync();
        return Ok(animalStatuses);
        
       
    }

    [HttpPost]
    public async Task<ActionResult<AnimalStatus>> AddAnimalStatus([FromBody] AnimalStatus animalStatus)
    {
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _animalStatusRepository.AddAsync(animalStatus);
        return CreatedAtAction(nameof(GetAllAnimalStatuses), new { id = animalStatus.Id }, animalStatus);
        
    }
}
