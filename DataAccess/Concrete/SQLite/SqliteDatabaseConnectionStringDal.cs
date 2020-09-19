using DataAccess.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQLite
{
    public class SqliteDatabaseConnectionStringDal : IDatabaseConnectionStringDal
    {
        Connection con = new Connection();
        public void Truncate()
        {
            con.con.Open();
            con.cmd.CommandText = "Delete From ConnectionStrings;";
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }
        public string GetConnectionString()
        {
            string connectionString = "";
            con.con.Open();
            con.cmd.CommandText = "Select * From ConnectionStrings;";
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
                connectionString = con.dr["ConnectionValue"].ToString();
            con.dr.Close();
            con.con.Close();
            return connectionString;
        }

        public void SetConnectionString(string connectionString)
        {
            this.Truncate();
            con.con.Open();
            con.cmd.CommandText = "Insert into ConnectionStrings(ConnectionValue) Values(@connectionvalue);";
            con.cmd.Parameters.AddWithValue("@connectionvalue", connectionString);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }
    }
}
