using DataAccess.Abstract;
using Entities.Concrete;
using Entities.Concrete.Models;
using System;
using System.Collections.Generic;

namespace DataAccess.Concrete.MySQL
{
    public class MySqlEmployeeDal : IEmployeeDal, IDalInstance
    {
        private Connection con = new Connection();

        public int Create(Employee parameter)
        {
            int createdEmployeeId = 0;

            con.con.Open();
            con.cmd.CommandText = "INSERT INTO Employees (PersonId, UserName, Password, IsDeleted) VALUES (@PersonId, @UserName, @Password, @isdeleted);";
            con.cmd.Parameters.AddWithValue("@PersonId", parameter.PersonId);
            con.cmd.Parameters.AddWithValue("@UserName", parameter.UserName);
            con.cmd.Parameters.AddWithValue("@Password", parameter.Password);
            con.cmd.Parameters.AddWithValue("@isdeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.cmd.CommandText = "Select LAST_INSERT_ID();";
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
                createdEmployeeId = Convert.ToInt32(con.dr[0]);

            con.con.Close();
            return createdEmployeeId;
        }

        public void Delete(int Id)
        {
            con.con.Open();
            con.cmd.CommandText = "Delete From Employees where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public Employee Find(int Id)
        {
            Employee employee = null;
            con.con.Open();
            con.cmd.CommandText = "Select * From Employees where Id=@Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                employee = new Employee()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    PersonId = Convert.ToInt32(con.dr["PersonId"]),
                    UserName = con.dr["UserName"].ToString(),
                    Password = con.dr["Password"].ToString(),
                    LastLoginDateTime = (con.dr["LastLoginDateTime"] != DBNull.Value) ? Convert.ToDateTime(con.dr["LastLoginDateTime"]) : null as DateTime?,
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                };
            }
            con.con.Close();
            return employee;
        }

        public Employee FindEmployeeByUsernamendPassword(Login parameter)
        {
            Employee employee = null;
            con.con.Open();
            con.cmd.CommandText = "Select * From Employees where UserName=@UserName and Password=@Password;";
            con.cmd.Parameters.AddWithValue("@UserName", parameter.UserName);
            con.cmd.Parameters.AddWithValue("@Password", parameter.Password);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
            {
                employee = new Employee()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    PersonId = Convert.ToInt32(con.dr["PersonId"]),
                    UserName = con.dr["UserName"].ToString(),
                    Password = con.dr["Password"].ToString(),
                    LastLoginDateTime = (con.dr["LastLoginDateTime"] != DBNull.Value) ? Convert.ToDateTime(con.dr["LastLoginDateTime"]) : null as DateTime?,
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                };
            }
            con.con.Close();
            return employee;
        }

        public List<Employee> ListByNonDeleted()
        {
            List<Employee> employees = new List<Employee>();
            con.con.Open();
            con.cmd.CommandText = "Select * From Employees where IsDeleted=@isdeleted;";
            con.cmd.Parameters.AddWithValue("@isdeleted", 0);
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                employees.Add(new Employee()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    PersonId = Convert.ToInt32(con.dr["PersonId"]),
                    UserName = con.dr["UserName"].ToString(),
                    Password = con.dr["Password"].ToString(),
                    LastLoginDateTime = (con.dr["LastLoginDateTime"] == DBNull.Value) ? null as DateTime? : Convert.ToDateTime(con.dr["LastLoginDateTime"]),
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                });
            }
            con.con.Close();
            return employees;
        }

        public List<Employee> List()
        {
            List<Employee> employees = new List<Employee>();
            con.con.Open();
            con.cmd.CommandText = "Select * From Employees;";
            con.dr = con.cmd.ExecuteReader();
            while (con.dr.Read())
            {
                employees.Add(new Employee()
                {
                    Id = Convert.ToInt32(con.dr["Id"]),
                    PersonId = Convert.ToInt32(con.dr["PersonId"]),
                    UserName = con.dr["UserName"].ToString(),
                    Password = con.dr["Password"].ToString(),
                    LastLoginDateTime = (con.dr["LastLoginDateTime"] != DBNull.Value) ? Convert.ToDateTime(con.dr["LastLoginDateTime"]) : null as DateTime?,
                    IsDeleted = Convert.ToBoolean(con.dr["IsDeleted"])
                });
            }
            con.con.Close();
            return employees;
        }
        public bool LoginControl(Login parameter)
        {
            bool result = false;
            con.con.Open();
            con.cmd.CommandText = "Select * From Employees where UserName=@UserName and Password=@Password;";
            con.cmd.Parameters.AddWithValue("@UserName", parameter.UserName);
            con.cmd.Parameters.AddWithValue("@Password", parameter.Password);
            con.dr = con.cmd.ExecuteReader();
            if (con.dr.Read())
                result = (con.dr[0] != DBNull.Value) ? true : false;

            con.con.Close();
            return result;
        }

        public void ChangeIsDeleteField(int Id, bool isDeleted)
        {
            con.con.Open();
            con.cmd.CommandText = "UPDATE Employees SET IsDeleted=@isdeleted WHERE Id = @Id;";
            con.cmd.Parameters.AddWithValue("@Id", Id);
            con.cmd.Parameters.AddWithValue("@isdeleted", isDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }

        public void Update(Employee parameter)
        {
            con.con.Open();
            con.cmd.CommandText = "UPDATE Employees SET PersonId = @PersonId, UserName = @UserName, Password = @Password, LastLoginDateTime = @LastLoginDateTime, IsDeleted=@isdeleted WHERE Id = @Id;";
            con.cmd.Parameters.AddWithValue("@Id", parameter.Id);
            con.cmd.Parameters.AddWithValue("@PersonId", parameter.PersonId);
            con.cmd.Parameters.AddWithValue("@UserName", parameter.UserName);
            con.cmd.Parameters.AddWithValue("@Password", parameter.Password);
            con.cmd.Parameters.AddWithValue("@LastLoginDateTime", parameter.LastLoginDateTime);
            con.cmd.Parameters.AddWithValue("@isdeleted", parameter.IsDeleted);
            con.cmd.ExecuteNonQuery();
            con.con.Close();
        }
    }
}