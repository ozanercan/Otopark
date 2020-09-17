using DataAccess.Abstract.License;
using Entities.Concrete.License;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core;
namespace DataAccess.Concrete.SQLite.License.Adonet
{
    public class SQLiteLocalLicenseDal : ILocalLicenseDal
    {
        Connection con = new Connection();
        public int Create(ProjectLicenseCode projectLicenseCode)
        {
            int createdId = 0;
            con.con.Open();
            con.cmd.CommandText = "Insert into LicenseInformations(Id, ProjectId, LicenseCode, StartedDateTime, FinishedDateTime, IsDeleted) Values (@id, @projectid, @licensecode, @starteddatetime, @finisheddatetime, @isdeleted);";
            con.cmd.Parameters.AddWithValue("@id", projectLicenseCode.Id);
            con.cmd.Parameters.AddWithValue("@projectid", projectLicenseCode.ProjectId);
            con.cmd.Parameters.AddWithValue("@licensecode", projectLicenseCode.LicenseCode);
            con.cmd.Parameters.AddWithValue("@starteddatetime", projectLicenseCode.StartedDateTime);
            con.cmd.Parameters.AddWithValue("@finisheddatetime", projectLicenseCode.FinishedDateTime);
            con.cmd.Parameters.AddWithValue("@isdeleted", projectLicenseCode.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.cmd.CommandText = "Select last_insert_rowid();";
            con.dr = con.cmd.ExecuteReader();
            createdId = con.dr.Read() == true ? con.dr[0].ToInt32() : 0;
            con.dr.Close();
            con.con.Close();
            return createdId;
        }

        public void Delete(int Id)
        {
            con.con.Open();
            con.cmd.CommandText = "Delete From LicenseInformations where Id=@id;";
            con.cmd.Parameters.AddWithValue("@id", Id);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public ProjectLicenseCode GetALicense()
        {
            ProjectLicenseCode projectLicenseCode = null;
            con.con.Open();
            con.cmd.CommandText = "Select * From LicenseInformations;";
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                projectLicenseCode = new ProjectLicenseCode()
                {
                    Id = con.dr["Id"].ToInt32(),
                    ProjectId = con.dr["ProjectId"].ToInt32(),
                    LicenseCode = con.dr["LicenseCode"].ToString(),
                    StartedDateTime = con.dr["StartedDateTime"].ToDateTime(),
                    FinishedDateTime = con.dr["FinishedDateTime"].ToDateTime(),
                    IsDeleted = con.dr["IsDeleted"].ToBoolean()
                };
            }
            con.dr.Close();
            con.con.Close();
            return projectLicenseCode;
        }

        public ProjectLicenseCode GetById(int Id)
        {
            ProjectLicenseCode projectLicenseCode = null;
            con.con.Open();
            con.cmd.CommandText = "Select * From LicenseInformations where Id=@id;";
            con.cmd.Parameters.AddWithValue("@id", Id);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                projectLicenseCode = new ProjectLicenseCode()
                {
                    Id = con.dr["Id"].ToInt32(),
                    ProjectId = con.dr["ProjectId"].ToInt32(),
                    LicenseCode = con.dr["LicenseCode"].ToString(),
                    StartedDateTime = con.dr["StartedDateTime"].ToDateTime(),
                    FinishedDateTime = con.dr["FinishedDateTime"].ToDateTime(),
                    IsDeleted = con.dr["IsDeleted"].ToBoolean()
                };
            }
            con.dr.Close();
            con.con.Close();
            return projectLicenseCode;
        }

        public void Truncate()
        {
            con.con.Open();
            con.cmd.CommandText = "Delete From LicenseInformations;";
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public void Update(ProjectLicenseCode projectLicenseCode)
        {
            con.con.Open();
            con.cmd.CommandText = "Update LicenseInformations set ProjectId=@projectid, LicenseCode=@licensecode, StartedDateTime=@starteddatetime, FinishedDateTime=@finisheddatetime, IsDeleted=@isdeleted where Id=@id;";
            con.cmd.Parameters.AddWithValue("@id", projectLicenseCode.Id);
            con.cmd.Parameters.AddWithValue("@projectid", projectLicenseCode.ProjectId);
            con.cmd.Parameters.AddWithValue("@licensecode", projectLicenseCode.LicenseCode);
            con.cmd.Parameters.AddWithValue("@starteddatetime", projectLicenseCode.StartedDateTime);
            con.cmd.Parameters.AddWithValue("@finisheddatetime", projectLicenseCode.FinishedDateTime);
            con.cmd.Parameters.AddWithValue("@isdeleted", projectLicenseCode.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }
    }
}
