using AutoMapper;
using FluentValidation;
using GestionEmpleados.DTO;
using GestionEmpleados.Migrations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleados.CQRS.Commands
{
    public class PutEmployee
    {
        public class PutEmployeeCommand : IRequest<EmployeeDTO>
        {
            public int Id { get; set; }
            public string Name { get; set; } = null!;
            public string LastName { get; set; } = null!;
            public int ChargeId { get; set; }
            public int SucursalId { get; set; }
            public string DNI { get; set; } = null!;
            public int? JefeId { get; set; }
        }

        public class PutEmployeeCommandValidator : AbstractValidator<PutEmployeeCommand>
        {
            private readonly ApplicationContext _context;

            public PutEmployeeCommandValidator(ApplicationContext context)
            {
                _context = context;

                RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("El nombre no puede estar vacío ni ser nulo");
                RuleFor(x => x.LastName).NotEmpty().NotNull().WithMessage("El apellido no puede estar vacío ni ser nulo");
                RuleFor(x => x.ChargeId).NotEmpty().NotNull().WithMessage("El cargo no puede estar vacío ni ser nulo");
                RuleFor(x => x.SucursalId).NotEmpty().NotNull().WithMessage("La sucursal no puede estar vacía ni ser nula");
                RuleFor(x => x.DNI).NotEmpty().NotNull().WithMessage("El DNI no puede estar vacío ni ser nulo");
                RuleFor(x => x).MustAsync(ExistEmployee).WithMessage("El empleado no existe");
                RuleFor(x => x).MustAsync(NotModifyDNI).WithMessage("No se puede modificar el DNI del empleado");
            }

            private async Task<bool> ExistEmployee(PutEmployeeCommand command, CancellationToken token)
            {
                bool employeeExists = await _context.Employees.AnyAsync(x => x.Id == command.Id);
                return employeeExists;
            }

            private async Task<bool> NotModifyDNI(PutEmployeeCommand command, CancellationToken token)
            {
                var existingEmployee = await _context.Employees.FindAsync(command.Id);
                return existingEmployee != null && existingEmployee.DNI == command.DNI;
            }
        }

        public class PutEmployeeCommandHandler : IRequestHandler<PutEmployeeCommand, EmployeeDTO>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            private readonly IValidator<PutEmployeeCommand> _validator;

            public PutEmployeeCommandHandler(ApplicationContext context, IMapper mapper, IValidator<PutEmployeeCommand> validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }

            public async Task<EmployeeDTO> Handle(PutEmployeeCommand request, CancellationToken cancellationToken)
            {
                var validationResult = await _validator.ValidateAsync(request, cancellationToken);

                if (!validationResult.IsValid)
                {
                    throw new ValidationException(validationResult.Errors);
                }
                else
                {
                    var existingEmployee = await _context.Employees.FindAsync(request.Id);

                    if (existingEmployee == null)
                    {
                        throw new Exception($"No se encontró un empleado con ID {request.Id}");
                    }

                    existingEmployee.Name = request.Name;
                    existingEmployee.LastName = request.LastName;
                    existingEmployee.ChargeId = request.ChargeId;
                    existingEmployee.SucursalId = request.SucursalId;
                    existingEmployee.JefeId = request.JefeId;

                    await _context.SaveChangesAsync();

                    var updatedEmployeeDTO = _mapper.Map<EmployeeDTO>(existingEmployee);
                    return updatedEmployeeDTO;
                }
                throw new Exception("No se pudo actualizar el empleado");
            }
        }
    }

}
