using ShelterAnimalBackend.Core.Entities;
using System.ComponentModel.DataAnnotations;

public class User
{
    [Required]
    public int Id { get; set; }
    [Required]
    public string Surname { get; set; }

    [Required]
    public string Name { get; set; }

    public string Patronymic { get; set; }

    [Required, EmailAddress]
    public string Email { get; set; }

    [Required, Phone]
    public string Phone { get; set; }

    [Required, MinLength(5)]
    public string Login { get; set; }

    [Required, MinLength(6)]
    public string Password { get; set; }

    [Required]
    public int RoleId { get; set; }
    public Role Role { get; set; }
}