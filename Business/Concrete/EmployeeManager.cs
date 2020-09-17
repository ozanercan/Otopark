using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.MySQL;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class EmployeeManager : IEmployeeService
    {
        private IEmployeeDal _employeeDal;
        private IPersonService _personService;

        public EmployeeManager(IEmployeeDal employeeDal, IPersonService personService)
        {
            _employeeDal = employeeDal;
            _personService = personService;
        }
        public List<Person> FindAllByPersonInformation()
        {
            List<Employee> employees = this.List();

            int[] ids = new int[employees.Count];
            for (int i = 0; i < employees.Count; i++)
            {
                ids[i] = employees[i].Id;
            }

            return _personService.ListByParams(ids);
        }

        public Employee FindEmployeeByUsernamendPassword(Login parameter)
        {
            return _employeeDal.FindEmployeeByUsernamendPassword(parameter);
        }

        public List<Employee> ListByNonDeleted()
        {
            return _employeeDal.ListByNonDeleted();
        }

        public List<EmployeeDto> ListDto()
        {
            var employees = this.List();
            var persons = _personService.List();
            var employeeDtos = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                foreach (var person in persons)
                {
                    if (employee.PersonId == person.Id)
                    {
                        employeeDtos.Add(new EmployeeDto()
                        {
                            Id = employee.Id,
                            FirstName = person.FirstName,
                            LastName = person.LastName,
                            CreationDate = person.CreationDate,
                            TelephoneNumber = person.TelephoneNumber,
                            NationalIdentityNumber = person.NationalIdentityNumber,
                            UserName = employee.UserName,
                            Password = employee.Password,
                            LastLoginDateTime = employee.LastLoginDateTime,
                            IsDeleted = employee.IsDeleted
                        });
                    }
                }
            }
            return employeeDtos;
        }

        public bool LoginControl(Login parameter)
        {
            return _employeeDal.LoginControl(parameter);
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            _employeeDal.ChangeIsDeleteField(Id, isDeleted);
        }

        public void UpdateLastLoginDateTime(Employee parameter)
        {
            parameter.LastLoginDateTime = DateTime.Now;
            this.Update(parameter);
        }

        public List<Employee> List()
        {
            return _employeeDal.List();
        }

        public int Create(Employee employee)
        {
            return _employeeDal.Create(employee);
        }

        public void Delete(int Id)
        {
            _employeeDal.Delete(Id);
        }

        public void Update(Employee employee)
        {
            _employeeDal.Update(employee);
        }

        public Employee Find(int Id)
        {
            return _employeeDal.Find(Id);
        }

        public List<EmployeeDto> ListDtoByNonDeleted()
        {
            var employees = this.ListByNonDeleted();
            var persons = _personService.List();
            var employeeDtos = new List<EmployeeDto>();

            foreach (var employee in employees)
            {
                foreach (var person in persons)
                {
                    if (employee.PersonId == person.Id)
                    {
                        employeeDtos.Add(new EmployeeDto()
                        {
                            Id = employee.Id,
                            FirstName = person.FirstName,
                            LastName = person.LastName,
                            CreationDate = person.CreationDate,
                            TelephoneNumber = person.TelephoneNumber,
                            NationalIdentityNumber = person.NationalIdentityNumber,
                            UserName = employee.UserName,
                            Password = employee.Password,
                            LastLoginDateTime = employee.LastLoginDateTime,
                            IsDeleted = employee.IsDeleted
                        });
                    }
                }
            }
            return employeeDtos;
        }
    }
}