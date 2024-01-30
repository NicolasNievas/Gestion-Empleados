using System.ComponentModel.DataAnnotations.Schema;

namespace GestionEmpleados.Models
{
    public class Employee
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        [ForeignKey("Charge")]
        public int ChargeId { get; set; }
        public Charge Charge { get; set; } 
        [ForeignKey("Sucursal")]
        public int SucursalId { get; set; }
        public Sucursal Sucursal { get; set; } 
        public string DNI { get; set; } = null!;
        public DateTime FechaAlta { get; set; }
        [ForeignKey("Jefe")]
        public int? JefeId { get; set; }
        public Employee Jefe { get; set; } 
        public virtual ICollection<Employee> Employees { get; set; } 
    }
}
