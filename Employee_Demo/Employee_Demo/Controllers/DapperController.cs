using Employee_Demo.Models;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Web.Mvc;
using Dapper;
using System.Configuration;
using System.Web.Http;
using Employee_Demo.Data;

namespace Employee_Demo.Controllers
{
    public class DapperController : ApiController
    {
        private readonly DapperContext db;

        public DapperController()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["connectionString"].ConnectionString;

            db = new DapperContext(connectionString);
        }

        // GET api/product
        public IEnumerable<Employee> Get()
        {
            return db.GetAllEmployee();
        }

        // GET api/product/5
        public IHttpActionResult Get(int id)
        {
            var product = db.GetEmployeeById(id);
            if (product == null)
                return NotFound();

            return Ok(product);
        }

        // POST api/product
        public IHttpActionResult Post(Employee employee)
        {
            db.InsertEmployee(employee);
            return Ok();
        }

        // PUT api/product/5
        public IHttpActionResult Put(int id, Employee employee)
        {
            var existingEmployee= db.GetEmployeeById(id);
            if (existingEmployee == null)
                return NotFound();

            existingEmployee.FirstName = employee.FirstName;
            existingEmployee.LastName = employee.LastName;
            db.UpdateEmployee(existingEmployee);

            return Ok();
        }

        // DELETE api/product/5
        
        public IHttpActionResult Delete(int id)
        {
            var existingEmployee = db.GetEmployeeById(id);
            if (existingEmployee == null)
                return NotFound();

            db.DeleteProduct(id);

            return Ok();
        }
    }
}