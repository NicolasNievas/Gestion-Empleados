using AutoMapper;
using GestionEmpleados.DTO;
using GestionEmpleados.Migrations;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace GestionEmpleados.CQRS.Queries
{
    public class Users
    {
        public class GetUserQuery : IRequest<List<UserDTO>>
        {
        }
        public class GetUsersQueryHandler : IRequestHandler<GetUserQuery, List<UserDTO>>
        {
            private readonly ApplicationContext _context;
            private readonly IMapper _mapper;
            public GetUsersQueryHandler(ApplicationContext context, IMapper mapper)
            {
                _context = context;
                _mapper = mapper;
            }
            public async Task<List<UserDTO>> Handle(GetUserQuery request, CancellationToken cancellationToken)
            {
                var users = await _context.Users.ToListAsync();
                return _mapper.Map<List<UserDTO>>(users);
            }
        }
    }
}
