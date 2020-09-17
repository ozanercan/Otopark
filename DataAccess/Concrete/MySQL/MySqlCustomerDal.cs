using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.MySQL
{
    public class MySqlCustomerDal : ICustomerDal, IDalInstance
    {
        private Connection con = new Connection();

        public int Create(Customer parameter)
        {
            int createdId = 0;
            con.con.Open();
            con.cmd.CommandText = "INSERT INTO Customers ( PersonId, IsDeleted ) VALUES ( @PersonId, @IsDeleted);";
            con.cmd.Parameters.AddWithValue("@PersonId", parameter.PersonId);
            con.cmd.Parameters.AddWithValue("@IsDeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.cmd.CommandText = "Select LAST_INSERT_ID();";
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                createdId = Convert.ToInt32(con.dr[0]);
            }
            con.con.Close();
            return createdId;
        }

        public void Delete(int Id)
        {
            con.con.Open();
            con.cmd.CommandText = "Delete From Customers where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public Customer Find(int Id)
        {
            Customer customer = null;
            con.con.Open();
            con.cmd.CommandText = "Select * From Customers where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                customer = new Customer()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    PersonId = Convert.ToInt32(con.dr["PersonId"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                };
            }
            con.con.Close();
            return customer;
        }

        public Customer GetCustomerByPersonId(int PersonId)
        {
            Customer customer = null;
            con.con.Open();
            con.cmd.CommandText = "Select * From Customers where PersonId=@PersonId;";
            con.cmd.Parameters.AddWithValue("@PersonId", PersonId);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                customer = new Customer()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    PersonId = Convert.ToInt32(con.dr["PersonId"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                };
            }
            con.con.Close();
            return customer;
        }

        public List<Customer> ListByNonDeleted()
        {
            List<Customer> customers = new List<Customer>();
            con.con.Open();
            con.cmd.CommandText = "Select * From Customers where IsDeleted=@IsDeleted;";
            con.cmd.Parameters.AddWithValue("@IsDeleted", 0);
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                customers.Add(new Customer()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    PersonId = Convert.ToInt32(con.dr["PersonId"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                });
            }
            con.con.Close();
            return customers;
        }

        public List<Customer> List()
        {
            List<Customer> customers = new List<Customer>();
            con.con.Open();
            con.cmd.CommandText = "Select * From Customers;";
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                customers.Add(new Customer()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    PersonId = Convert.ToInt32(con.dr["PersonId"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                });
            }
            con.con.Close();
            return customers;
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            con.con.Open();
            con.cmd.CommandText = "UPDATE Customers SET IsDeleted=@isdeleted WHERE Id = @Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.Parameters.AddWithValue("@isdeleted", isDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public void Update(Customer parameter)
        {
            con.con.Open();
            con.cmd.CommandText = "UPDATE Customers SET PersonId = @PersonId, IsDeleted=@isdeleted WHERE Id = @Id;";
            con.cmd.Parameters.AddWithValue("@Id", parameter.Id);
            con.cmd.Parameters.AddWithValue("@PersonId", parameter.PersonId);
            con.cmd.Parameters.AddWithValue("@isdeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }
    }
}