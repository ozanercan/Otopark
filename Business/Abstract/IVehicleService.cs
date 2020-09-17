using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IVehicleService
    {
        List<VehicleDto> ListDto();
        List<VehicleDto> ListDtoByNonDeleted();

        List<Vehicle> List();

        int Create(Vehicle vehicle);
        bool ControlByPlate(string plate);
        void Update(Vehicle vehicle);
        void Delete(int Id);
        Vehicle Find(int Id);
        List<Vehicle> ListByNonDeleted();

        void ChangeIsDeletedByVehicleId(int Id);
        void ChangeIsNonDeletedByVehicleId(int Id);
    }
}