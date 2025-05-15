using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModelsLibrary
{
    public class Animal
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeAnimalId { get; set; }
        public TypeAnimal TypeAnimal { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public int AnimalStatusId { get; set; }
        public AnimalStatus AnimalStatus { get; set; }
        public string Description { get; set; }
        public string Photo { get; set; }
        
        

    }
}
