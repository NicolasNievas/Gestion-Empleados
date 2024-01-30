using GestionEmpleados.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleados.Migrations
{
    public class ApplicationContext : DbContext
    {
        public ApplicationContext() { }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
        public virtual DbSet<Charge> Charges { get; set; } 
        public virtual DbSet<City> Cities { get; set; }
        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Sucursal> Sucursals { get; set; }
        public virtual DbSet<User> Users { get; set; }
    }
}
