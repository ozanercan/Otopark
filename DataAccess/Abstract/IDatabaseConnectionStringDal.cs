using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IDatabaseConnectionStringDal
    {
        void Truncate();
        void SetConnectionString(string connectionString);
        string GetConnectionString();
    }
}
