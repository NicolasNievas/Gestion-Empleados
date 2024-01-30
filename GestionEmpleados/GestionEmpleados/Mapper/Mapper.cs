using AutoMapper;
using GestionEmpleados.CQRS.Commands;
using GestionEmpleados.DTO;
using GestionEmpleados.Models;
using static GestionEmpleados.CQRS.Commands.PostCharge;
using static GestionEmpleados.CQRS.Commands.PostCity;
using static GestionEmpleados.CQRS.Commands.PostEmployee;

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
            CreateMap<Charge, PostChargeCommand>().ReverseMap();
            CreateMap<City, PostCityCommand>().ReverseMap();
            CreateMap<Employee, PostEmployeeCommand>().ReverseMap();
        }
    }
}
