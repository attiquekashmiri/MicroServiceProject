using Common.Helpers;
using CommonService.Common.ViewModels;
using EmployeeCommon.Shared.ViewModel;
using EmployeeService.Services.EmployeesManagement;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmployeeService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeeController : ControllerBase
    {
        private readonly IEmployeesService _employeesService;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _environment;
        public EmployeeController(IEmployeesService employeesService, Microsoft.AspNetCore.Hosting.IHostingEnvironment environment)
        {
            _employeesService = employeesService;
            ConstantHelpers.BaseUrl = environment.ContentRootPath;
        }

        // GET: api/<EmployeeController>
        [Route("GetAllItems")]
        [HttpGet]
        public IEnumerable<EmployeeVM> GetAllItems()
        {
            return _employeesService.GetAllEmployees();
        }

        // GET api/<EmployeeController>/5
        [Route("SearchEmployee")]
        [HttpGet]
        public IEnumerable<EmployeeVM> SearchEmployee(string text)
        {
            return _employeesService.SearchEmployees(text);
        }

        // POST api/<EmployeeController>'
        [Route("AddEmployee")]
        [HttpPost]
        public bool AddEmployee(EmployeeVM employee)
        {
            return _employeesService.AddEmployee(employee);
        }

        // PUT api/<EmployeeController>/5
        [Route("UpdateEmployee")]
        [HttpPut]
        public bool UpdateEmployee(EmployeeVM employee)
        {
            return _employeesService.UpdateEmployee(employee);
        }
        [Route("DisableEmployee")]
        [HttpPut]
        public bool DisableEmployee(long id)
        {
            return _employeesService.DisableEmployee(id);
        }
        [Route("EnableEmployee")]
        [HttpPut]
        public bool EnableEmployee(long id)
        {
            return _employeesService.DisableEmployee(id);
        }

        // DELETE api/<EmployeeController>/5
        [Route("DeleteEmployee")]
        [HttpDelete]
        public void DeleteEmployee(int id)
        {
            _employeesService.DeleteEmployee(id);
        }
    }
}
