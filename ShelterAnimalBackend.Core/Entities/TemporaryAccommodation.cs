using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelterAnimalBackend.Core.Entities
{
    public class TemporaryAccommodation
    {
        public int Id { get; set; }
        public int VolunteerId { get; set; }
        public User User { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public DateTime DateAnimalCapture { get; set; }
        public DateTime DateAnimalReturn { get; set; }


    }
}
