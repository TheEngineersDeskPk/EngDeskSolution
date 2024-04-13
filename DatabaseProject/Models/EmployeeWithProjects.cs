using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject.Models
{
    public class EmployeeWithProjects
    {
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }
        public string? City { get; set; }
        public DateTime DateofJoining { get; set; }
        public decimal Salary { get; set; }
        public List<string> Projects { get; set; }
    }
}
