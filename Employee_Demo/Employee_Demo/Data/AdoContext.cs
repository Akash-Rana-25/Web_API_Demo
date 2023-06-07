using Employee_Demo.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Employee_Demo.Data
{
    public class AdoContext
    {
        private string connectionString;
        public AdoContext() {
            connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;
        }

        public IEnumerable<Employee> GetAllEmployee()
        {
            List<Employee> Employees = new List<Employee>();

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT * FROM Employee";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    connection.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Employee Employee = new Employee
                            {
                                Employee_PK = (int)reader["Employee_PK"],
                                FirstName = (string)reader["FirstName"],
                                MiddleName = (string)reader["MiddleName"],
                                LastName = (string)reader["LastName"],
                                EmpCode = (int)reader["EmpCode"],
                                Gender = (Gender)reader["Gender"],
                                DOB = (DateTime)reader["DOB"],
                                salary = (Decimal)reader["salary"],
                                JoiningDate = (DateTime)reader["JoiningDate"],
                                ResignDate = (DateTime)reader["ResignDate"]
                            };
                            Employees.Add(Employee);
                        }
                    }
                }
            }

            return Employees;
        }


        public Employee GetByID(int Id) 
        {
            Employee Employee = null;

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cm = new SqlCommand("select * from Employee where Employee_PK= @Id", connection);
                cm.Parameters.AddWithValue("@Id", Id);
                connection.Open();
                using (SqlDataReader reader = cm.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        Employee = new Employee
                        {
                            Employee_PK = (int)reader["Id"],
                            FirstName = (string)reader["firstName"],
                            MiddleName = (string)reader["middleName"],
                            LastName = (string)reader["lastName"],
                            EmpCode = (int)reader["EmpCode"],
                            Gender = (Gender)reader["Gender"],
                            DOB = (DateTime)reader["DOB"],
                            salary = (Decimal)reader["salry"],
                            JoiningDate = (DateTime)reader["joinDate"],
                            ResignDate = (DateTime)reader["resignDate"]
                        };
                    }
                }

            }

            return Employee;
        }


        
        
        public void Post(Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string sqlText = @"INSERT INTO Employee VALUES (@Employee_PK,@FirstName,@MiddleName,@LastName,@EmpCode,@gender,@DOB,@salary,@JoiningDate,@ResignDate);";
                SqlCommand cmd = new SqlCommand(sqlText, connection);

                cmd.Parameters.AddWithValue("@Employee_PK", employee.Employee_PK);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@EmpCode", employee.EmpCode);
                cmd.Parameters.AddWithValue("@gender", employee.Gender);
                cmd.Parameters.AddWithValue("@DOB", employee.DOB);
                cmd.Parameters.AddWithValue("@salary", employee.salary);
                cmd.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                cmd.Parameters.AddWithValue("@ResignDate", employee.ResignDate);
            }
        }


        public void Put(int Id, Employee employee)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand cmd = new SqlCommand("Update Employee set FirstName=@FirstName,MiddleName =@MiddleName,LastName =@LastName,EmpCode=@EmpCode,Gender=@gender,salary=@salary,JoiningDate=@JoiningDate,ResignDate=@ResignDate Where Employee_PK=@Id;", connection);
                cmd.Parameters.AddWithValue("@Id", Id);
                cmd.Parameters.AddWithValue("@FirstName", employee.FirstName);
                cmd.Parameters.AddWithValue("@MiddleName", employee.MiddleName);
                cmd.Parameters.AddWithValue("@LastName", employee.LastName);
                cmd.Parameters.AddWithValue("@EmpCode", employee.EmpCode);
                cmd.Parameters.AddWithValue("@gender", employee.Gender);
                cmd.Parameters.AddWithValue("@DOB", employee.DOB);
                cmd.Parameters.AddWithValue("@salary", employee.salary);
                cmd.Parameters.AddWithValue("@JoiningDate", employee.JoiningDate);
                cmd.Parameters.AddWithValue("@ResignDate", employee.ResignDate);
                connection.Open();
                cmd.ExecuteNonQuery();
            }
        }



        public void Delete(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "Delete from Employee where Employee_PK= @Id";
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@Id", id);
                    connection.Open();
                    command.ExecuteNonQuery();
                }
            }
        }
        public bool UserExists(int id)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                string query = "SELECT COUNT(*) FROM Employee WHERE Employee_PK = @Id";
                SqlCommand command = new SqlCommand(query, connection);
                command.Parameters.AddWithValue("@Id", id);
                connection.Open();

                int count = Convert.ToInt32(command.ExecuteScalar());
                return count > 0;
            }
        }
    }
}
