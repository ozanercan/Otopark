using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using Core;
namespace DataAccess.Concrete.MySQL
{
    public class MySqlVehicleTypeDal : IVehicleTypeDal, IDalInstance
    {
        private Connection con = new Connection();

        public int Create(VehicleType parameter)
        {
            int createdVehicleId = 0;
            con.con.Open();
            con.cmd.CommandText = "INSERT INTO vehicletypes(Name, IsDeleted) VALUES (@Name, @isdeleted);";
            con.cmd.Parameters.AddWithValue("@Name", parameter.Name);
            con.cmd.Parameters.AddWithValue("@isdeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.cmd.CommandText = "Select LAST_INSERT_ID();";
            con.dr = con.cmd.ExecuteReader();
            createdVehicleId = con.dr.Read() ? con.dr[0].ToInt32() : 0;
            con.con.Close();
            return createdVehicleId;
        }

        public void Delete(int Id)
        {
            con.con.Open();
            con.cmd.CommandText = "Delete From VehicleTypes where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public VehicleType Find(int Id)
        {
            VehicleType vehicleType = null;
            con.con.Open();
            con.cmd.CommandText = "Select * From VehicleTypes where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                vehicleType = new VehicleType()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    Name = con.dr["Name"].ToString(),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                };
            }
            con.con.Close();
            return vehicleType;
        }

        public List<VehicleType> List()
        {
            List<VehicleType> vehicleTypes = new List<VehicleType>();
            con.con.Open();
            con.cmd.CommandText = "Select * From VehicleTypes;";
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                vehicleTypes.Add(new VehicleType()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    Name = con.dr["Name"].ToString(),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                });
            }
            con.con.Close();
            return vehicleTypes;
        }

        public List<VehicleType> ListByNonDeleted()
        {
            List<VehicleType> vehicleTypes = new List<VehicleType>();
            con.con.Open();
            con.cmd.CommandText = "Select * From VehicleTypes where IsDeleted=@isdeleted;";
            con.cmd.Parameters.AddWithValue("@isdeleted", 0);
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                vehicleTypes.Add(new VehicleType()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    Name = con.dr["Name"].ToString(),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                });
            }
            con.con.Close();
            return vehicleTypes;
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            con.con.Open();
            con.cmd.CommandText = "Update VehicleTypes Set IsDeleted=@isdeleted where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.Parameters.AddWithValue("@isdeleted", isDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public void Update(VehicleType parameter)
        {
            con.con.Open();
            con.cmd.CommandText = "Update VehicleTypes Set Name=@Name, IsDeleted=@isdeleted where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", parameter.Id);
            con.cmd.Parameters.AddWithValue("@Name", parameter.Name);
            con.cmd.Parameters.AddWithValue("@isdeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }
    }
}