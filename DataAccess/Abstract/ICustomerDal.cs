using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface ICustomerDal : IBasicMethods<Customer>, IDeleteEntity<Customer>
    {
        Customer GetCustomerByPersonId(int PersonId);
    }
}