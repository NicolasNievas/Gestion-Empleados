using FluentValidation;
using GestionEmpleados.Migrations;
using GestionEmpleados.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleados.CQRS.Commands
{
    public class DeleteEmployee
    {
        public class DeleteEmployeeCommand : IRequest<Employee>
        {
            public int Id { get; set; }
        }
        public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
        {
            private readonly ApplicationContext _context;
            public DeleteEmployeeCommandValidator(ApplicationContext context)
            {
                RuleFor(x => x.Id).NotEmpty().NotNull().WithMessage("Seleccione un empleado a eliminar");
                RuleFor(x => x.Id).MustAsync(ExistEmployee).WithMessage("El empleado no existe");
                _context = context;
            }

            private async Task<bool> ExistEmployee(int cmd, CancellationToken token)
            {
                bool e = await _context.Employees.AnyAsync(x => x.Id == cmd);
                return e;
            }
        }
        public class DeleteEmployeeCommandHandler : IRequestHandler<DeleteEmployeeCommand, Employee>
        {
            private readonly ApplicationContext _context;
            public DeleteEmployeeCommandHandler(ApplicationContext context)
            {
                _context = context;
            }
            public async Task<Employee> Handle(DeleteEmployeeCommand request, CancellationToken cancellationToken)
            {
                try
                {
                    var validator = new DeleteEmployeeCommandValidator(_context);
                    var result = await validator.ValidateAsync(request, cancellationToken);
                    if(!result.IsValid)
                    {
                        throw new ValidationException(result.Errors);
                    }
                    else
                    {
                        var employee = await _context.Employees.FindAsync(request.Id);
                        _context.Employees.Remove(employee);
                        await _context.SaveChangesAsync();
                        return employee;
                    }
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
        }
    }
}
