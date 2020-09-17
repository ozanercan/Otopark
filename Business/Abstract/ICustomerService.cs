using Entities.Concrete;
using Entities.Concrete.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface ICustomerService
    {
        List<CustomerDto> ListDto();
        List<CustomerDto> ListDtoByNonDeleted();
        List<Customer> List();
        int Create(Customer customer);
        Customer GetCustomerByPersonId(int PersonId);
        Customer Find(int Id);
        void Update(Customer customer);
    }
}