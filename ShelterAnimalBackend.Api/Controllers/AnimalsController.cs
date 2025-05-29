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
        try
        {
            var animals = await _animalService.GetAllAsync();
            return Ok(animals);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при получении списка животных");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<Animal>> GetAnimalById(int id)
    {
        try
        {
            var animal = await _animalService.GetByIdAsync(id);
            if (animal == null)
            {
                return NotFound($"Животное с ID {id} не найдено");
            }
            return Ok(animal);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при получении животного с ID {id}");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }

    [HttpPost]
    public async Task<ActionResult<Animal>> AddAnimal([FromBody] CreateAnimalDto animalDto)
    {
        try
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
                Photo = string.IsNullOrEmpty(animalDto.Photo) ? "/images/заглушка.png" : animalDto.Photo
            };

            await _animalService.AddAsync(animal);

            return CreatedAtAction(nameof(GetAnimalById), new { id = animal.Id }, animal);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Ошибка при добавлении животного");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAnimal(int id)
    {
        try
        {
            var animal = await _animalService.GetByIdAsync(id);
            if (animal == null)
            {
                return NotFound($"Животное с ID {id} не найдено");
            }

            await _animalService.DeleteAsync(id);
            return NoContent(); 
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Ошибка при удалении животного с ID {id}");
            return StatusCode(500, "Внутренняя ошибка сервера");
        }
    }
    [HttpPost("upload")]
    public async Task<IActionResult> UploadImage(IFormFile file)
    {
        if (file == null || file.Length == 0)
            return BadRequest("Файл не выбран");

        var allowedExtensions = new[] { ".png", ".jpg", ".jpeg" };
        var fileExtension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(fileExtension))
            return BadRequest("Недопустимый формат файла. Разрешены только PNG, JPG и JPEG.");

        var uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "public", "images");
        if (!Directory.Exists(uploadsFolder))
            Directory.CreateDirectory(uploadsFolder);

        var uniqueFileName = Guid.NewGuid().ToString() + fileExtension;
        var filePath = Path.Combine(uploadsFolder, uniqueFileName);

        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        var relativePath = $"/images/{uniqueFileName}";
        return Ok(new { filePath = relativePath });
    }
}