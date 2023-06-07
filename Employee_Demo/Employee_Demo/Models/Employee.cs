using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Employee_Demo.Models
{
    public enum Gender
    {
        other = 0,
        male = 1,
        female = 2
    }
    [Table("Employee")]
    public partial class Employee
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Employee_PK { get; set; }
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string MiddleName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int EmpCode { get; set; }

        public Gender Gender { get; set; }
        [Required]
        public System.DateTime DOB { get; set; }
        [Required]
        public Nullable<decimal> salary { get; set; }
        [Required]
        public System.DateTime JoiningDate { get; set; }
        public Nullable<System.DateTime> ResignDate { get; set; }
    }
}