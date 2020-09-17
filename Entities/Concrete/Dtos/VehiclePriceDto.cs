using Entities.Abstract;

namespace Entities.Concrete.Dtos
{
    public class VehiclePriceDto : IDto
    {
        public int Id { get; set; }
        public string VehicleTypeName { get; set; }
        public int Hour { get; set; }
        public double Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}