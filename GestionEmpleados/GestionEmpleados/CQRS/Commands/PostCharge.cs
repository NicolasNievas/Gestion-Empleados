using AutoMapper;
using FluentValidation;
using GestionEmpleados.DTO;
using GestionEmpleados.Migrations;
using GestionEmpleados.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleados.CQRS.Commands
{
    public class PostCharge
    {
        public class PostChargeCommand : IRequest<ChargeDTO>
        {
            public string Name { get; set; } = null!;
        }
        public class PostChargeCommandValidator : AbstractValidator<PostChargeCommand>
        {
            private readonly ApplicationContext _context;
            public PostChargeCommandValidator(ApplicationContext context)
            {
                RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("El cargo no puede estar vacío ni ser nulo");
                RuleFor(x => x.Name).MustAsync(ExistCharge).WithMessage("El cargo ya existe");
                _context = context;
            }

            private async Task<bool> ExistCharge(string cmd, CancellationToken token)
            {
                bool e = await _context.Charges.AnyAsync(x => x.Name == cmd);
                return !e;
            }
        }
        public class PostChargeCommandHandler : IRequestHandler<PostChargeCommand, ChargeDTO>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            private readonly IValidator<PostChargeCommand> _validator;
            public PostChargeCommandHandler(ApplicationContext context, IMapper mapper, IValidator<PostChargeCommand> validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }
            public async Task<ChargeDTO> Handle(PostChargeCommand request, CancellationToken cancellationToken)
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
                        var charge = new Charge
                        {
                            Name = request.Name
                        };
                        var charges = _mapper.Map<Charge>(request);
                        await _context.Charges.AddAsync(charges);
                        await _context.SaveChangesAsync();
                        var chargeDTO = _mapper.Map<ChargeDTO>(charges);
                        return chargeDTO;
                    }
                }
                catch (Exception)
                {

                    throw;
                }
            }
        }
    }
}
