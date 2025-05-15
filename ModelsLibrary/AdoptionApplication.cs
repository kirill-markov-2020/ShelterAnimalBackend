using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestLibrary.Models
{
    public class AdoptionApplication
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public User User { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public int StatusAdoptionId { get; set; }
        public StatusAdoption StatusAdoption { get; set; }
        public DateTime ApplicationDate { get; set; }
        public string Description { get; set; }
    }
}
