using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ModelsLibrary;

namespace ModelsLibrary
{
    public class Adoption
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public User User { get; set; }
        public int AnimalId { get; set; }
        public Animal Animal { get; set; }
        public DateTime ApplicationDate { get; set; }
        public int EmployeeId { get; set; }

    }
}
    

