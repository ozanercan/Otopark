using Entities.Abstract;

namespace Entities.Concrete
{
    public class Person : APerson, IEntity
    {
        public int Id { get; set; }
    }
}