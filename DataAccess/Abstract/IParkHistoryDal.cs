using Entities.Concrete;
using System.Collections.Generic;
using System.Windows.Documents;

namespace DataAccess.Abstract
{
    public interface IParkHistoryDal : IBasicMethods<ParkHistory>, IDeleteEntity<ParkHistory>
    {
        List<ParkHistory> List();
    }
}