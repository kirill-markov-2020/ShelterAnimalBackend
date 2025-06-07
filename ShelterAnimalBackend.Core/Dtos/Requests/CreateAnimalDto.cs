using System.ComponentModel.DataAnnotations;

namespace ShelterAnimalBackend.Core.Dtos.Requests
{
    public class CreateAnimalDto
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public int TypeAnimalId { get; set; }

        [Required]
        [StringLength(20)]
        public string Gender { get; set; }

        [Required]
        [Range(0, 100)]
        public int Age { get; set; }

        [Required]
        public int AnimalStatusId { get; set; }

        public string Description { get; set; }

        public string Photo { get; set; }
    }
}