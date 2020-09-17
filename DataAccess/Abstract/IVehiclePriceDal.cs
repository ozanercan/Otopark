using Entities.Concrete;
using System.Collections.Generic;

namespace DataAccess.Abstract
{
    public interface IVehiclePriceDal : IBasicMethods<VehiclePrice>, IDeleteEntity<VehiclePrice>
    {
        /// <summary>
        /// Taşıt tipine göre maksimum fiyatlandırmayi geri döndürür.
        /// </summary>
        VehiclePrice GetMaxPrice(int vehicleTypeId);

        /// <summary>
        /// Taşıt Id'ye göre Fiyatlandırma Listesini geriye döndürür.
        /// </summary>
        List<VehiclePrice> ListByVehicleTypeId(int vehicleTypeId);

        /// <summary>
        /// Taşıt Id'ye ve Silinmeme Durumuna göre Fiyatlandırma Listesini geriye döndürür.
        /// </summary>
        List<VehiclePrice> ListByVehicleTypeIdAndNonDeleted(int vehicleTypeId);

    }
}