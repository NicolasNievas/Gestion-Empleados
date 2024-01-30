using AutoMapper;
using GestionEmpleados.DTO;
using GestionEmpleados.Migrations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleados.CQRS.Queries
{
    public class GetCharges
    {
        public class GetChargesQuery : IRequest<List<ChargeDTO>>
        {
        }
        public class GetChargesQueryHandler : IRequestHandler<GetChargesQuery, List<ChargeDTO>>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetChargesQueryHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<ChargeDTO>> Handle(GetChargesQuery request, CancellationToken cancellationToken)
            {
                var charges = await _context.Charges.ToListAsync();
                return _mapper.Map<List<ChargeDTO>>(charges);
            }
        }
    }
}
