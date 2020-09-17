using Core;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.MySQL
{
    public class MySqlBrandDal : IBrandDal
    {
        Connection con = new Connection();
        public int Create(Brand parameter)
        {
            int createdId = 0;
            con.con.Open();
            con.cmd.CommandText = "Insert into brands(Name, IsDeleted) Values (@name, @isdeleted);";
            con.cmd.Parameters.AddWithValue("@name", parameter.Name);
            con.cmd.Parameters.AddWithValue("@isdeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.cmd.CommandText = "SELECT LAST_INSERT_ID();";
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
                createdId = Convert.ToInt32(con.dr[0]);
            con.con.Close();
            return createdId;
        }

        public void Delete(int Id)
        {
            con.con.Open();
            con.cmd.CommandText = "Delete From brands where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public Brand Find(int Id)
        {
            Brand brand = null;
            con.con.Open();
            con.cmd.CommandText = "Select * From brands where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                brand = new Brand()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    Name = con.dr["Name"].ToString(),
                    IsDeleted = con.dr["IsDeleted"].ToBoolean()
                };
            }
            con.con.Close();
            return brand;
        }

        public List<Brand> List()
        {
            List<Brand> makes = new List<Brand>();
            con.con.Open();
            con.cmd.CommandText = "Select * From brands;";
            con.cmd.Connection = con.con;
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                makes.Add(new Brand()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    Name = con.dr["Name"].ToString(),
                    IsDeleted = con.dr["IsDeleted"].ToBoolean()
                });
            }
            con.con.Close();
            return makes;
        }

        public List<Brand> ListByNonDeleted()
        {
            List<Brand> makes = new List<Brand>();
            con.con.Open();
            con.cmd.CommandText = "Select * From brands where IsDeleted=@isdeleted;";
            con.cmd.Parameters.AddWithValue("@isdeleted", 0);
            con.cmd.Connection = con.con;
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                makes.Add(new Brand()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    Name = con.dr["Name"].ToString(),
                    IsDeleted = con.dr["IsDeleted"].ToBoolean()
                });
            }
            con.con.Close();
            return makes;
        }

        public void ChangeIsDeleteField(int id, bool isDeleted)
        {
            con.con.Open();
            con.cmd.CommandText = "Update brands set IsDeleted=@isdeleted where Id=@id;";
            con.cmd.Parameters.AddWithValue("@id", id);
            con.cmd.Parameters.AddWithValue("@isdeleted", isDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public void Update(Brand parameter)
        {
            con.con.Open();
            con.cmd.CommandText = "Update brands set Name=@name, IsDeleted=@isdeleted where Id=@id;";
            con.cmd.Parameters.AddWithValue("@id", parameter.Id);
            con.cmd.Parameters.AddWithValue("@name", parameter.Name);
            con.cmd.Parameters.AddWithValue("@isdeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }
    }
}
