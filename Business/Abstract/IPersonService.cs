using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IPersonService
    {
        List<Person> ListByParams(params int[] ids);
        List<Person> List();
        int Create(Person person);
        void Update(Person person);
        void Delete(int Id);
        Person Find(int Id);
    }
}