using DatabaseProject.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseProject.Interfaces
{
    public interface IEmployeeRepository
    {
        List<Employee> GetEmployees();

        Employee GetEmployeeById(int id);

        Employee AddEmployee(Employee employee);


        Employee GetEmployeeById_AdoNet(int id);

        List<QnABank> GetQnABank();

        List<EmployeeWithProjects> GetEmployeesAndProjects();


    }
}
