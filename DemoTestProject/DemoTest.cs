
using DatabaseProject.Interfaces;
using DatabaseProject.Models;
using EngineersDeskAPI.Controllers;
using LazyCache;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace DemoTestProject
{
    [TestClass]
    public class DemoTest
    {
        [TestMethod]
        public void TestGetEmployeeBy_Id_Method()
        {
            var employee = new Employee
            {
                EmployeeId = 1,
                EmployeeName = "Pankaj",
                City = "Delhi",
                DateofJoining = System.DateTime.MaxValue,
                Salary = 134000
            };

            var employeeRepository = new Mock<IEmployeeRepository>();
            var mockRepo = new Mock<ICacheProvider>();
            employeeRepository.Setup(x => x.GetEmployeeById(It.IsAny<int>())).Returns(employee);
            var controller = new EmployeeController(employeeRepository.Object, mockRepo.Object);

            var getEmployeeById = controller.GetEmployeeById(1);
            Assert.IsNotNull(getEmployeeById);

        }

        [TestMethod]
        public void TestGetSum()
        {

            int x = 9, y = 7;

            var result = GetSum(x, y);
            Assert.AreEqual(x + y, result);
            Assert.AreNotEqual(x - y, result);
        }

        [TestMethod]
        public void TestGetFullName()
        {
            string firstName = "Pankaj";
            string lastName = "singh";
            var result = GetFullName(firstName, lastName);
            Assert.AreEqual(firstName + lastName, result);
            Assert.IsNotNull(result);
        }

        private int GetSum(int x, int y)
        {
            return x + y;
        }

        private string GetFullName(string firstName, string lastName)
        {
            string fullName = firstName + lastName;
            return fullName;
        }

    }
}
