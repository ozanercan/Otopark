using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IParkService
    {
        List<ParkDto> ListDto();
        List<ParkDto> ListDtoByNonDeleted();
        List<Park> List();
        int Create(Park park);
        void Update(Park park);
        void Delete(int Id);

        Park GetByParkPlaceIdandNonDeleted(int ParkPlaceId);
    }
}