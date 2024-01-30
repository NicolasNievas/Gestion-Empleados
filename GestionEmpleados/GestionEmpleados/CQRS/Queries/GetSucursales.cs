using AutoMapper;
using GestionEmpleados.DTO;
using GestionEmpleados.Migrations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleados.CQRS.Queries
{
    public class GetSucursales
    {
        public class GetSucursalQuery : IRequest<List<SucursalDTO>>
        {
        }
        public class GetSucursalesQueryHandler : IRequestHandler<GetSucursalQuery, List<SucursalDTO>>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetSucursalesQueryHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<SucursalDTO>> Handle(GetSucursalQuery request, CancellationToken cancellationToken)
            {
                var sucursales = await _context.Sucursals.ToListAsync();
                return _mapper.Map<List<SucursalDTO>>(sucursales);
            }
        }
    }
}
