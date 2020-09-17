using Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IParkPlaceDal : IBasicMethods<ParkPlace>, IDeleteEntity<ParkPlace>
    {
        void MultipleCreate(List<ParkPlace> parameter);

        void MultipleUpdate(List<ParkPlace> parameter);

        void MultipleDelete(List<int> parameters);

    }
}