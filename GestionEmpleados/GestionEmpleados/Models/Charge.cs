namespace GestionEmpleados.Models
{
    public class Charge
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual ICollection<Employee> Employees { get; set; } 
    }
}
