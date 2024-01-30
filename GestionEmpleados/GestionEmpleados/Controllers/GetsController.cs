using GestionEmpleados.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GestionEmpleados.CQRS.Queries.GetEmployees;
using static GestionEmpleados.CQRS.Queries.GetCharges;
using static GestionEmpleados.CQRS.Queries.GetUsers;
using static GestionEmpleados.CQRS.Queries.GetSucursales;
using static GestionEmpleados.CQRS.Queries.GetCitys;
using static GestionEmpleados.CQRS.Queries.GetEmployeesById;

namespace GestionEmpleados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public GetsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        [Route("/Get/Employees")]
        public async Task<List<EmployeeDTO>> GetEmployees()
        {
            return await _mediator.Send(new GetEmployeeQuery());
        }
        [HttpGet]
        [Route("/Get/Charges")]
        public async Task<List<ChargeDTO>> GetCharges()
        {
            return await _mediator.Send(new GetChargesQuery());
        }
        [HttpGet]
        [Route("/Get/Users")]
        public async Task<List<UserDTO>> GetUsers()
        {
            return await _mediator.Send(new GetUserQuery());
        }
        [HttpGet]
        [Route("/Get/Sucursales")]
        public async Task<List<SucursalDTO>> GetSucursales()
        {
            return await _mediator.Send(new GetSucursalQuery());
        }
        [HttpGet]
        [Route("Get/City")]
        public async Task<List<CityDTO>> GetCities()
        {
            return await _mediator.Send(new GetCityQuery());
        }
        [HttpGet]
        [Route("/Get/Employee/{id}")]
        public async Task<EmployeeDTO> GetEmployee(int id)
        {
            return await _mediator.Send(new GetEmployeeByIdQuery { Id = id });
        }
    }
}
