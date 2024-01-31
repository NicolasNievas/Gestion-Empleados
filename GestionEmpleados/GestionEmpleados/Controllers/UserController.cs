using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GestionEmpleados.CQRS.Commands.NewUser;

namespace GestionEmpleados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;
        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost]
        [Route("Sing Up")]
        public async Task<ActionResult> SingUp(NewUserCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(SingUp), result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
