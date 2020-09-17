using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.MySQL
{
    public class MySqlPersonDal : IPersonDal, IDalInstance
    {
        private Connection con = new Connection();

        public int Create(Person parameter)
        {
            int createdPersonId = 0;
            con.con.Open();
            con.cmd.CommandText = "INSERT INTO persons(NationalIdentityNumber, FirstName, LastName, TelephoneNumber, CreationDate) VALUES (@NationalIdentityNumber, @FirstName, @LastName, @TelephoneNumber, @CreationDate);";
            con.cmd.Parameters.AddWithValue("@NationalIdentityNumber", parameter.NationalIdentityNumber);
            con.cmd.Parameters.AddWithValue("@FirstName", parameter.FirstName);
            con.cmd.Parameters.AddWithValue("@LastName", parameter.LastName);
            con.cmd.Parameters.AddWithValue("@TelephoneNumber", parameter.TelephoneNumber);
            con.cmd.Parameters.AddWithValue("@CreationDate", parameter.CreationDate);
            con.cmd.ExecuteNonQuery();
            con.cmd.CommandText = "Select LAST_INSERT_ID();";
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                createdPersonId = Convert.ToInt32(con.dr[0]);
            }
            con.con.Close();
            return createdPersonId;
        }

        public void Delete(int Id)
        {
            con.con.Open();
            con.cmd.CommandText = "Delete From Persons where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public Person Find(int Id)
        {
            Person person = null;
            con.con.Open();
            con.cmd.CommandText = "Select * From Persons where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                person = new Person()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    NationalIdentityNumber = con.dr["NationalIdentityNumber"].ToString(),
                    FirstName = con.dr["FirstName"].ToString(),
                    LastName = con.dr["LastName"].ToString(),
                    TelephoneNumber = con.dr["TelephoneNumber"].ToString(),
                    CreationDate = Convert.ToDateTime(con.dr["CreationDate"])
                };
            }
            con.con.Close();
            return person;
        }

        public List<Person> List()
        {
            List<Person> persons = new List<Person>();
            con.con.Open();
            con.cmd.CommandText = "Select * From Persons;";
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                persons.Add(new Person()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    NationalIdentityNumber = con.dr["NationalIdentityNumber"].ToString(),
                    FirstName = con.dr["FirstName"].ToString(),
                    LastName = con.dr["LastName"].ToString(),
                    TelephoneNumber = con.dr["TelephoneNumber"].ToString(),
                    CreationDate = Convert.ToDateTime(con.dr["CreationDate"])
                });
            }
            con.con.Close();
            return persons;
        }

        public List<Person> ListByParams(params int[] ids)
        {
            string query = "";
            for (int i = 0; i < ids.Length; i++)
            {
                query = $"Select * From Persons where Id=@id{i}; ";
                con.cmd.Parameters.AddWithValue($"@id{i}", ids[i]);
            }
            List<Person> persons = new List<Person>();
            con.con.Open();
            con.cmd.CommandText = query;
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                persons.Add(new Person()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    NationalIdentityNumber = con.dr["NationalIdentityNumber"].ToString(),
                    FirstName = con.dr["FirstName"].ToString(),
                    LastName = con.dr["LastName"].ToString(),
                    TelephoneNumber = con.dr["TelephoneNumber"].ToString(),
                    CreationDate = Convert.ToDateTime(con.dr["CreationDate"])
                });
            }
            con.con.Close();
            return persons;
        }

        public void Update(Person parameter)
        {
            con.con.Open();
            con.cmd.CommandText = "UPDATE Persons SET NationalIdentityNumber = @NationalIdentityNumber, FirstName = @FirstName, LastName = @LastName, TelephoneNumber = @TelephoneNumber, CreationDate = @CreationDate WHERE Id = @Id;";
            con.cmd.Parameters.AddWithValue("@Id", parameter.Id);
            con.cmd.Parameters.AddWithValue("@NationalIdentityNumber", parameter.NationalIdentityNumber);
            con.cmd.Parameters.AddWithValue("@FirstName", parameter.FirstName);
            con.cmd.Parameters.AddWithValue("@LastName", parameter.LastName);
            con.cmd.Parameters.AddWithValue("@TelephoneNumber", parameter.TelephoneNumber);
            con.cmd.Parameters.AddWithValue("@CreationDate", parameter.CreationDate);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }
    }
}