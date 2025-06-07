using ShelterAnimalBackend.Core.Entities;
using System.ComponentModel.DataAnnotations;

public class Animal
{
    public int Id { get; set; }

    [Required]
    [StringLength(100)]
    public string Name { get; set; }

    [Required]
    public int TypeAnimalId { get; set; }
    public TypeAnimal TypeAnimal { get; set; }

    [Required]
    [StringLength(20)]
    public string Gender { get; set; }

    [Required]
    [Range(0, 100)]
    public int Age { get; set; }

    [Required]
    public int AnimalStatusId { get; set; }
    public AnimalStatus AnimalStatus { get; set; }

    public string Description { get; set; }

    public string Photo { get; set; }
}