using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IVehicleTypeDal : IBasicMethods<VehicleType>, IDeleteEntity<VehicleType>
    {
    }
}