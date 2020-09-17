using Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IPersonDal : IBasicMethods<Person>
    {
        List<Person> ListByParams(params int[] ids);
    }
}