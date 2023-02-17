using EmployeeCommon.Models;
using Microsoft.EntityFrameworkCore;
namespace EmployeeCommon.Data
{
    public class EmployeeDbContext : DbContext
    {
        public EmployeeDbContext(DbContextOptions options): base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }

    }
}
