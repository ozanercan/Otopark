using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SQLite;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concrete.SQLite.License.Adonet
{
    public class Connection
    {
        internal SQLiteConnection con;
        internal SQLiteCommand cmd;
        internal SQLiteDataReader dr;

        public Connection()
        {
            string connectionString = $@"Data Source={Environment.CurrentDirectory}\license.db;Version=3;Password=07mekan07;";
            con = new SQLiteConnection(connectionString);
            cmd = new SQLiteCommand() { Connection = con };

            con.StateChange += Con_StateChange;
        }

        private void Con_StateChange(object sender, System.Data.StateChangeEventArgs e)
        {
            if (cmd != null)
                cmd.Parameters.Clear();
        }
    }
}
