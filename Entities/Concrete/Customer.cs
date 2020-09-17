using Entities.Abstract;

namespace Entities.Concrete
{
    public class Customer : IEntity
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public bool IsDeleted { get; set; }
    }
}