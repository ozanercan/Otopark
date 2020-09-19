using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQLite
{
    internal class Connection
    {
        internal SQLiteConnection con;
        internal SQLiteCommand cmd;
        internal SQLiteDataReader dr;

        private string _connectionString = "Data Source=common.db;Version=3;";
        public Connection()
        {
            con = new SQLiteConnection(_connectionString);
            cmd = new SQLiteCommand() { Connection = con };
            con.StateChange += Con_StateChange;
        }

        private void Con_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            if (e.CurrentState == System.Data.ConnectionState.Closed && cmd != null)
                cmd.Parameters.Clear();
        }
    }
}
