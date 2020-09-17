using Core;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.MySQL
{
    public class MySqlParkDal : IParkDal, IDalInstance
    {
        private Connection con = new Connection();

        public Park GetByParkPlaceIdandNonDeleted(int ParkPlaceId)
        {
            Park park = null;
            con.con.Open();
            con.cmd.CommandText = "Select * From Parks where ParkPlaceId=@ParkPlaceId and IsDeleted=@isdeleted;";
            con.cmd.Parameters.AddWithValue("@ParkPlaceId", ParkPlaceId);
            con.cmd.Parameters.AddWithValue("@isdeleted", 0);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                park = new Park()
                {
                    Id = con.dr["Id"].ToInt32(),
                    ParkPlaceId = con.dr["ParkPlaceId"].ToInt32(),
                    CampaignId = (con.dr["CampaignId"] != DBNull.Value) ? con.dr["CampaignId"].ToInt32() : (int?)null,
                    CustomerId = (con.dr["CustomerId"] != DBNull.Value) ? con.dr["CustomerId"].ToInt32() : (int?)null,
                    VehicleId = (con.dr["VehicleId"] != DBNull.Value) ? con.dr["VehicleId"].ToInt32() : (int?)null,
                    EmployeeId = con.dr["EmployeeId"].ToInt32(),
                    ParkingStartDateTime = con.dr["ParkingStartDateTime"].ToDateTime(),
                    CreationDateTime = con.dr["CreationDateTime"].ToDateTime(),
                    IsDeleted = con.dr["IsDeleted"].ToBoolean()
                };
            }
            con.con.Close();
            return park;
        }

        public Park Control(int ParkPlaceId, int VehicleId, bool State)
        {
            throw new NotImplementedException();
        }

        public int Create(Park parameter)
        {
            int createdId = 0;
            con.con.Open();
            con.cmd.CommandText = "INSERT INTO Parks (ParkPlaceId, CampaignId, CustomerId, VehicleId, EmployeeId, ParkingStartDateTime, CreationDateTime, IsDeleted ) VALUES (@ParkPlaceId, @CampaignId, @CustomerId, @VehicleId, @EmployeeId, @ParkingStartDateTime,  @CreationDateTime, @isdeleted );";
            con.cmd.Parameters.AddWithValue("@ParkPlaceId", parameter.ParkPlaceId);
            con.cmd.Parameters.AddWithValue("@CampaignId", parameter.CampaignId);
            con.cmd.Parameters.AddWithValue("@CustomerId", parameter.CustomerId);
            con.cmd.Parameters.AddWithValue("@VehicleId", parameter.VehicleId);
            con.cmd.Parameters.AddWithValue("@EmployeeId", parameter.EmployeeId);
            con.cmd.Parameters.AddWithValue("@ParkingStartDateTime", parameter.ParkingStartDateTime);
            con.cmd.Parameters.AddWithValue("@CreationDateTime", parameter.CreationDateTime);
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
            con.cmd.CommandText = "Delete From Parks where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public Park Find(int Id)
        {
            Park park = null;
            con.con.Open();
            con.cmd.CommandText = "Select * From Parks where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                park = new Park()
                {
                    Id = con.dr["Id"].ToInt32(),
                    ParkPlaceId = con.dr["ParkPlaceId"].ToInt32(),
                    CampaignId = (con.dr["CampaignId"] != DBNull.Value) ? con.dr["CampaignId"].ToInt32() : (int?)null,
                    CustomerId = (con.dr["CustomerId"] != DBNull.Value) ? con.dr["CustomerId"].ToInt32() : (int?)null,
                    VehicleId = (con.dr["VehicleId"] != DBNull.Value) ? con.dr["VehicleId"].ToInt32() : (int?)null,
                    EmployeeId = con.dr["EmployeeId"].ToInt32(),
                    ParkingStartDateTime = con.dr["ParkingStartDateTime"].ToDateTime(),
                    CreationDateTime = con.dr["CreationDateTime"].ToDateTime(),
                    IsDeleted = con.dr["IsDeleted"].ToBoolean()
                };
            }
            con.con.Close();
            return park;
        }

        public List<Park> List()
        {
            List<Park> parks = new List<Park>();
            con.con.Open();
            con.cmd.CommandText = "Select * From Parks;";
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                parks.Add(new Park()
                {
                    Id = con.dr["Id"].ToInt32(),
                    ParkPlaceId = con.dr["ParkPlaceId"].ToInt32(),
                    CampaignId = (con.dr["CampaignId"] != DBNull.Value) ? con.dr["CampaignId"].ToInt32() : (int?)null,
                    CustomerId = (con.dr["CustomerId"] != DBNull.Value) ? con.dr["CustomerId"].ToInt32() : (int?)null,
                    VehicleId = (con.dr["VehicleId"] != DBNull.Value) ? con.dr["VehicleId"].ToInt32() : (int?)null,
                    EmployeeId = con.dr["EmployeeId"].ToInt32(),
                    ParkingStartDateTime = con.dr["ParkingStartDateTime"].ToDateTime(),
                    CreationDateTime = con.dr["CreationDateTime"].ToDateTime(),
                    IsDeleted = con.dr["IsDeleted"].ToBoolean()
                });
            }
            con.con.Close();
            return parks;
        }

        public List<Park> ListByNonDeleted()
        {
            List<Park> parks = new List<Park>();
            con.con.Open();
            con.cmd.CommandText = "Select * From Parks where IsDeleted=@isdeleted;";
            con.cmd.Parameters.AddWithValue("@isdeleted", 0);
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                parks.Add(new Park()
                {
                    Id = con.dr["Id"].ToInt32(),
                    ParkPlaceId = con.dr["ParkPlaceId"].ToInt32(),
                    CampaignId = (con.dr["CampaignId"] != DBNull.Value) ? con.dr["CampaignId"].ToInt32() : (int?)null,
                    CustomerId = (con.dr["CustomerId"] != DBNull.Value) ? con.dr["CustomerId"].ToInt32() : (int?)null,
                    VehicleId = (con.dr["VehicleId"] != DBNull.Value) ? con.dr["VehicleId"].ToInt32() : (int?)null,
                    EmployeeId = con.dr["EmployeeId"].ToInt32(),
                    ParkingStartDateTime = con.dr["ParkingStartDateTime"].ToDateTime(),
                    CreationDateTime = con.dr["CreationDateTime"].ToDateTime(),
                    IsDeleted = con.dr["IsDeleted"].ToBoolean()
                });
            }
            con.con.Close();
            return parks;
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            con.con.Open();
            con.cmd.CommandText = "UPDATE Parks SET IsDeleted=@isdeleted WHERE Id = @Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.Parameters.AddWithValue("@isdeleted", isDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public void Update(Park parameter)
        {
            con.con.Open();
            con.cmd.CommandText = "UPDATE Parks SET ParkPlaceId = @ParkPlaceId, CampaignId = @CampaignId, CustomerId=@CustomerId, VehicleId = @VehicleId, EmployeeId=@EmployeeId, ParkingStartDateTime = @ParkingStartDateTime, CreationDateTime=@CreationDateTime, IsDeleted=@isdeleted WHERE Id = @Id;";
            con.cmd.Parameters.AddWithValue("@Id", parameter.Id);
            con.cmd.Parameters.AddWithValue("@ParkPlaceId", parameter.ParkPlaceId);
            con.cmd.Parameters.AddWithValue("@CampaignId", parameter.CampaignId);
            con.cmd.Parameters.AddWithValue("@CustomerId", parameter.CustomerId);
            con.cmd.Parameters.AddWithValue("@VehicleId", parameter.VehicleId);
            con.cmd.Parameters.AddWithValue("@EmployeeId", parameter.EmployeeId);
            con.cmd.Parameters.AddWithValue("@ParkingStartDateTime", parameter.ParkingStartDateTime);
            con.cmd.Parameters.AddWithValue("@CreationDateTime", parameter.CreationDateTime);
            con.cmd.Parameters.AddWithValue("@isdeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }
    }
}