using Employee_Demo.Data;
using Employee_Demo.Models;
using System;
using System.Collections.Generic;

using System.Net;
using System.Web.Http;


namespace Employee_Demo.Controllers
{

    public class AdoController : ApiController
    {

        private readonly AdoContext db;

        public AdoController()
        {
            db = new AdoContext();
        }
        public IEnumerable<Employee> Get()
        {
            return db.GetAllEmployee();
        }

        // GET api/users/{id}
        public IHttpActionResult Get(int id)
        {
            var user = db.GetByID(id);
            if (user == null)
                return NotFound();

            return Ok(user);
        }

        // POST api/users
        public IHttpActionResult Post(Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            db.Post(employee);
            return CreatedAtRoute("DefaultApi", new { id = employee.Employee_PK }, employee);
        }

        // PUT api/users/{id}
        public IHttpActionResult Put(int id, Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            if (!db.UserExists(id))
                return NotFound();

            db.Put(id, employee);
            return StatusCode(HttpStatusCode.NoContent);
        }

        // DELETE api/users/{id}
        public IHttpActionResult Delete(int id)
        {
            if (!db.UserExists(id))
                return NotFound();

            db.Delete(id);
            return StatusCode(HttpStatusCode.NoContent);
        }

    }


}