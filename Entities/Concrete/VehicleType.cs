using Entities.Abstract;

namespace Entities.Concrete
{
    public class VehicleType : IEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsDeleted { get; set; }
    }
}