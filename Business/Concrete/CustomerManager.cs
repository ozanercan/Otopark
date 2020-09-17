using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.MySQL;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class CustomerManager : ICustomerService
    {
        private ICustomerDal _customerDal;
        private IPersonService _personService;

        public CustomerManager(ICustomerDal customerDal, IPersonService personService)
        {
            _customerDal = customerDal;
            _personService = personService;
        }
        public Customer GetCustomerByPersonId(int PersonId)
        {
            return _customerDal.GetCustomerByPersonId(PersonId);
        }

        public List<Customer> ListByNonDeleted()
        {
            return _customerDal.ListByNonDeleted();
        }

        public List<CustomerDto> ListDto()
        {
            var customers = this.List();
            var persons = _personService.List();
            var customerDtos = new List<CustomerDto>();

            foreach (var customer in customers)
            {
                foreach (var person in persons)
                {
                    if (customer.PersonId == person.Id)
                    {
                        customerDtos.Add(new CustomerDto()
                        {
                            Id = customer.Id,
                            FirstName = person.FirstName,
                            LastName = person.LastName,
                            CreationDate = person.CreationDate,
                            NationalIdentityNumber = person.NationalIdentityNumber,
                            TelephoneNumber = person.TelephoneNumber,
                            IsDeleted = customer.IsDeleted
                        });
                    }
                }
            }

            return customerDtos;
        }

        public List<CustomerDto> ListDtoByNonDeleted()
        {
            var customers = this.ListByNonDeleted();
            var persons = _personService.List();
            var customerDtos = new List<CustomerDto>();

            foreach (var customer in customers)
            {
                foreach (var person in persons)
                {
                    if (customer.PersonId == person.Id)
                    {
                        customerDtos.Add(new CustomerDto()
                        {
                            Id = customer.Id,
                            FirstName = person.FirstName,
                            LastName = person.LastName,
                            CreationDate = person.CreationDate,
                            NationalIdentityNumber = person.NationalIdentityNumber,
                            TelephoneNumber = person.TelephoneNumber,
                            IsDeleted = customer.IsDeleted
                        });
                    }
                }
            }

            return customerDtos;
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            _customerDal.ChangeIsDeleteField(Id, isDeleted);
        }

        public List<Customer> List()
        {
            return _customerDal.List();
        }

        public int Create(Customer customer)
        {
            return _customerDal.Create(customer);
        }

        public Customer Find(int Id)
        {
            return _customerDal.Find(Id);
        }

        public void Update(Customer customer)
        {
            _customerDal.Update(customer);
        }
    }
}