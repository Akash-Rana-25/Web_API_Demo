using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Employee_Demo.Models
{
    
        public class EmployeeDb : DbContext
        {
            public EmployeeDb()
               : base("EmployeeDb")
            {
            }

            public DbSet<Employee> Employees { get; set; }
        }
    
}