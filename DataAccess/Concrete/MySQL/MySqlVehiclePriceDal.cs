using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.MySQL
{
    public class MySqlVehiclePriceDal : IVehiclePriceDal, IDalInstance
    {
        private readonly Connection con = new Connection();

        public int Create(VehiclePrice parameter)
        {
            int createdVehiclePriceId = 0;
            con.con.Open();
            con.cmd.CommandText = "INSERT INTO vehicleprices(VehicleTypeId, Hour, Price, IsDeleted) VALUES (@VehicleTypeId, @Hour, @Price, @isdeleted);";
            con.cmd.Parameters.AddWithValue("@VehicleTypeId", parameter.VehicleTypeId);
            con.cmd.Parameters.AddWithValue("@Hour", parameter.Hour);
            con.cmd.Parameters.AddWithValue("@Price", parameter.Price);
            con.cmd.Parameters.AddWithValue("@isdeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.cmd.CommandText = "Select LAST_INSERT_ID();";
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
                createdVehiclePriceId = Convert.ToInt32(con.dr[0]);
            con.con.Close();
            return createdVehiclePriceId;
        }

        public void Delete(int Id)
        {
            con.con.Open();
            con.cmd.CommandText = "Delete From VehiclePrices where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public VehiclePrice Find(int Id)
        {
            VehiclePrice searchedVehiclePrice = null;
            con.con.Open();
            con.cmd.CommandText = "Select * From VehiclePrices where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                searchedVehiclePrice = new VehiclePrice()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    VehicleTypeId = Convert.ToInt32(con.dr["VehicleTypeId"]),
                    Hour = Convert.ToInt32(con.dr["Hour"]),
                    Price = Convert.ToDouble(con.dr["Price"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                };
            }
            con.con.Close();
            return searchedVehiclePrice;
        }

        public VehiclePrice GetMaxPrice(int vehicleTypeId)
        {
            VehiclePrice maxPrice = null;
            con.con.Open();
            con.cmd.CommandText = "SELECT * From vehicleprices where VehicleTypeId=@VehicleTypeId ORDER BY Hour DESC LIMIT 1;";
            con.cmd.Parameters.AddWithValue("@VehicleTypeId", vehicleTypeId);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                maxPrice = new VehiclePrice()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    VehicleTypeId = Convert.ToInt32(con.dr["VehicleTypeId"]),
                    Hour = Convert.ToInt32(con.dr["Hour"]),
                    Price = Convert.ToDouble(con.dr["Price"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                };
            }
            con.con.Close();
            return maxPrice;
        }

        public List<VehiclePrice> List()
        {
            List<VehiclePrice> vehiclePrices = new List<VehiclePrice>();
            con.con.Open();
            con.cmd.CommandText = "Select * From VehiclePrices;";
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                vehiclePrices.Add(new VehiclePrice()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    VehicleTypeId = Convert.ToInt32(con.dr["VehicleTypeId"]),
                    Hour = Convert.ToInt32(con.dr["Hour"]),
                    Price = Convert.ToDouble(con.dr["Price"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                });
            }
            con.con.Close();
            return vehiclePrices;
        }

        public List<VehiclePrice> ListByNonDeleted()
        {
            List<VehiclePrice> vehiclePrices = new List<VehiclePrice>();
            con.con.Open();
            con.cmd.CommandText = "Select * From VehiclePrices where IsDeleted=@isdeleted;";
            con.cmd.Parameters.AddWithValue("@isdeleted", 0);
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                vehiclePrices.Add(new VehiclePrice()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    VehicleTypeId = Convert.ToInt32(con.dr["VehicleTypeId"]),
                    Hour = Convert.ToInt32(con.dr["Hour"]),
                    Price = Convert.ToDouble(con.dr["Price"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                });
            }
            con.con.Close();
            return vehiclePrices;
        }

        public List<VehiclePrice> ListByVehicleTypeId(int vehicleTypeId)
        {
            List<VehiclePrice> vehiclePrices = new List<VehiclePrice>();
            con.con.Open();
            con.cmd.CommandText = "Select * From VehiclePrices where VehicleTypeId=@VehicleTypeId;";
            con.cmd.Parameters.AddWithValue("@VehicleTypeId", vehicleTypeId);
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                vehiclePrices.Add(new VehiclePrice()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    VehicleTypeId = Convert.ToInt32(con.dr["VehicleTypeId"]),
                    Hour = Convert.ToInt32(con.dr["Hour"]),
                    Price = Convert.ToDouble(con.dr["Price"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                });
            }
            con.con.Close();
            return vehiclePrices;
        }

        public List<VehiclePrice> ListByVehicleTypeIdAndNonDeleted(int vehicleTypeId)
        {
            List<VehiclePrice> vehiclePrices = new List<VehiclePrice>();
            con.con.Open();
            con.cmd.CommandText = "Select * From VehiclePrices where VehicleTypeId=@VehicleTypeId and IsDeleted=@isdeleted;";
            con.cmd.Parameters.AddWithValue("@VehicleTypeId", vehicleTypeId);
            con.cmd.Parameters.AddWithValue("@isdeleted", 0);
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                vehiclePrices.Add(new VehiclePrice()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    VehicleTypeId = Convert.ToInt32(con.dr["VehicleTypeId"]),
                    Hour = Convert.ToInt32(con.dr["Hour"]),
                    Price = Convert.ToDouble(con.dr["Price"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                });
            }
            con.con.Close();
            return vehiclePrices;
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            con.con.Open();
            con.cmd.CommandText = "UPDATE vehicleprices SET IsDeleted=@isdeleted WHERE Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.Parameters.AddWithValue("@isdeleted", isDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public void Update(VehiclePrice parameter)
        {
            con.con.Open();
            con.cmd.CommandText = "UPDATE vehicleprices SET VehicleTypeId=@VehicleTypeId, Hour=@Hour, Price=@Price, IsDeleted=@isdeleted WHERE Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", parameter.Id);
            con.cmd.Parameters.AddWithValue("@VehicleTypeId", parameter.VehicleTypeId);
            con.cmd.Parameters.AddWithValue("@Hour", parameter.Hour);
            con.cmd.Parameters.AddWithValue("@Price", parameter.Price);
            con.cmd.Parameters.AddWithValue("@isdeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }
    }
}