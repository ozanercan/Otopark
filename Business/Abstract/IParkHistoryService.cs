using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Dtos;
using System.Collections.Generic;

namespace Business.Abstract
{
    public interface IParkHistoryService
    {
        List<ParkHistoryDto> ListDto();
        List<ParkHistoryDto> ListDtoByNonDeleted();

        int Create(ParkHistory parkHistory);

        List<ParkHistory> List();
        List<ParkHistory> ListByNonDeleted();
    }
}