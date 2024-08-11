using CarOperationSystem.DAL.Models;

namespace CarOperationSystem.UI.Models
{
    public class CarVM
    {
        public int Id { get; set; }
        public int BrandId { get; set; }
        public string BrandName { get; set; }
        public List<Brand> Brands { get; set; }
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public List<Model> Models { get; set; }
        public int Year { get; set; }
        public string Color { get; set; }
        public int Milage { get; set; }
    }
}
