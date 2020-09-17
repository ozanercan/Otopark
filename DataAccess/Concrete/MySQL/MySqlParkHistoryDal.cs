using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using Core;
namespace DataAccess.Concrete.MySQL
{
    public class MySqlParkHistoryDal : IParkHistoryDal, IDalInstance
    {
        private Connection con = new Connection();

        public int Create(ParkHistory parameter)
        {
            int createdId = 0;
            con.con.Open();
            con.cmd.CommandText = "INSERT INTO parkhistories(ParkPlaceId, CampaignId, CustomerId, VehicleId, EmployeeId, Price, ParkingStartDateTime, ParkedLeaveDateTime, CreationDateTime, IsDeleted) VALUES (@ParkPlaceId, @CampaignId, @CustomerId,  @VehicleId, @EmployeeId, @Price, @ParkingStartDateTime, @ParkedLeaveDateTime, @CreationDateTime, @isdeleted);";
            con.cmd.Parameters.AddWithValue("@ParkPlaceId", parameter.ParkPlaceId);
            con.cmd.Parameters.AddWithValue("@CampaignId", parameter.CampaignId);
            con.cmd.Parameters.AddWithValue("@CustomerId", parameter.CustomerId);
            con.cmd.Parameters.AddWithValue("@VehicleId", parameter.VehicleId);
            con.cmd.Parameters.AddWithValue("@EmployeeId", parameter.EmployeeId);
            con.cmd.Parameters.AddWithValue("@Price", parameter.Price);
            con.cmd.Parameters.AddWithValue("@ParkingStartDateTime", parameter.ParkingStartDateTime);
            con.cmd.Parameters.AddWithValue("@ParkedLeaveDateTime", parameter.ParkedLeaveDateTime);
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
            con.cmd.CommandText = "Delete From ParkHistories where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public ParkHistory Find(int Id)
        {
            ParkHistory park = null;
            con.con.Open();
            con.cmd.CommandText = "Select * From ParkHistories where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                park = new ParkHistory()
                {
                    Id = con.dr["Id"].ToInt32(),
                    ParkPlaceId = con.dr["ParkPlaceId"].ToInt32(),
                    CampaignId = (con.dr["CampaignId"] != DBNull.Value) ? con.dr["CampaignId"].ToInt32() : (int?)null,
                    CustomerId = (con.dr["CustomerId"] != DBNull.Value) ? con.dr["CustomerId"].ToInt32() : (int?)null,
                    VehicleId = (con.dr["VehicleId"] != DBNull.Value) ? con.dr["VehicleId"].ToInt32() : (int?)null,
                    EmployeeId = con.dr["EmployeeId"].ToInt32(),
                    ParkingStartDateTime = con.dr["ParkingStartDateTime"].ToDateTime(),
                    ParkedLeaveDateTime = con.dr["ParkedLeaveDateTime"].ToDateTime(),
                    Price = con.dr["Price"].ToDouble(),
                    CreationDateTime = con.dr["CreationDateTime"].ToDateTime(),
                    IsDeleted = con.dr["IsDeleted"].ToBoolean()
                };
            }
            con.con.Close();
            return park;
        }

        public List<ParkHistory> List()
        {
            List<ParkHistory> parks = new List<ParkHistory>();
            con.con.Open();
            con.cmd.CommandText = "Select * From ParkHistories;";
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                parks.Add(new ParkHistory()
                {
                    Id = con.dr["Id"].ToInt32(),
                    ParkPlaceId = con.dr["ParkPlaceId"].ToInt32(),
                    CampaignId = (con.dr["CampaignId"] != DBNull.Value) ? con.dr["CampaignId"].ToInt32() : (int?)null,
                    CustomerId = (con.dr["CustomerId"] != DBNull.Value) ? con.dr["CustomerId"].ToInt32() : (int?)null,
                    VehicleId = (con.dr["VehicleId"] != DBNull.Value) ? con.dr["VehicleId"].ToInt32() : (int?)null,
                    EmployeeId = con.dr["EmployeeId"].ToInt32(),
                    ParkingStartDateTime = con.dr["ParkingStartDateTime"].ToDateTime(),
                    ParkedLeaveDateTime = con.dr["ParkedLeaveDateTime"].ToDateTime(),
                    Price = con.dr["Price"].ToDouble(),
                    CreationDateTime = con.dr["CreationDateTime"].ToDateTime(),
                    IsDeleted = con.dr["IsDeleted"].ToBoolean()
                });
            }
            con.con.Close();
            return parks;
        }

        public List<ParkHistory> ListByNonDeleted()
        {
            List<ParkHistory> parks = new List<ParkHistory>();
            con.con.Open();
            con.cmd.CommandText = "Select * From ParkHistories where IsDeleted=@isdeleted;";
            con.cmd.Parameters.AddWithValue("@isdeleted", 0);
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                parks.Add(new ParkHistory()
                {
                    Id = con.dr["Id"].ToInt32(),
                    ParkPlaceId = con.dr["ParkPlaceId"].ToInt32(),
                    CampaignId = (con.dr["CampaignId"] != DBNull.Value) ? con.dr["CampaignId"].ToInt32() : (int?)null,
                    CustomerId = (con.dr["CustomerId"] != DBNull.Value) ? con.dr["CustomerId"].ToInt32() : (int?)null,
                    VehicleId = (con.dr["VehicleId"] != DBNull.Value) ? con.dr["VehicleId"].ToInt32() : (int?)null,
                    EmployeeId = con.dr["EmployeeId"].ToInt32(),
                    ParkingStartDateTime = con.dr["ParkingStartDateTime"].ToDateTime(),
                    ParkedLeaveDateTime = con.dr["ParkedLeaveDateTime"].ToDateTime(),
                    Price = con.dr["Price"].ToDouble(),
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
            con.cmd.CommandText = "UPDATE ParkHistories SET IsDeleted=@isdeleted WHERE Id = @Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.Parameters.AddWithValue("@isdeleted", isDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public void Update(ParkHistory parameter)
        {
            con.con.Open();
            con.cmd.CommandText = "UPDATE ParkHistories SET ParkPlaceId = @ParkPlaceId, CampaignId = @CampaignId, CustomerId=@CustomerId, VehicleId = @VehicleId, EmployeeId=@EmployeeId, Price=@Price, ParkingStartDateTime = @ParkingStartDateTime, ParkedLeaveDateTime=@ParkedLeaveDateTime, CreationDateTime=@CreationDateTime, IsDeleted=@isdeleted WHERE Id = @Id;";
            con.cmd.Parameters.AddWithValue("@Id", parameter.Id);
            con.cmd.Parameters.AddWithValue("@ParkPlaceId", parameter.ParkPlaceId);
            con.cmd.Parameters.AddWithValue("@CampaignId", parameter.CampaignId);
            con.cmd.Parameters.AddWithValue("@CustomerId", parameter.CustomerId);
            con.cmd.Parameters.AddWithValue("@VehicleId", parameter.VehicleId);
            con.cmd.Parameters.AddWithValue("@EmployeeId", parameter.EmployeeId);
            con.cmd.Parameters.AddWithValue("@Price", parameter.Price);
            con.cmd.Parameters.AddWithValue("@ParkingStartDateTime", parameter.ParkingStartDateTime);
            con.cmd.Parameters.AddWithValue("@ParkedLeaveDateTime", parameter.ParkedLeaveDateTime);
            con.cmd.Parameters.AddWithValue("@CreationDateTime", parameter.CreationDateTime);
            con.cmd.Parameters.AddWithValue("@isdeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }
    }
}