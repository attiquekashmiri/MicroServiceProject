using CommonService.Models;
using Microsoft.EntityFrameworkCore;
namespace CommonService.Data
{
    public class AppDbContext : DbContext
    {
        //protected readonly IConfiguration Configuration;
        public AppDbContext(DbContextOptions options): base(options)
        {
            //Configuration = configuration;
        }
        // protected override void OnConfiguring(DbContextOptionsBuilder options)
        //{
        //    // connect to sql server with connection string from app settings
        //    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
        //}
        public DbSet<Item> Items { get; set; }

    }
}
