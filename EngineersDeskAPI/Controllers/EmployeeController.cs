using DatabaseProject.Interfaces;
using DatabaseProject.Models;
using EngineersDeskAPI.Caching;
using LazyCache;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;

namespace EngineersDeskAPI.Controllers
{
    [Route("api/[controller]/[Action]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeeRepository _EmployeeRepository;
        private ICacheProvider _CacheProvider;

        public EmployeeController (IEmployeeRepository employeeRepository, ICacheProvider cacheProvider)
        {
            _EmployeeRepository = employeeRepository;
            _CacheProvider = cacheProvider;
        }

        [HttpGet]

        public ActionResult GetEmployees()
        {
            if(!_CacheProvider.TryGetValue(CacheKeys.Employee, out List<Employee> employees))
            {
                employees = _EmployeeRepository.GetEmployees();

                var cacheEntryOption = new MemoryCacheEntryOptions
                {
                    AbsoluteExpiration = DateTime.Now.AddSeconds(30),
                    SlidingExpiration = TimeSpan.FromSeconds(30),
                    Size = 1024
                };
                _CacheProvider.Set(CacheKeys.Employee, employees, cacheEntryOption);
            }
            return Ok(employees);
        }

        [HttpGet]

        public ActionResult GetEmployeeById(int Id)
        {
            try
            {
                var employee = _EmployeeRepository.GetEmployeeById(Id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex.Message);
            }
        }

        [HttpGet]

        public ActionResult GetEmployeeById_AdoNet(int Id)
        {
            try
            {
                var employee = _EmployeeRepository.GetEmployeeById_AdoNet(Id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex.Message);
            }
        }



        [HttpPost]
        public ActionResult AddEmployee(Employee employee )
        {
            try
            {
                var addedEmployee = _EmployeeRepository.AddEmployee(employee);
                return Ok(addedEmployee);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex.Message);
            }

        }

        [HttpGet]
        public ActionResult GetQnABank()
        {
            try
            {
                var employees = _EmployeeRepository.GetQnABank();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status417ExpectationFailed, ex.Message);
            }
        }


        [HttpGet]
        public ActionResult GetPnrDetails(string pnrNumber)
        {
            if(pnrNumber == "4335734389")
            {
                var res = System.IO.File.ReadAllText("D:\\YouTubeDemoCode\\DatabaseProject\\Models\\PnrDetails.json");
                return Ok(res);
            }
            else
            {
                return StatusCode(StatusCodes.Status404NotFound);
            }
          
        }

        [HttpGet]
        public ActionResult GetEmployeesAndProjects()
        {
            var result = _EmployeeRepository.GetEmployeesAndProjects();
            return Ok(result);
        }


    }
}