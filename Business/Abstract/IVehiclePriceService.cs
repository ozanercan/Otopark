using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IVehiclePriceService
    {
        VehiclePrice Calculate(int vehicleTypeId, DateTime parkingStartDateTimeparameter);

        List<VehiclePriceDto> ListDto();
        List<VehiclePriceDto> ListDtoByNonDeleted();

        VehiclePrice GetMaxPrice(int vehicleTypeId);

        void Delete(int Id);
        int Create(VehiclePrice vehiclePrice);
        void Update(VehiclePrice vehiclePrice);
        List<VehiclePrice> List();
        List<VehiclePrice> ListByNonDeleted();
    }
}