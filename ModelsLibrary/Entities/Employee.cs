using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShelterAnimalBackend.Core.Entities
{
    public class Employee
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public bool IsAdmin {get; set;}
        public DateTime HireDate { get; set; }
    }
}
