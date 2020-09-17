using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IBasicMethods<IEntity> where IEntity : class, new()
    {
        IEntity Find(int Id);

        int Create(IEntity parameter);

        void Update(IEntity parameter);

        void Delete(int Id);

        List<IEntity> List();
    }
}