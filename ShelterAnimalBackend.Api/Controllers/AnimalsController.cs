using Microsoft.AspNetCore.Mvc;
using ShelterAnimalBackend.Application.Services;
using ShelterAnimalBackend.Core.Dtos.Requests;
using ShelterAnimalBackend.Core.Entities;

namespace ShelterAnimalBackend.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AnimalsController : ControllerBase
{
    private readonly AnimalService _animalService;
    private readonly ILogger<AnimalsController> _logger;

    public AnimalsController(AnimalService animalService, ILogger<AnimalsController> logger)
    {
        _animalService = animalService;
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<List<Animal>>> GetAllAnimals()
    {        
        var animals = await _animalService.GetAllAsync();
        return Ok(animals);       
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Animal>> GetAnimalById(int id)
    {        
        var animal = await _animalService.GetByIdAsync(id);
        if (animal == null)
        {
            return NotFound($"Животное с ID {id} не найдено");
        }
        return Ok(animal); 
    }

    [HttpPost]
    public async Task<ActionResult<Animal>> AddAnimal([FromBody] CreateAnimalDto animalDto)
    {
        
        if (!ModelState.IsValid)
        {
            return BadRequest(ModelState);
        }

        var animal = new Animal
        {
            Name = animalDto.Name,
            TypeAnimalId = animalDto.TypeAnimalId,
            Gender = animalDto.Gender,
            Age = animalDto.Age,
            AnimalStatusId = animalDto.AnimalStatusId,
            Description = animalDto.Description,
            Photo = string.IsNullOrEmpty(animalDto.Photo) ? "http://localhost:5164/images/заглушка.png" : animalDto.Photo
        };

        await _animalService.AddAsync(animal);

        return CreatedAtAction(nameof(GetAnimalById), new { id = animal.Id }, animal);
        
    }




    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {   
        var animal = await _animalService.GetByIdAsync(id);
        if (animal == null)
        {
            return NotFound($"Животное с ID {id} не найдено");
        }

        if (!string.IsNullOrEmpty(animal.Photo) && animal.Photo != "http://localhost:5164/images/заглушка.png")
        {
            var fileName = Path.GetFileName(animal.Photo);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }

        await _animalService.DeleteAsync(id);
        return NoContent();        
    }
    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateAnimal(int id, [FromBody] Animal updatedAnimal)
    {
        if (updatedAnimal == null || updatedAnimal.Id == 0)
        {
            return BadRequest("Некорректные данные животного");
        }
        if (id != updatedAnimal.Id)
        {
            return BadRequest("ID животного не совпадает с ID в URL");
        }
        var existingAnimal = await _animalService.GetByIdAsync(id);
        if (existingAnimal == null)
        {
            return NotFound($"Животное с ID {id} не найдено");
        }
        existingAnimal.Name = updatedAnimal.Name;
        existingAnimal.TypeAnimalId = updatedAnimal.TypeAnimalId;
        existingAnimal.Gender = updatedAnimal.Gender;
        existingAnimal.Age = updatedAnimal.Age;
        existingAnimal.AnimalStatusId = updatedAnimal.AnimalStatusId;
        existingAnimal.Description = updatedAnimal.Description;
        if (!string.IsNullOrEmpty(updatedAnimal.Photo) && updatedAnimal.Photo != "http://localhost:5164/images/заглушка.png")
        {
            var fileName = Path.GetFileName(updatedAnimal.Photo);
            var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", fileName);

            if (!System.IO.File.Exists(filePath))
            {
                existingAnimal.Photo = "http://localhost:5164/images/заглушка.png";
            }
            else
            {
                existingAnimal.Photo = updatedAnimal.Photo;
            }
        }
        else
        {
            existingAnimal.Photo = "http://localhost:5164/images/заглушка.png";
        }
        await _animalService.UpdateAsync(existingAnimal);
        return NoContent();
    }
}