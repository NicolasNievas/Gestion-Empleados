using System.ComponentModel.DataAnnotations.Schema;

namespace GestionEmpleados.Models
{
    public class Sucursal
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        [ForeignKey("City")]
        public int CityId { get; set; }
        public City City { get; set; } 
        public virtual ICollection<Employee> Employees { get; set; }
    }
}
