using DataAccess.Abstract;
using Entities.Concrete;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IVehicleTypeService
    {
        int FindIdByVehicleName(List<VehicleType> vehicleTypes, string Name);
        List<VehicleType> List();
        int Create(VehicleType vehicleType);
        List<VehicleType> ListByNonDeleted();
        void Delete(int Id);
        void Update(VehicleType vehicleType);
        void ChangeIsDeletedByVehicleTypeId(int Id);
        void ChangeIsNonDeletedByVehicleTypeId(int Id);
    }
}