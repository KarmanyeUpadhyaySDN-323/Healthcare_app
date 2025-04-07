using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using StaffService.Model;
namespace StaffService.Dbfolder
{
    public class StaffDbContext : DbContext
    {
        public StaffDbContext(DbContextOptions<StaffDbContext> options) : base(options) { }
        public DbSet<Staff> Staffs { get; set; }
    }
}
