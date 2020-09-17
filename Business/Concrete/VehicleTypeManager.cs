using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.MySQL;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Concrete
{
    public class VehicleTypeManager : IVehicleTypeService
    {
        private IVehicleTypeDal _vehicleTypeDal;
        public VehicleTypeManager(IVehicleTypeDal vehicleTypeDal)
        {
            _vehicleTypeDal = vehicleTypeDal;
        }

        public void ChangeIsDeletedByVehicleTypeId(int Id)
        {
            _vehicleTypeDal.ChangeIsDeleteField(Id, true);
        }

        public void ChangeIsNonDeletedByVehicleTypeId(int Id)
        {
            _vehicleTypeDal.ChangeIsDeleteField(Id, false);

        }

        public int Create(VehicleType vehicleType)
        {
            return _vehicleTypeDal.Create(vehicleType);
        }

        public void Delete(int Id)
        {
            _vehicleTypeDal.Delete(Id);
        }

        public int FindIdByVehicleName(List<VehicleType> vehicleTypes, string Name)
        {
            int result = -1;
            foreach (var item in vehicleTypes)
            {
                if (item.Name.Equals(Name) == true)
                {
                    result = item.Id;
                    break;
                }
            }
            return result;
        }

        public List<VehicleType> List()
        {
            return _vehicleTypeDal.List();
        }

        public List<VehicleType> ListByNonDeleted()
        {
            return _vehicleTypeDal.ListByNonDeleted();
        }

        public void Update(VehicleType vehicleType)
        {
            _vehicleTypeDal.Update(vehicleType);
        }
    }
}