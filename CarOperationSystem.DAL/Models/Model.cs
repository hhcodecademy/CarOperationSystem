using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarOperationSystem.DAL.Models
{
    public class Model:BaseEntity
    {
        public string Name { get; set; }
        public int BrandId { get; set; }
        public Brand Brand { get; set; }
        public ICollection<Car> Cars { get; set; }
    }
}
