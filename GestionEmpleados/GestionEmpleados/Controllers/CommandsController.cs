using GestionEmpleados.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GestionEmpleados.CQRS.Commands.PostCharge;
using static GestionEmpleados.CQRS.Commands.PostCity;

namespace GestionEmpleados.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CommandsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CommandsController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpPost("PostCharge")]
        public async Task<ActionResult> PostCharge(PostChargeCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(PostCharge), result);
            }
            catch (Exception e )
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPost]
        [Route("PostCity")]
        public async Task<ActionResult> PostCity(PostCityCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(PostCity), result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
