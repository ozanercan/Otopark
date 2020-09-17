using Core;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.MySQL
{
    public class MySqlModelDal : IModelDal
    {
        Connection con = new Connection();
        public int Create(Model parameter)
        {
            int createdId = 0;
            con.con.Open();
            con.cmd.CommandText = "Insert into models(BrandId, Name, IsDeleted) Values (@brandid,@name,@isdeleted);";
            con.cmd.Parameters.AddWithValue("@brandid", parameter.BrandId);
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
            con.cmd.CommandText = "Delete From models where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public Model Find(int Id)
        {
            Model model = null;
            con.con.Open();
            con.cmd.CommandText = "Select * From models where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                model = new Model()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    BrandId = Convert.ToInt32(con.dr["BrandId"]),
                    Name = con.dr["Name"].ToString(),
                    IsDeleted = con.dr["IsDeleted"].ToBoolean()
                };
            }
            con.con.Close();
            return model;
        }

        public List<Model> List()
        {
            List<Model> models = new List<Model>();
            con.con.Open();
            con.cmd.CommandText = "Select * From models;";
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                models.Add(new Model()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    BrandId = Convert.ToInt32(con.dr["BrandId"]),
                    Name = con.dr["Name"].ToString(),
                    IsDeleted = con.dr["IsDeleted"].ToBoolean()
                });
            }
            con.con.Close();
            return models;
        }

        public List<Model> ListByBrandId(int brandId)
        {
            List<Model> models = new List<Model>();
            con.con.Open();
            con.cmd.CommandText = "SELECT * FROM models where BrandId=@brandid;";
            con.cmd.Parameters.AddWithValue("@brandid", brandId);
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                models.Add(new Model()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    BrandId = Convert.ToInt32(con.dr["BrandId"]),
                    Name = con.dr["Name"].ToString(),
                    IsDeleted = con.dr["IsDeleted"].ToBoolean()
                });
            }
            con.con.Close();
            return models;
        }

        public List<Model> ListByBrandIdAndNonDeleted(int brandId)
        {
            List<Model> models = new List<Model>();
            con.con.Open();
            con.cmd.CommandText = "SELECT * FROM models where BrandId=@brandid and IsDeleted=@isdeleted;";
            con.cmd.Parameters.AddWithValue("@brandid", brandId);
            con.cmd.Parameters.AddWithValue("@isdeleted", 0);
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                models.Add(new Model()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    BrandId = Convert.ToInt32(con.dr["BrandId"]),
                    Name = con.dr["Name"].ToString(),
                    IsDeleted = con.dr["IsDeleted"].ToBoolean()
                });
            }
            con.con.Close();
            return models;
        }

        public List<Model> ListByNonDeleted()
        {
            List<Model> models = new List<Model>();
            con.con.Open();
            con.cmd.CommandText = "Select * From models where IsDeleted=@isdeleted;";
            con.cmd.Parameters.AddWithValue("@isdeleted", 0);
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                models.Add(new Model()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    BrandId = Convert.ToInt32(con.dr["BrandId"]),
                    Name = con.dr["Name"].ToString(),
                    IsDeleted = con.dr["IsDeleted"].ToBoolean()
                });
            }
            con.con.Close();
            return models;
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            con.con.Open();
            con.cmd.CommandText = "Update models set IsDeleted=@isdeleted where Id=@id;";
            con.cmd.Parameters.AddWithValue("@id", Id);
            con.cmd.Parameters.AddWithValue("@isdeleted", isDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public void Update(Model parameter)
        {
            con.con.Open();
            con.cmd.CommandText = "Update models set BrandId=@brandid and Name=@name and IsDeleted=@isdeleted where Id=@id;";
            con.cmd.Parameters.AddWithValue("@id", parameter.Id);
            con.cmd.Parameters.AddWithValue("@brandid", parameter.BrandId);
            con.cmd.Parameters.AddWithValue("@name", parameter.Name);
            con.cmd.Parameters.AddWithValue("@isdeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }
    }
}
