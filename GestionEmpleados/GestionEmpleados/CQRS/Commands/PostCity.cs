using AutoMapper;
using FluentValidation;
using GestionEmpleados.DTO;
using GestionEmpleados.Migrations;
using GestionEmpleados.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleados.CQRS.Commands
{
    public class PostCity
    {
        public class PostCityCommand : IRequest<CityDTO>
        {
            public string Name { get; set; } = null!;
        }
        public class PostCityCommandValidator : AbstractValidator<PostCityCommand>
        {
            private readonly ApplicationContext _context;
            public PostCityCommandValidator(ApplicationContext context)
            {
                RuleFor(x => x.Name).NotEmpty().NotNull().WithMessage("La ciudad no puede estar vacío ni ser nulo");
                RuleFor(x => x.Name).MustAsync(ExistCity).WithMessage("La ciudad ya existe");
                _context = context;
            }

            private async Task<bool> ExistCity(string cmd, CancellationToken token)
            {
                bool e = await _context.Cities.AnyAsync(x => x.Name == cmd);
                return !e;
            }
        }
        public class PostCityCommandHandler : IRequestHandler<PostCityCommand, CityDTO>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            private readonly IValidator<PostCityCommand> _validator;
            public PostCityCommandHandler(ApplicationContext context, IMapper mapper, IValidator<PostCityCommand> validator)
            {
                _context = context;
                _mapper = mapper;
                _validator = validator;
            }
            public async Task<CityDTO> Handle(PostCityCommand request, CancellationToken cancellationToken)
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
                        var cities = _mapper.Map<City>(request);
                        await _context.Cities.AddAsync(cities);
                        await _context.SaveChangesAsync();
                        var cityDTO = _mapper.Map<CityDTO>(cities);
                        return cityDTO;
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
