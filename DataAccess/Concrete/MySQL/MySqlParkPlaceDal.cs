using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.MySQL
{
    public class MySqlParkPlaceDal : IParkPlaceDal, IDalInstance
    {
        private Connection con = new Connection();

        public int Create(ParkPlace parameter)
        {
            int createdId = 0;
            con.con.Open();
            con.cmd.CommandText = "INSERT INTO ParkPlaces ( X, Y, Width, Height, Name, CreationDate, IsDeleted ) VALUES ( @X, @Y, @Width, @Height, @Name, @CreationDate, @isdeleted );";
            con.cmd.Parameters.AddWithValue("@X", parameter.X);
            con.cmd.Parameters.AddWithValue("@Y", parameter.Y);
            con.cmd.Parameters.AddWithValue("@Width", parameter.Width);
            con.cmd.Parameters.AddWithValue("@Height", parameter.Height);
            con.cmd.Parameters.AddWithValue("@Name", parameter.Name);
            con.cmd.Parameters.AddWithValue("@CreationDate", parameter.CreationDate);
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
            con.cmd.CommandText = "Delete From ParkPlaces where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public ParkPlace Find(int Id)
        {
            ParkPlace parkplace = null;
            con.con.Open();
            con.cmd.CommandText = "Select * From ParkPlaces where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                parkplace = new ParkPlace()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    X = Convert.ToInt32(con.dr["X"]),
                    Y = Convert.ToInt32(con.dr["Y"]),
                    Width = Convert.ToInt32(con.dr["Width"]),
                    Height = Convert.ToInt32(con.dr["Height"]),
                    Name = con.dr["Name"].ToString(),
                    CreationDate = Convert.ToDateTime(con.dr["CreationDate"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                };
            }
            con.con.Close();
            return parkplace;
        }

        public List<ParkPlace> ListByNonDeleted()
        {
            List<ParkPlace> parkplaces = new List<ParkPlace>();
            con.con.Open();
            con.cmd.CommandText = "Select * From ParkPlaces where IsDeleted=@isdeleted;";
            con.cmd.Parameters.AddWithValue("@isdeleted", 0);
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                parkplaces.Add(new ParkPlace()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    X = Convert.ToInt32(con.dr["X"]),
                    Y = Convert.ToInt32(con.dr["Y"]),
                    Width = Convert.ToInt32(con.dr["Width"]),
                    Height = Convert.ToInt32(con.dr["Height"]),
                    Name = con.dr["Name"].ToString(),
                    CreationDate = Convert.ToDateTime(con.dr["CreationDate"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"]),
                });
            }
            con.con.Close();
            return parkplaces;
        }

        public List<ParkPlace> List()
        {
            List<ParkPlace> parkplaces = new List<ParkPlace>();
            con.con.Open();
            con.cmd.CommandText = "Select * From ParkPlaces;";
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                parkplaces.Add(new ParkPlace()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    X = Convert.ToInt32(con.dr["X"]),
                    Y = Convert.ToInt32(con.dr["Y"]),
                    Width = Convert.ToInt32(con.dr["Width"]),
                    Height = Convert.ToInt32(con.dr["Height"]),
                    Name = con.dr["Name"].ToString(),
                    CreationDate = Convert.ToDateTime(con.dr["CreationDate"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"]),
                });
            }
            con.con.Close();
            return parkplaces;
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            con.con.Open();
            con.cmd.CommandText = "UPDATE ParkPlaces SET IsDeleted = @isdeleted WHERE Id = @Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.Parameters.AddWithValue("@isdeleted", isDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public void MultipleCreate(List<ParkPlace> parameter)
        {
            string query = "";

            for (int i = 0; i < parameter.Count; i++)
            {
                query += $"INSERT INTO ParkPlaces ( X, Y, Width, Height, Name, CreationDate, IsDeleted ) VALUES ( @X{i}, @Y{i}, @Width{i}, @Height{i}, @Name{i}, @CreationDate{i}, @isdeleted{i} ); ";
                con.cmd.Parameters.AddWithValue($"@X{i}", parameter[i].X);
                con.cmd.Parameters.AddWithValue($"@Y{i}", parameter[i].Y);
                con.cmd.Parameters.AddWithValue($"@Width{i}", parameter[i].Width);
                con.cmd.Parameters.AddWithValue($"@Height{i}", parameter[i].Height);
                con.cmd.Parameters.AddWithValue($"@Name{i}", parameter[i].Name);
                con.cmd.Parameters.AddWithValue($"@CreationDate{i}", parameter[i].CreationDate);
                con.cmd.Parameters.AddWithValue($"@isdeleted{i}", parameter[i].IsDeleted);
            }
            con.con.Open();
            con.cmd.CommandText = query;
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public void MultipleDelete(List<int> parameters)
        {
            string query = "";

            for (int i = 0; i < parameters.Count; i++)
            {
                query += $"Delete From ParkPlaces where Id=@Id{i}; ";
                con.cmd.Parameters.AddWithValue($"@Id{i}", parameters[i]);
            }
            con.con.Open();
            con.cmd.CommandText = query;
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public void MultipleUpdate(List<ParkPlace> parameter)
        {
            string query = "";
            for (int i = 0; i < parameter.Count; i++)
            {
                query += $"UPDATE ParkPlaces SET X=@X{i}, Y=@Y{i}, Width=@Width{i}, Height=@Height{i}, Name=@Name{i}, IsDeleted = @isdeleted{i} WHERE Id = @Id{i}; ";
                con.cmd.Parameters.AddWithValue($"@Id{i}", parameter[i].Id);
                con.cmd.Parameters.AddWithValue($"@X{i}", parameter[i].X);
                con.cmd.Parameters.AddWithValue($"@Y{i}", parameter[i].Y);
                con.cmd.Parameters.AddWithValue($"@Width{i}", parameter[i].Width);
                con.cmd.Parameters.AddWithValue($"@Height{i}", parameter[i].Height);
                con.cmd.Parameters.AddWithValue($"@Name{i}", parameter[i].Name);
                con.cmd.Parameters.AddWithValue($"@isdeleted{i}", parameter[i].IsDeleted);
            }
            con.con.Open();
            con.cmd.CommandText = query;
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public void Update(ParkPlace parameter)
        {
            con.con.Open();
            con.cmd.CommandText = "UPDATE ParkPlaces SET X=@X, Y=@Y, Width=@Width, Height=@Height, Name=@Name, CreationDate = @CreationDate, IsDeleted = @isdeleted WHERE Id = @Id;";
            con.cmd.Parameters.AddWithValue("@Id", parameter.Id);
            con.cmd.Parameters.AddWithValue("@X", parameter.X);
            con.cmd.Parameters.AddWithValue("@Y", parameter.Y);
            con.cmd.Parameters.AddWithValue("@Width", parameter.Width);
            con.cmd.Parameters.AddWithValue("@Height", parameter.Height);
            con.cmd.Parameters.AddWithValue("@Name", parameter.Name);
            con.cmd.Parameters.AddWithValue("@CreationDate", parameter.CreationDate);
            con.cmd.Parameters.AddWithValue("@isdeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }
    }
}