using DataAccess.Abstract;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Abstract
{
    public interface IDatabaseConnectionOperationService : IDatabaseConnectionStringDal
    {
        bool Test(DatabaseConnectionInformation databaseConnectionInformation);
        bool Test(string connectionString);
        string GenerateConnectionString(DatabaseConnectionInformation databaseConnectionInformation);
    }
}
