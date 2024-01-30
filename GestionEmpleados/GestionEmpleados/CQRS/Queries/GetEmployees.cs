using AutoMapper;
using GestionEmpleados.DTO;
using GestionEmpleados.Migrations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleados.CQRS.Queries
{
    public class GetEmployees
    {
        public class GetEmployeeQuery : IRequest<List<EmployeeDTO>>
        {
        }
        public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeeQuery, List<EmployeeDTO>>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetEmployeesQueryHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<EmployeeDTO>> Handle(GetEmployeeQuery request, CancellationToken cancellationToken)
            {
                var employees = await _context.Employees.Include(e => e.Charge).Include(e => e.Sucursal.City).ToListAsync();
                return _mapper.Map<List<EmployeeDTO>>(employees);
            }
        }
    }
}
