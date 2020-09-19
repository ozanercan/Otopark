using DataAccess.Abstract;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace DataAccess.Concrete.MySQL
{
    public class MySqlConnectionTestDal : IDatabaseConnectionTestItem
    {
        Connection con = new Connection();
        public bool MakeTest(string connectionString)
        {
            try
            {
                bool isConnectionOpen = false;
                con.con.ConnectionString = connectionString;
                con.con.Open();
                isConnectionOpen = (con.con.State == System.Data.ConnectionState.Open) ? true : false;
                con.con.Close();
                return isConnectionOpen;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
