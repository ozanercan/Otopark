using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Abstract
{
    public interface IDatabaseConnectionTestItem
    {
        bool MakeTest(string connectionString);
    }
}
