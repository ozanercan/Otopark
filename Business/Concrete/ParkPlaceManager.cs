using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.MySQL;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class ParkPlaceManager : IParkPlaceService
    {
        private IParkPlaceDal _parkPlaceDal;

        public ParkPlaceManager(IParkPlaceDal parkPlaceDal)
        {
            _parkPlaceDal = parkPlaceDal;
        }

        public List<ParkPlace> ListByNonDeleted()
        {
            return _parkPlaceDal.ListByNonDeleted();
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            _parkPlaceDal.ChangeIsDeleteField(Id, isDeleted);
        }

        public void MultipleCreate(List<ParkPlace> parameter)
        {
            _parkPlaceDal.MultipleCreate(parameter);
        }

        public void MultipleDelete(List<int> parameters)
        {
            _parkPlaceDal.MultipleDelete(parameters);
        }

        public void MultipleUpdate(List<ParkPlace> parameter)
        {
            _parkPlaceDal.MultipleUpdate(parameter);
        }

        public List<ParkPlace> List()
        {
            return _parkPlaceDal.List();
        }
    }
}