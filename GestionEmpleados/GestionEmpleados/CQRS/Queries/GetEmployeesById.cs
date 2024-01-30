using AutoMapper;
using FluentValidation;
using GestionEmpleados.DTO;
using GestionEmpleados.Migrations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleados.CQRS.Queries
{
    public class GetEmployeesById
    {
        public class GetEmployeeByIdQuery : IRequest<EmployeeDTO>
        {
            public int Id { get; set; }
        }
        public class GetEmployeeByIdQueryValidators : AbstractValidator<GetEmployeeByIdQuery>
        {
            private readonly ApplicationContext _context;
            public GetEmployeeByIdQueryValidators(ApplicationContext context)
            {
                RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("El campo Id no puede estar vacío");
                RuleFor(x => x.Id).MustAsync(ExistEmployee).WithMessage("El empleado no existe");
                _context = context;
            }

            private async Task<bool> ExistEmployee(int cmd, CancellationToken token)
            {
                bool e = await _context.Employees.AnyAsync(x => x.Id == cmd);
                return e;
            }
        }
        public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, EmployeeDTO>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetEmployeeByIdQueryHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<EmployeeDTO> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
            {
                var validator = new GetEmployeeByIdQueryValidators(_context);
                var result = await validator.ValidateAsync(request);
                if (!result.IsValid)
                {
                    throw new ValidationException(result.Errors);
                }
                else
                {
                    var employees = await _context.Employees.Include(e => e.Charge).Include(e => e.Sucursal.City)
                        .Include(e => e.Jefe)
                        .ThenInclude(jefe => jefe.Charge) 
                        .Include(e => e.Jefe) 
                        .ThenInclude(jefe => jefe.Jefe) 
                        .ThenInclude(jefe => jefe.Charge).FirstOrDefaultAsync(e => e.Id == request.Id);
                    return _mapper.Map<EmployeeDTO>(employees);
                }
            }
        }
    }
}
