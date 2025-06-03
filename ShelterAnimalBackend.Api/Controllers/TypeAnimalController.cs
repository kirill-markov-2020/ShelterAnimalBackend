using Microsoft.AspNetCore.Mvc;
using ShelterAnimalBackend.Application.Services;
using ShelterAnimalBackend.Core.Entities;
using ShelterAnimalBackend.Core.Interfaces;

namespace ShelterAnimalBackend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TypeAnimalController : ControllerBase
{
    private readonly ITypeAnimalRepository _typeAnimalRepository;

    public TypeAnimalController(ITypeAnimalRepository typeAnimalRepository)
    {
        _typeAnimalRepository = typeAnimalRepository;
    }

    [HttpGet]
    public async Task<ActionResult<List<TypeAnimal>>> GetAllTypeAnimals()
    {
            var typeAnimals = await _typeAnimalRepository.GetAllAsync();
            return Ok(typeAnimals);        
    }

    [HttpPost]
    public async Task<ActionResult<TypeAnimal>> AddTypeAnimal([FromBody] TypeAnimal typeAnimal)
    {       
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        await _typeAnimalRepository.AddAsync(typeAnimal);
        return CreatedAtAction(nameof(GetAllTypeAnimals), new { id = typeAnimal.Id }, typeAnimal);        
    }
}
