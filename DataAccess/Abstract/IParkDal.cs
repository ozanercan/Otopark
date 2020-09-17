using Entities.Concrete;

namespace DataAccess.Abstract
{
    public interface IParkDal : IBasicMethods<Park>, IDeleteEntity<Park>
    {
        /// <summary>
        /// Park'ı Park Alanı Id'ye göre ve Silinmeme Durumuna göre getirir.
        /// </summary>
        Park GetByParkPlaceIdandNonDeleted(int ParkPlaceId);

        Park Control(int ParkPlaceId, int VehicleId, bool State);
    }
}