using Microsoft.AspNetCore.Mvc;
using ShelterAnimalBackend.Application.Services;
using ShelterAnimalBackend.Core.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ShelterAnimalBackend.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdoptionApplicationController : ControllerBase
    {
        private readonly AdoptionApplicationService _adoptionApplicationService;

        public AdoptionApplicationController(AdoptionApplicationService adoptionApplicationService)
        {
            _adoptionApplicationService = adoptionApplicationService;
        }

        [HttpGet]
        public async Task<ActionResult<List<AdoptionApplication>>> GetAllApplications()
        {
            try
            {
                var applications = await _adoptionApplicationService.GetAllAsync();
                return Ok(applications);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Internal server error: {ex.Message}");
            }
        }
    }
}
