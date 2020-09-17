using Entities.Abstract;
using System;

namespace Entities.Concrete.Dtos
{
    public class EmployeeDto : APerson, IDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public DateTime? LastLoginDateTime { get; set; }
        public bool IsDeleted { get; set; }
    }
}