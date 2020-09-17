using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete.MySQL;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Business.Concrete
{
    public class VehiclePriceManager : IVehiclePriceService
    {
        private IVehiclePriceDal _vehiclePriceDal;

        private IVehicleTypeService _vehicleTypeService;

        public VehiclePriceManager(IVehiclePriceDal vehiclePriceDal, IVehicleTypeService vehicleTypeService)
        {
            _vehiclePriceDal = vehiclePriceDal;
            _vehicleTypeService = vehicleTypeService;
        }

        public VehiclePrice Calculate(int vehicleTypeId, DateTime parkingStartDateTime)
        {
            List<VehiclePrice> vehiclePrices = ListByVehicleTypeId(vehicleTypeId);

            VehiclePrice vehiclePrice = null;

            foreach (var item in vehiclePrices)
            {
                DateTime actualDateTime = DateTime.Now;
                actualDateTime.Date.Add(new TimeSpan(item.Hour, 0, 0));

                TimeSpan timedifference = actualDateTime - parkingStartDateTime;
                if (item.Hour > timedifference.TotalHours)
                {
                    vehiclePrice = item;
                    break;
                }
            }
            // Saat Bazlı Ödeme İşlemi Bulunamazsa Max fiyatı çeker.
            //if (vehiclePrice == null)
            //{
            //    vehiclePrice = this.GetMaxPrice(vehicleTypeId);
            //}

            return vehiclePrice;
        }

        public int Create(VehiclePrice vehiclePrice)
        {
            return _vehiclePriceDal.Create(vehiclePrice);
        }

        public void Delete(int Id)
        {
            _vehiclePriceDal.Delete(Id);
        }

        public VehiclePrice GetMaxPrice(int vehicleTypeId)
        {
            return _vehiclePriceDal.GetMaxPrice(vehicleTypeId);
        }

        public List<VehiclePrice> List()
        {
            return _vehiclePriceDal.List();
        }

        public List<VehiclePrice> ListByNonDeleted()
        {
            return _vehiclePriceDal.ListByNonDeleted();
        }

        public List<VehiclePrice> ListByVehicleTypeId(int vehicleTypeId)
        {
            return _vehiclePriceDal.ListByVehicleTypeId(vehicleTypeId);
        }

        public List<VehiclePrice> ListByVehicleTypeIdAndNonDeleted(int vehicleTypeId)
        {
            return _vehiclePriceDal.ListByVehicleTypeIdAndNonDeleted(vehicleTypeId);
        }

        public List<VehiclePriceDto> ListDto()
        {
            var vehiclePrices = this.List();

            var vehicleTypes = _vehicleTypeService.List();

            var vehiclePriceDtos = new List<VehiclePriceDto>();

            foreach (var vehiclePrice in vehiclePrices)
            {
                vehiclePriceDtos.Add(new VehiclePriceDto()
                {
                    Id = vehiclePrice.Id,
                    VehicleTypeName = vehicleTypes.Where(i => i.Id == vehiclePrice.VehicleTypeId).FirstOrDefault().Name,
                    Hour = vehiclePrice.Hour,
                    Price = vehiclePrice.Price,
                    IsDeleted = vehiclePrice.IsDeleted
                });
            }

            return vehiclePriceDtos;
        }

        public List<VehiclePriceDto> ListDtoByNonDeleted()
        {
            var vehiclePrices = this.ListByNonDeleted();

            var vehicleTypes = _vehicleTypeService.List();

            var vehiclePriceDtos = new List<VehiclePriceDto>();

            foreach (var vehiclePrice in vehiclePrices)
            {
                vehiclePriceDtos.Add(new VehiclePriceDto()
                {
                    Id = vehiclePrice.Id,
                    VehicleTypeName = vehicleTypes.Where(i => i.Id == vehiclePrice.VehicleTypeId).FirstOrDefault().Name,
                    Hour = vehiclePrice.Hour,
                    Price = vehiclePrice.Price,
                    IsDeleted = vehiclePrice.IsDeleted
                });
            }

            return vehiclePriceDtos;
        }

        public void Update(VehiclePrice vehiclePrice)
        {
            _vehiclePriceDal.Update(vehiclePrice);
        }
    }
}