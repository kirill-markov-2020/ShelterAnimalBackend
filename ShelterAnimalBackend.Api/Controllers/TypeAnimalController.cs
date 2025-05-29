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
        try
        {
            var typeAnimals = await _typeAnimalRepository.GetAllAsync();
            return Ok(typeAnimals);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    [HttpPost]
    public async Task<ActionResult<TypeAnimal>> AddTypeAnimal([FromBody] TypeAnimal typeAnimal)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _typeAnimalRepository.AddAsync(typeAnimal);
            return CreatedAtAction(nameof(GetAllTypeAnimals), new { id = typeAnimal.Id }, typeAnimal);
        }
        catch (Exception ex)
        {
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
}
