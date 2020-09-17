using System;

namespace Entities.Abstract
{
    public abstract class APerson
    {
        public string NationalIdentityNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string TelephoneNumber { get; set; }
        public DateTime CreationDate { get; set; }
    }
}