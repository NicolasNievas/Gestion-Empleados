namespace GestionEmpleados.DTO
{
    public class SucursalDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public CityDTO City { get; set; } 
    }
}
