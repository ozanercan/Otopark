using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concrete
{
    public class DatabaseConnectionOperationManager : IDatabaseConnectionOperationService
    {
        private IDatabaseConnectionTestItem _databaseConnectionTestItem;
        private IDatabaseConnectionStringDal _databaseConnectionStringDal;
        public DatabaseConnectionOperationManager(IDatabaseConnectionTestItem databaseConnectionTestItem, IDatabaseConnectionStringDal databaseConnectionStringDal)
        {
            _databaseConnectionTestItem = databaseConnectionTestItem;
            _databaseConnectionStringDal = databaseConnectionStringDal;
        }

        public string GenerateConnectionString(DatabaseConnectionInformation databaseConnectionInformation)
        {
            return $"Server={databaseConnectionInformation.Hostname}; Database={databaseConnectionInformation.Database}; Uid={databaseConnectionInformation.Username}; Pwd={databaseConnectionInformation.Password};";
        }

        public string GetConnectionString()
        {
            return _databaseConnectionStringDal.GetConnectionString();
        }

        public void SetConnectionString(string connectionString)
        {
            _databaseConnectionStringDal.SetConnectionString(connectionString);
        }

        public bool Test(DatabaseConnectionInformation databaseConnectionInformation)
        {
            return _databaseConnectionTestItem.MakeTest(GenerateConnectionString(databaseConnectionInformation));
        }

        public bool Test(string connectionString)
        {
            return _databaseConnectionTestItem.MakeTest(connectionString);
        }

        public void Truncate()
        {
            _databaseConnectionStringDal.Truncate();
        }
    }
}
