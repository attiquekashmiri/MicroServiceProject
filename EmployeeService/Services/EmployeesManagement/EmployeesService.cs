using Common.Helpers;
using CommonService.Common.ViewModels;
using EmployeeCommon.Data;
using EmployeeCommon.Models;
using EmployeeCommon.Shared.ViewModel;
using System.Diagnostics.Metrics;
using System.IO;
using System.Reflection.Emit;

namespace EmployeeService.Services.EmployeesManagement
{
    public class EmployeesService : IEmployeesService
    {
        private readonly EmployeeDbContext _context;
        ConstantHelpers ConstantHelpers = new ConstantHelpers();
        public EmployeesService(EmployeeDbContext context)
        {
            _context = context;
        }
        #region Employee CRUD
        public bool AddEmployee(EmployeeVM employee)
        {
            try
            {
                if (!string.IsNullOrEmpty(employee.EmployeeCode) && !string.IsNullOrEmpty(employee.FirstName) && !string.IsNullOrEmpty(employee.LastName) && !_context.Employees.Any(x=>x.EmployeeCode == employee.EmployeeCode))
                {
                    Employee entry = new Employee()
                    {
                        EmployeeCode = employee.EmployeeCode,
                        FirstName = employee.FirstName,
                        LastName = employee.LastName,
                        DateOfBirth = employee.DateOfBirth,
                        JoiningDate = employee.JoiningDate,
                        isActive = employee.isActive,
                        AddedDateTime = DateTime.Now,
                        City= employee.City,
                        Country= employee.Country,
                        Salary= employee.Salary,
                        Street= employee.Street,
                        ZipCode= employee.ZipCode,
                        PositionTitle= employee.PositionTitle,
                    };
                    _context.Employees.Add(entry);
                    Save();
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                ConstantHelpers.ApiLogs("AddEmployee", e.ToString(), false);
                return false;
            }
        }

        public bool DeleteEmployee(long id)
        {
            try
            {
                if (id>0)
                {
                    var employee= _context.Employees.Where(x=>x.Id == id).FirstOrDefault();
                    if (employee!=null) { _context.Employees.Remove(employee); Save(); }
                    return true;
                }
                return false;
            }
            catch (Exception e)
            {
                ConstantHelpers.ApiLogs("DeleteEmployee", e.ToString(), false);
                return false;
            }
        }

        public bool DisableEmployee(long id)
        {
            try
            {
                var employee = _context.Employees.Where(x => x.Id == id && x.isActive==true).FirstOrDefault();
                if (employee != null) { employee.isActive = false; Save(); }
                return true;
            }
            catch (Exception e)
            {

                ConstantHelpers.ApiLogs("DisableEmployee", e.ToString(), false);
                return false;
            }
        }

        public bool EnableEmployee(long id)
        {
            try
            {
                var employee = _context.Employees.Where(x => x.Id == id && x.isActive == false).FirstOrDefault();
                if (employee != null) { employee.isActive = true; Save(); }
                return true;
            }
            catch (Exception e)
            {

                ConstantHelpers.ApiLogs("EnableEmployee", e.ToString(), false);
                return false;
            }
        }

        public List<EmployeeVM> GetAllEmployees()
        {
            try
            {
                var result = _context.Employees.Select(employee => new EmployeeVM()
                {
                    Id= employee.Id,
                    EmployeeCode = employee.EmployeeCode,
                    FirstName = employee.FirstName,
                    LastName = employee.LastName,
                    DateOfBirth = employee.DateOfBirth,
                    JoiningDate = employee.JoiningDate,
                    isActive = employee.isActive,
                    City = employee.City,
                    Country = employee.Country,
                    Salary = employee.Salary,
                    Street = employee.Street,
                    ZipCode = employee.ZipCode,
                    PositionTitle = employee.PositionTitle,
                }).ToList();
                return result != null && result.Count > 0 ? result.OrderBy(x => ConstantHelpers.PadNumbers(x.FirstName)).ToList()
                   : new List<EmployeeVM>();

            }
            catch (Exception e)
            {
                ConstantHelpers.ApiLogs("GetAllEmployees", e.ToString(), false);
                return new List<EmployeeVM>(); 
            }
        }

        public List<EmployeeVM> SearchEmployees(string query)
        {
            try
            {
                if (!string.IsNullOrEmpty(query))
                {
                    var employees = _context.Employees.Where(x => x.EmployeeCode == query || x.FirstName.ToLower().Contains(query.ToLower()) || x.LastName.ToLower().Contains(query.ToLower()) || (x.FirstName + " " + x.LastName).ToLower().Contains(query.ToLower()))
                        .Select(employee => new EmployeeVM()
                        {
                            Id = employee.Id,
                            EmployeeCode = employee.EmployeeCode,
                            FirstName = employee.FirstName,
                            LastName = employee.LastName,
                            DateOfBirth = employee.DateOfBirth,
                            JoiningDate = employee.JoiningDate,
                            isActive = employee.isActive,
                            City = employee.City,
                            Country = employee.Country,
                            Salary = employee.Salary,
                            Street = employee.Street,
                            ZipCode = employee.ZipCode,
                            PositionTitle = employee.PositionTitle,
                        })
                        .ToList();
                    return employees != null && employees.Count > 0 ? employees.OrderBy(x => ConstantHelpers.PadNumbers(x.FirstName)).ToList()
                   : new List<EmployeeVM>();
                }
                return new List<EmployeeVM>();
            }
            catch (Exception e)
            {
                ConstantHelpers.ApiLogs("SearchEmployees", e.ToString(), false);
                return new List<EmployeeVM>();
            }
        }

        public bool UpdateEmployee(EmployeeVM employee)
        {
            try
            {
                if (employee.Id>0 && !string.IsNullOrEmpty(employee.EmployeeCode) && !string.IsNullOrEmpty(employee.FirstName) && !string.IsNullOrEmpty(employee.LastName) && !_context.Employees.Any(x => x.EmployeeCode == employee.EmployeeCode && x.Id !=employee.Id))
                {
                    var result = _context.Employees.Where(x => x.Id == employee.Id).FirstOrDefault();
                    if (result != null) {

                        result.EmployeeCode = employee.EmployeeCode;
                        result.FirstName = employee.FirstName;
                        result.LastName = employee.LastName;
                        result.DateOfBirth = employee.DateOfBirth;
                        result.JoiningDate = employee.JoiningDate;
                        result.isActive = employee.isActive;
                        result.UpdatedDateTime = DateTime.Now;
                        result.City = employee.City;
                        result.Country = employee.Country;
                        result.Salary = employee.Salary;
                        result.Street = employee.Street;
                        result.ZipCode = employee.ZipCode;
                        result.PositionTitle = employee.PositionTitle;
                        Save();
                        return true;
                    }
                }
                return false;
            }
            catch (Exception e)
            {
                ConstantHelpers.ApiLogs("UpdateEmployee", e.ToString(), false);
                return false;
            }
        }
        #endregion

        #region private methods
        private void Save()
        {
            _context.SaveChanges();
        }
        #endregion
    }
}
