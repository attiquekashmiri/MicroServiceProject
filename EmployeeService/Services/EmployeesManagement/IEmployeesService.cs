

using EmployeeCommon.Shared.ViewModel;

namespace EmployeeService.Services.EmployeesManagement
{
    public interface IEmployeesService
    {
        public bool AddEmployee(EmployeeVM employee);
        public List<EmployeeVM> GetAllEmployees();
        public List<EmployeeVM> SearchEmployees(string query);
        public bool DeleteEmployee(long id);
        public bool UpdateEmployee(EmployeeVM employee);
        public bool EnableEmployee(long id);
        public bool DisableEmployee(long id);
    }
}
