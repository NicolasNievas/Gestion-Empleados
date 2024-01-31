using AutoMapper;
using FluentValidation;
using GestionEmpleados.DTO;
using GestionEmpleados.Migrations;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleados.CQRS.Commands
{
    public class Login
    {
        public class LoginCommand : IRequest<UserDTO>
        {
            public string Name { get; set; }
            public string Password { get; set; }
        }
        public class LoginCommandValidator : AbstractValidator<LoginCommand>
        {
            private readonly ApplicationContext _context;
            public LoginCommandValidator(ApplicationContext context)
            {
                RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("Ingrese un usuario valido");
                RuleFor(x => x.Password).NotEmpty().NotNull().WithMessage("Ingrese una contraseña valida");
                RuleFor(x => x.Password).MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres");
                RuleFor(x => x.Password).Matches(@"[A-Z]+").WithMessage("La contraseña debe tener al menos una mayuscula");
                RuleFor(x => x.Password).Matches(@"[a-z]+").WithMessage("La contraseña debe tener al menos una minuscula");
                RuleFor(x => x.Password).Matches(@"[0-9]+").WithMessage("La contraseña debe tener al menos un numero");
                RuleFor(x => x).MustAsync(ExistUser).WithMessage("El usuario no existe");
                _context = context;
            }

            private async Task<bool> ExistUser(LoginCommand command, CancellationToken token)
            {
                bool e = await _context.Users.AnyAsync(x => x.Name != command.Name
                                                      && x.Password != command.Password);
                return e;
            }
        }
        public class LoginCommandHandler : IRequestHandler<LoginCommand, UserDTO>
        {
            private readonly ApplicationContext _context;
            private readonly IValidator<LoginCommand> _validator;
            private readonly IMapper _mapper;
            public LoginCommandHandler(ApplicationContext context, IValidator<LoginCommand> validator, IMapper mapper)
            {
                _context = context;
                _validator = validator;
                _mapper = mapper;
            }
            public async Task<UserDTO> Handle(LoginCommand request, CancellationToken cancellationToken)
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
                        var user = await _context.Users.FirstOrDefaultAsync(x => x.Name == request.Name
                                                                                             && x.Password == request.Password);
                        if (user == null)
                        {
                            throw new ValidationException("El usuario y/o contraseña incorrectos");
                        }
                        return _mapper.Map<UserDTO>(user);
                    }
                }
                catch (Exception e)
                {
                    throw new ValidationException($"Error en el login: {e.Message}");
                }
            }
        }
    }
}
