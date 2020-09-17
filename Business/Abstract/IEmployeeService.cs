using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using Entities.Concrete.Models;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IEmployeeService
    {
        List<Person> FindAllByPersonInformation();

        void UpdateLastLoginDateTime(Employee parameter);

        List<EmployeeDto> ListDto();
        List<EmployeeDto> ListDtoByNonDeleted();

        List<Employee> List();

        bool LoginControl(Login login);

        Employee FindEmployeeByUsernamendPassword(Login login);

        int Create(Employee employee);

        void Delete(int Id);

        void Update(Employee employee);

        Employee Find(int Id);
    }
}