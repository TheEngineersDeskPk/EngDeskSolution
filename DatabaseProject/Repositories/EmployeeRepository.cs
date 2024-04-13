using DatabaseProject.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DatabaseProject.DatabaseContext;
using DatabaseProject.Models;
using System.Data.SqlClient;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DatabaseProject.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly SqlServerContext _SqlServerContext;

        public EmployeeRepository(SqlServerContext sqlServerContext)
        {
            _SqlServerContext = sqlServerContext;
        }


        public List<Employee> GetEmployees()
        {
            var lstEmployees = _SqlServerContext.Employee.ToList();
            return lstEmployees;

        }



        public Employee GetEmployeeById(int id)
        {
            IEnumerable<Employee> employees = _SqlServerContext.Employee;
            var employee = employees.FirstOrDefault(x=>x.EmployeeId == id);
            return employee;
        }

        public Employee AddEmployee(Employee employee)
        {
            _SqlServerContext.Employee.Add(employee);
            _SqlServerContext.SaveChanges();
            return employee;
        }


        public Employee GetEmployeeById_AdoNet(int id)
        {
            Employee emp = new Employee();

            using (SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=EngineersDesk;Trusted_Connection=true";
                connection.Open();

                SqlCommand cmd = new SqlCommand("sp_employees", connection);
                cmd.Parameters.Add(new SqlParameter("Id", id));

                using (SqlDataReader reader = cmd.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        Employee employee = new Employee()
                        {
                            EmployeeId = (int)reader["EmployeeId"],
                            EmployeeName = (string)reader["EmployeeName"],
                            City = (string)reader["City"],
                            DateofJoining = (DateTime)reader["DateofJoining"],
                            Salary = (decimal)reader["Salary"]
                        };
                        emp = employee;
                    }
                }
            }
            return emp;
        }


        public List<QnABank> GetQnABank()
        {
            var qnabankques = _SqlServerContext.QuestionBank.ToList();

            int length = qnabankques.Where(x => x.isCorrect == true).Count();


            List<QnABank> QnABankLst = new List<QnABank>();
            for (int i = 1; i <= length; i++)
            {
                QnABankLst.Add(BuildListQnA(qnabankques, i));
            }


            return QnABankLst;



        }

        private QnABank BuildListQnA(List<QuestionBank> questionbank, int i)
        {
            var qnabankques = questionbank.Where(x => x.QuestionId == i);

            List<Answers> answers = new List<Answers>();
            foreach (var x in qnabankques)
            {
                var Answers = new Answers()
                {
                    Answer = x.Answer,
                    isCorrect = x.isCorrect,
                };
                answers.Add(Answers);
            }

            QnABank qnABank = new QnABank()
            {
                Question = qnabankques.Select(x => x.Question).FirstOrDefault(),
                Answers = answers
            };

            return qnABank;
        }

        //create a new method
        public List<EmployeeWithProjects> GetEmployeesAndProjects()
        {
            List<EmployeeWithProjects> employees = new List<EmployeeWithProjects>();

            using(SqlConnection connection = new SqlConnection())
            {
                connection.ConnectionString = "Server=localhost\\SQLEXPRESS;Database=EngineersDesk;Trusted_Connection=true";
                connection.Open();

                SqlCommand cmd = new SqlCommand("sp_Employee_Project", connection);

                using(SqlDataReader reader = cmd.ExecuteReader())
                {
                   Dictionary<int, EmployeeWithProjects> dictEmployee = new Dictionary<int, EmployeeWithProjects>();
                    while(reader.Read())
                    {
                        var employeeId = (int)reader["EmployeeId"];
                        if(!dictEmployee.ContainsKey(employeeId))
                        {
                            var employee = new EmployeeWithProjects()
                            {
                                EmployeeId = employeeId,
                                EmployeeName = reader["EmployeeName"].ToString(),
                                Salary = (decimal)reader["Salary"],
                                City = reader["City"].ToString(),
                                DateofJoining = (DateTime)reader["DateofJoining"],
                                Projects = new List<string>()
                            };
                            dictEmployee.Add(employeeId, employee);
                        }
                        dictEmployee[employeeId].Projects.Add(reader["ProjectName"].ToString());
                    }
                    employees.AddRange(dictEmployee.Values);
                }
            }

            return employees;
        }
    }
}