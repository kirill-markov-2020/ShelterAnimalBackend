using Microsoft.EntityFrameworkCore;
using ShelterAnimalBackend.Core.Entities;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelterAnimalBackend.Infrastructure.Data
{
    public class AnimalShelterDbContext : DbContext
    {
        public AnimalShelterDbContext(DbContextOptions<AnimalShelterDbContext> options) : base(options) { }
        public DbSet<User> User { get; set; }
        public DbSet<Animal> Animal { get; set; }
        public DbSet<AnimalStatus> AnimalStatus { get; set; }
        public DbSet<Adoption> Adoption { get; set; }
        public DbSet<AdoptionApplication> AdoptionApplication { get; set; }
        public DbSet<TypeAnimal> TypeAnimal { get; set; }
        public DbSet<StatusAdoption> StatusAdoption { get; set; }
        public DbSet<Employee> Employee { get; set; }
        public DbSet<TemporaryAccommodation> TemporaryAccommodation { get; set; }
        public DbSet<Role> Role { get; set; }

    }
}
