using AutoMapper;
using GestionEmpleados.DTO;
using GestionEmpleados.Models;

namespace GestionEmpleados.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            CreateMap<City, CityDTO>().ReverseMap();
            CreateMap<Charge, ChargeDTO>().ReverseMap();
            CreateMap<Sucursal, SucursalDTO>().ReverseMap();
            CreateMap<Employee, EmployeeDTO>().ReverseMap();
            CreateMap<User, UserDTO>().ReverseMap();
        }
    }
}
