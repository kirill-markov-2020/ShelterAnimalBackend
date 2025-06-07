using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelterAnimalBackend.Core.Entities
{
    public class Adoption
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public DateTime ApplicationDate { get; set; }

    }
}
    

