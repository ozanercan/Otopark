using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.MySQL
{
    public class MySqlVehicleDal : IVehicleDal, IDalInstance
    {
        private Connection con = new Connection();

        public bool ControlByPlate(string Plate)
        {
            bool result = false;
            con.con.Open();
            con.cmd.CommandText = "Select * From Vehicles where Plate = @Plate;";
            con.cmd.Parameters.AddWithValue("@Plate", Plate);
            con.dr = con.cmd.ExecuteReader();
            result = con.dr.Read() ? true : false;
            con.con.Close();
            return result;
        }

        public int Create(Vehicle parameter)
        {
            int createdId = 0;
            con.con.Open();
            con.cmd.CommandText = "INSERT INTO Vehicles ( VehicleTypeId, EmployeeId, BrandId, ModelId, Plate, Color, IsDeleted ) VALUES ( @VehicleTypeId, @EmployeeId, @BrandId, @ModelId, @Plate, @Color, @isdeleted );";
            con.cmd.Parameters.AddWithValue("@VehicleTypeId", parameter.VehicleTypeId);
            con.cmd.Parameters.AddWithValue("@EmployeeId", parameter.EmployeeId);
            con.cmd.Parameters.AddWithValue("@Plate", parameter.Plate);
            con.cmd.Parameters.AddWithValue("@BrandId", parameter.BrandId);
            con.cmd.Parameters.AddWithValue("@ModelId", parameter.ModelId);
            con.cmd.Parameters.AddWithValue("@Color", parameter.Color);
            con.cmd.Parameters.AddWithValue("@isdeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.cmd.CommandText = "Select LAST_INSERT_ID();";
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
                createdId = Convert.ToInt32(con.dr[0]);
            con.con.Close();
            return createdId;
        }

        public void Delete(int Id)
        {
            con.con.Open();
            con.cmd.CommandText = "Delete From Vehicles where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public Vehicle Find(int Id)
        {
            Vehicle vehicle = null;
            con.con.Open();
            con.cmd.CommandText = "Select* From Vehicles where Id = @Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                vehicle = new Vehicle()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    VehicleTypeId = Convert.ToInt32(con.dr["VehicleTypeId"]),
                    EmployeeId = Convert.ToInt32(con.dr["EmployeeId"]),
                    BrandId = Convert.ToInt32(con.dr["BrandId"]),
                    ModelId = Convert.ToInt32(con.dr["ModelId"]),
                    Plate = con.dr["Plate"].ToString(),
                    Color = con.dr["Color"].ToString(),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                };
            }
            con.con.Close();
            return vehicle;
        }

        public List<Vehicle> List()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            con.con.Open();
            con.cmd.CommandText = "Select * From Vehicles;";
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                vehicles.Add(new Vehicle()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    VehicleTypeId = Convert.ToInt32(con.dr["VehicleTypeId"]),
                    EmployeeId = Convert.ToInt32(con.dr["EmployeeId"]),
                    BrandId = Convert.ToInt32(con.dr["BrandId"]),
                    ModelId = Convert.ToInt32(con.dr["ModelId"]),
                    Plate = con.dr["Plate"].ToString(),
                    Color = con.dr["Color"].ToString(),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                });
            }
            con.con.Close();
            return vehicles;
        }

        public List<Vehicle> ListByNonDeleted()
        {
            List<Vehicle> vehicles = new List<Vehicle>();
            con.con.Open();
            con.cmd.CommandText = "Select * From Vehicles where IsDeleted=@isdeleted;";
            con.cmd.Parameters.AddWithValue("@isdeleted", 0);
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                vehicles.Add(new Vehicle()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    VehicleTypeId = Convert.ToInt32(con.dr["VehicleTypeId"]),
                    EmployeeId = Convert.ToInt32(con.dr["EmployeeId"]),
                    BrandId = Convert.ToInt32(con.dr["BrandId"]),
                    ModelId = Convert.ToInt32(con.dr["ModelId"]),
                    Plate = con.dr["Plate"].ToString(),
                    Color = con.dr["Color"].ToString(),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                });
            }
            con.con.Close();
            return vehicles;
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            con.con.Open();
            con.cmd.CommandText = "Update Vehicles Set IsDeleted=@isdeleted where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.Parameters.AddWithValue("@isdeleted", isDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public void Update(Vehicle parameter)
        {
            con.con.Open();
            con.cmd.CommandText = "Update Vehicles Set VehicleTypeId = @VehicleTypeId, EmployeeId=@EmployeeId, Plate = @Plate, BrandId = @BrandId, ModelId = @ModelId, Color = @Color, IsDeleted=@isdeleted where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", parameter.Id);
            con.cmd.Parameters.AddWithValue("@VehicleTypeId", parameter.VehicleTypeId);
            con.cmd.Parameters.AddWithValue("@EmployeeId", parameter.EmployeeId);
            con.cmd.Parameters.AddWithValue("@Plate", parameter.Plate);
            con.cmd.Parameters.AddWithValue("@BrandId", parameter.BrandId);
            con.cmd.Parameters.AddWithValue("@ModelId", parameter.ModelId);
            con.cmd.Parameters.AddWithValue("@Color", parameter.Color);
            con.cmd.Parameters.AddWithValue("@isdeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }
    }
}