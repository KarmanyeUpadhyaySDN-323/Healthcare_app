using Microsoft.EntityFrameworkCore;
using PatientService.Model;

namespace PatientService.DbCont
{
    public class PatientDbContext : DbContext
    {
        public PatientDbContext(DbContextOptions<PatientDbContext> options) : base(options) { }
        public DbSet<Patient> Patients { get; set; }
    }
}
