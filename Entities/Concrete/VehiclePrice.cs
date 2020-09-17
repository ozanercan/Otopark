using Entities.Abstract;

namespace Entities.Concrete
{
    public class VehiclePrice : IEntity
    {
        public int Id { get; set; }
        public int VehicleTypeId { get; set; }
        public int Hour { get; set; }
        public double Price { get; set; }
        public bool IsDeleted { get; set; }
    }
}