namespace GestionEmpleados.DTO
{
    public class EmployeeDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public ChargeDTO Charge { get; set; } 
        public SucursalDTO Sucursal { get; set; } 
        public string DNI { get; set; } = null!;
        public EmployeeDTO? Jefe { get; set; }
    }
}
