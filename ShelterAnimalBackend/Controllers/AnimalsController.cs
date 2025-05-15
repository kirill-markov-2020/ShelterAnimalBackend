using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ModelsLibrary;
using ShelterAnimalBackend.Data;

namespace ShelterAnimalBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnimalsController : ControllerBase
    {
        private readonly AnimalShelterDbContext _context;

        public AnimalsController(AnimalShelterDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Animal>>> GetAnimals()
        {
            return await _context.Animal
                .Include(a => a.TypeAnimal)
                .Include(a => a.AnimalStatus)
                .ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Animal>> GetAnimal(int id)
        {
            var animal = await _context.Animal
                .Include(a => a.TypeAnimal)
                .Include(a => a.AnimalStatus)
                .FirstOrDefaultAsync(a => a.Id == id);

            if (animal == null)
            {
                return NotFound();
            }

            return animal;
        }
    }
}
