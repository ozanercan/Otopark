using Entities.Abstract;

namespace Entities.Concrete.Dtos
{
    public class CustomerDto : APerson, IDto
    {
        public int Id { get; set; }
        public bool IsDeleted { get; set; }
    }
}