using Entities.Abstract;
using System;

namespace Entities.Concrete
{
    public class Employee : IEntity
    {
        public int Id { get; set; }
        public int PersonId { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}