using Entities.Abstract;

namespace Entities.Concrete.Dtos
{
    public class VehicleDto : IDto
    {
        public int Id { get; set; }
        public string VehicleType { get; set; }
        public string Employee { get; set; }
        public string Plate { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string Color { get; set; }
        public bool IsDeleted { get; set; }
    }
}