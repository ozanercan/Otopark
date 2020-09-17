using Entities.Abstract;

namespace Entities.Concrete
{
    public class Vehicle : IEntity
    {
        public int Id { get; set; }
        public int VehicleTypeId { get; set; }
        public int EmployeeId { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public string Plate { get; set; }
        public string Color { get; set; }
        public bool IsDeleted { get; set; }
    }
}