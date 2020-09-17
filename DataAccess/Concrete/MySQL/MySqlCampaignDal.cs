using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.MySQL
{
    public class MySqlCampaignDal : ICampaignDal, IDalInstance
    {
        private readonly Connection con = new Connection();

        public int Create(Campaign parameter)
        {
            int createdCampaignId = 0;
            con.con.Open();
            con.cmd.CommandText = "INSERT INTO campaigns(ParkPlaceId, Name, Description, PricePerMin, StartingDate, FinishingDate, IsDeleted) VALUES (ParkPlaceId,@Name,@Description,@PricePerMin,@StartingDate,@FinishingDate,@IsDeleted);";
            con.cmd.Parameters.AddWithValue("@ParkPlaceId", parameter.ParkPlaceId);
            con.cmd.Parameters.AddWithValue("@Name", parameter.Name);
            con.cmd.Parameters.AddWithValue("@Description", parameter.Description);
            con.cmd.Parameters.AddWithValue("@PricePerMin", parameter.PricePerMin);
            con.cmd.Parameters.AddWithValue("@StartingDate", parameter.StartingDate);
            con.cmd.Parameters.AddWithValue("@FinishingDate", parameter.FinishingDate);
            con.cmd.Parameters.AddWithValue("@IsDeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.cmd.CommandText = "Select last_insert_rowid();";
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                createdCampaignId = Convert.ToInt32(con.dr[0]);
            }
            con.con.Close();
            return createdCampaignId;
        }

        public void Delete(int Id)
        {
            con.con.Open();
            con.cmd.CommandText = "Delete From Campaign where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public Campaign Find(int Id)
        {
            Campaign campaign = null;
            con.con.Open();
            con.cmd.CommandText = "Select * From Campaigns where Id=@id;";
            con.cmd.Parameters.AddWithValue("@id", Id);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                campaign = new Campaign()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    ParkPlaceId = Convert.ToInt32(con.dr["ParkPlaceId"]),
                    Name = con.dr["Name"].ToString(),
                    Description = con.dr["Description"].ToString(),
                    PricePerMin = Convert.ToDouble(con.dr["PricePerMin"]),
                    StartingDate = Convert.ToDateTime(con.dr["StartingDate"]),
                    FinishingDate = Convert.ToDateTime(con.dr["FinishingDate"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                };
            }
            con.con.Close();
            return campaign;
        }

        public List<Campaign> List()
        {
            List<Campaign> campaigns = new List<Campaign>();
            con.con.Open();
            con.cmd.CommandText = "Select * From Campaigns;";
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                campaigns.Add(new Campaign()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    ParkPlaceId = Convert.ToInt32(con.dr["ParkPlaceId"]),
                    Name = con.dr["Name"].ToString(),
                    Description = con.dr["Description"].ToString(),
                    PricePerMin = Convert.ToDouble(con.dr["PricePerMin"]),
                    StartingDate = Convert.ToDateTime(con.dr["StartingDate"]),
                    FinishingDate = Convert.ToDateTime(con.dr["FinishingDate"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                });
            }
            con.con.Close();
            return campaigns;
        }

        public List<Campaign> ListByNonDeleted()
        {
            List<Campaign> campaigns = new List<Campaign>();
            con.con.Open();
            con.cmd.CommandText = "Select * From Campaigns";
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                campaigns.Add(new Campaign()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    ParkPlaceId = Convert.ToInt32(con.dr["ParkPlaceId"]),
                    Name = con.dr["Name"].ToString(),
                    Description = con.dr["Description"].ToString(),
                    PricePerMin = Convert.ToDouble(con.dr["PricePerMin"]),
                    StartingDate = Convert.ToDateTime(con.dr["StartingDate"]),
                    FinishingDate = Convert.ToDateTime(con.dr["FinishingDate"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                });
            }
            con.con.Close();
            return campaigns;
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            con.con.Open();
            con.cmd.CommandText = "UPDATE IsDeleted=@isdeleted WHERE Id = @id;";
            con.cmd.Parameters.AddWithValue("@id", Id);
            con.cmd.Parameters.AddWithValue("@isdeleted", isDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public void Update(Campaign parameter)
        {
            con.con.Open();
            con.cmd.CommandText = "UPDATE Campaigns SET ParkPlaceId = @ParkPlaceId, Name = @Name, Description = @Description, PricePerMin = @PricePerMin, StartingDate = @StartingDate, FinishingDate = @FinishingDate, IsDeleted=@isdeleted WHERE Id = @Id;";
            con.cmd.Parameters.AddWithValue("@Id", parameter.Id);
            con.cmd.Parameters.AddWithValue("@ParkPlaceId", parameter.ParkPlaceId);
            con.cmd.Parameters.AddWithValue("@Name", parameter.Name);
            con.cmd.Parameters.AddWithValue("@Description", parameter.Description);
            con.cmd.Parameters.AddWithValue("@PricePerMin", parameter.PricePerMin);
            con.cmd.Parameters.AddWithValue("@StartingDate", parameter.StartingDate);
            con.cmd.Parameters.AddWithValue("@FinishingDate", parameter.FinishingDate);
            con.cmd.Parameters.AddWithValue("@isdeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }
    }
}