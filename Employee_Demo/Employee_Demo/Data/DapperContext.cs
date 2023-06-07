using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using Dapper;
using Employee_Demo.Models;

namespace Employee_Demo.Data
{
  
public class DapperContext
{
    private readonly string connectionString;

    public DapperContext(string connectionString)
    {
        this.connectionString = connectionString;
    }

    private IDbConnection CreateConnection()
    {
        return new SqlConnection(connectionString);
    }

    public IEnumerable<Employee> GetAllEmployee()
    {
        using (var connection = CreateConnection())
        {
            connection.Open();
            return connection.Query<Employee>("SELECT * FROM Employee");
        }
    }

    public Employee GetEmployeeById(int id)
    {
        using (var connection = CreateConnection())
        {
            connection.Open();
            return connection.QuerySingleOrDefault<Employee>("SELECT * FROM Employee where Employee_PK= @Id", new { Id = id });
        }
    }

    public void InsertEmployee(Employee employee)
    {
        using (var connection = CreateConnection())
        {
            connection.Open();
            connection.Execute("INSERT INTO Employee (FirstName,MiddleName,LastName,EmpCode,Gender,DOB,salary,JoiningDate,ResignDate) VALUES (@firstName,@middleName,@lastName,@empCode,@gender,@dob,@Salary,@joiningDate,@resignDate);", employee);
        }
    }

    public void UpdateEmployee(Employee employee)
    {
        using (var connection = CreateConnection())
        {
            connection.Open();
            connection.Execute("Update Employee set FirstName=@FirstName,MiddleName =@MiddleName,LastName =@LastName,EmpCode=@EmpCode,Gender=@gender,salary=@salary,JoiningDate=@JoiningDate,ResignDate=@ResignDate Where Employee_PK== @Id", employee);
        }
    }

    public void DeleteProduct(int id)
    {
        using (var connection = CreateConnection())
        {
            connection.Open();
            connection.Execute("Delete From Employee  Where Employee_PK = @Id", new { Id = id });
        }
    }
}

}