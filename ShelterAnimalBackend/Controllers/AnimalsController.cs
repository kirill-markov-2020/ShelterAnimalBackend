// ShelterAnimalBackend.Api/Controllers/AnimalsController.cs
using Microsoft.AspNetCore.Mvc;
using ShelterAnimalBackend.Application.Services;
using ShelterAnimalBackend.Core.Entities;

namespace ShelterAnimalBackend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private readonly AnimalService _animalService;

    public AnimalsController(AnimalService animalService)
    {
        _animalService = animalService;
    }

    [HttpGet]
    public async Task<ActionResult<List<Animal>>> GetAnimals()
    {
        var animals = await _animalService.GetAllAsync();
        return Ok(animals);
    }
}