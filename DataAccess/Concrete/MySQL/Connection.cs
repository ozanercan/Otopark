using DataAccess.Abstract;
using MySqlConnector;

namespace DataAccess.Concrete.MySQL
{
    public class Connection
    {
        internal MySqlConnection con;
        internal MySqlCommand cmd;
        internal MySqlDataReader dr;

        //private string onlineConnectionQuery = "Server=ozanercan.com.tr;Uid=ozanercan;Pwd=07mekan07;Database=ozanerca_otopark;";
        //private string localConnectionQuery = "Server=localhost;Uid=root;Database=otopark;Pwd=;";
        private string _connectionString = "";
        public Connection()
        {
            Abstract.IDatabaseConnectionStringDal databaseConnectionStringDal = new SQLite.SqliteDatabaseConnectionStringDal();

            _connectionString = databaseConnectionStringDal.GetConnectionString();

            con = new MySqlConnection(_connectionString);
            cmd = new MySqlCommand() { Connection = con };
            con.StateChange += Con_StateChange;
        }

        private void Con_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            if (e.CurrentState == System.Data.ConnectionState.Closed && cmd != null)
                cmd.Parameters.Clear();
        }
    }
}