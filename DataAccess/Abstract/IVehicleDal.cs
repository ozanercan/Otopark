using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IVehicleDal : IBasicMethods<Vehicle>, IDeleteEntity<Vehicle>
    {
        bool ControlByPlate(string Plate);
    }
}