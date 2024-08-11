using CarOperationSystem.DAL.Models;

namespace CarOperationSystem.UI.Models
{
    public class ModelVM
    {
        public int Id { get; set; }
        public string Name{ get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }

        public List<Brand> Brands { get; set; }

    }
}
