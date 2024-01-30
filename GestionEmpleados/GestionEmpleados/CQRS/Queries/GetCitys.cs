using AutoMapper;
using GestionEmpleados.DTO;
using GestionEmpleados.Migrations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleados.CQRS.Queries
{
    public class GetCitys
    {
        public class GetCityQuery : IRequest<List<CityDTO>>
        {
        }
        public class GetCitysQueryHandler : IRequestHandler<GetCityQuery, List<CityDTO>>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetCitysQueryHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<CityDTO>> Handle(GetCityQuery request, CancellationToken cancellationToken)
            {
                var citys = await _context.Cities.ToListAsync();
                return _mapper.Map<List<CityDTO>>(citys);
            }
        }
    }
}
