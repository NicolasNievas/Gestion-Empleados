using AutoMapper;
using FluentValidation;
using GestionEmpleados.DTO;
using GestionEmpleados.Migrations;
using GestionEmpleados.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleados.CQRS.Commands
{
    public class PostEmployee
    {
        public class PostEmployeeCommand : IRequest<EmployeeDTO>
        {
            public string Name { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public int ChargeId { get; set; }
            public int SucursalId { get; set; }
            public string DNI { get; set; } = null!;
            public int? JefeId { get; set; }
        }
        public class PostEmployeeCommandValidator : AbstractValidator<PostEmployeeCommand>
        {
            private readonly ApplicationContext _context;
            public PostEmployeeCommandValidator(ApplicationContext context)
            {
                RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("El nombre no puede estar vacio ni ser nulo");
                RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("El apellido no puede estar vacio ni ser nulo");
                RuleFor(x => x.ChargeId).NotEmpty().NotNull().WithMessage("El cargo no puede estar vacio ni ser nulo");
                RuleFor(x => x.SucursalId).NotEmpty().NotNull().WithMessage("La sucursal no puede estar vacio ni ser nulo");
                RuleFor(x => x.DNI).NotEmpty().NotNull().WithMessage("El DNI no puede estar vacio ni ser nulo");
                RuleFor(x => x).MustAsync(ExistEmployee).WithMessage("El Empleado ya existe");
                _context = context;
            }

            private async Task<bool> ExistEmployee(PostEmployeeCommand command, CancellationToken token)
            {
                bool e = await _context.Employees.AnyAsync(x => x.DNI == command.DNI);
                return !e;
            }
        }
        public class PostEmployeeCommandHandler : IRequestHandler<PostEmployeeCommand, EmployeeDTO>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            private readonly IValidator<PostEmployeeCommand> _validator;
            public PostEmployeeCommandHandler(ApplicationContext context, IMapper mapper, IValidator<PostEmployeeCommand> validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }
            public async Task<EmployeeDTO> Handle(PostEmployeeCommand request, CancellationToken cancellationToken)
            {
                
                try
                {
                    var result = await _validator.ValidateAsync(request, cancellationToken);
                    if (!result.IsValid)
                    {
                        throw new ValidationException(result.Errors);
                    }
                    else
                    {
                        var employee = _mapper.Map<Employee>(request);
                        employee.FechaAlta = DateTime.Now.ToUniversalTime();
                        await _context.Employees.AddAsync(employee);
                        await _context.SaveChangesAsync();
                        var employeeDTO = _mapper.Map<EmployeeDTO>(employee);
                        return employeeDTO;
                    }
                }
                catch (Exception e)
                {
                    throw;
                }
            }
        }
    }
}
