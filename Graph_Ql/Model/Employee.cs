using Dapper.Contrib.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace Graph_Ql.Model
{
    public class Employee
    {
        [ExplicitKey]
        public int EmployeeId { get; set; }

        [Required]
        public string EmployeeName { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [Range(minimum: 20, maximum: 50)]
        public int Age { get; set; }

        public int DepartmentId { get; set; }

        public Department Department { get; set; }
        public string Name { get; internal set; }
    }
}
