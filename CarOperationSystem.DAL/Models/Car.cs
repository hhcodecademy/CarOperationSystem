using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarOperationSystem.DAL.Models
{
    public class Car:BaseEntity
    {
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public int ModelId { get; set; }
        public Model Model { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int Milage { get; set; }
    }
}
