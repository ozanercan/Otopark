using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.MySQL;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class PersonManager : IPersonService
    {
        private IPersonDal _personDal;

        public PersonManager(IPersonDal personDal)
        {
            _personDal = personDal;
        }

        public int Create(Person person)
        {
            return _personDal.Create(person);
        }

        public void Delete(int Id)
        {
            _personDal.Delete(Id);
        }

        public Person Find(int Id)
        {
            return _personDal.Find(Id);
        }

        public List<Person> List()
        {
            return _personDal.List();
        }

        public List<Person> ListByParams(params int[] ids)
        {
            return _personDal.ListByParams(ids);
        }

        public void Update(Person person)
        {
            _personDal.Update(person);
        }
    }
}