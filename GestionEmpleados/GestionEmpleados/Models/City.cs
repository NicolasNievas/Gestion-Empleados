namespace GestionEmpleados.Models
{
    public class City
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public virtual ICollection<Sucursal> Sucursals { get; set; } 
    }
}
