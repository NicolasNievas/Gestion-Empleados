using AutoMapper;
using FluentValidation;
using GestionEmpleados.DTO;
using GestionEmpleados.Migrations;
using GestionEmpleados.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleados.CQRS.Commands
{
    public class NewUser
    {
        public class NewUserCommand : IRequest<UserDTO>
        {
            public string Name { get; set; } = null!;
            public string Password { get; set; } = null!;
        }
        public class NewUserCommandValidator : AbstractValidator<NewUserCommand>
        {
            private readonly ApplicationContext _context;
            public NewUserCommandValidator(ApplicationContext context)
            {
                RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Ingrese un nombre de usuario valido");
                RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Ingrese una contraseña valida");
                RuleFor(x => x.Password).MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres");
                RuleFor(x => x.Password).Matches(@"[A-Z]+").WithMessage("La contraseña debe tener al menos una mayuscula");
                RuleFor(x => x.Password).Matches(@"[a-z]+").WithMessage("La contraseña debe tener al menos una minuscula");
                RuleFor(x => x.Password).Matches(@"[0-9]+").WithMessage("La contraseña debe tener al menos un numero");
                RuleFor(x => x).MustAsync(ExistUser).WithMessage("El usuario ya existe");
                _context = context;
            }

            private async Task<bool> ExistUser(NewUserCommand command, CancellationToken token)
            {
                bool e = await _context.Users.AnyAsync(x => x.Name == command.Name);
                return !e;
            }
        }
        public class NewUserCommandHandler : IRequestHandler<NewUserCommand, UserDTO>
        {
            private readonly ApplicationContext _context;
            private readonly IValidator<NewUserCommand> _validator;
            private readonly IMapper _mapper;
            public NewUserCommandHandler(ApplicationContext context, IValidator<NewUserCommand> validator, IMapper mapper)
            {
                _context = context;
                _validator = validator;
                _mapper = mapper;
            }
            public async Task<UserDTO> Handle(NewUserCommand request, CancellationToken cancellationToken)
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
                        var user = _mapper.Map<User>(request);
                        await _context.Users.AddAsync(user);
                        await _context.SaveChangesAsync();
                        return _mapper.Map<UserDTO>(user);
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
