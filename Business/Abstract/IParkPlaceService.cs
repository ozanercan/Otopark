using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IParkPlaceService
    {
        List<ParkPlace> List();
        List<ParkPlace> ListByNonDeleted();
        void MultipleCreate(List<ParkPlace> parameter);
        void MultipleDelete(List<int> parameters);
        void MultipleUpdate(List<ParkPlace> parameter);
        void ChangeIsDeleteField(int Id, bool isDeleted);
    }
}