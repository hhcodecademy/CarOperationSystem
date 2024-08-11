using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarOperationSystem.DAL.Models
{
    public class Brand:BaseEntity
    {
        public string Name { get; set; }
        public ICollection<Model> Models { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
