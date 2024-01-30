using GestionEmpleados.DTO;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GestionEmpleados.CQRS.Commands.DeleteEmployee;
using static GestionEmpleados.CQRS.Commands.PostCharge;
using static GestionEmpleados.CQRS.Commands.PostCity;
using static GestionEmpleados.CQRS.Commands.PostEmployee;
using static GestionEmpleados.CQRS.Commands.PutEmployee;

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
        [HttpPost]
        [Route("PostEmployee")]
        public async Task<ActionResult> PostEmployee(PostEmployeeCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(PostEmployee), result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpPut]
        [Route("PutEmployee")]
        public async Task<ActionResult> PutEmployee(PutEmployeeCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return CreatedAtAction(nameof(PutEmployee), result);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        [HttpDelete]
        [Route("DeleteEmployee/{id}")]
        public async Task<ActionResult> DeleteEmployee(int id)
        {
            var cmd = new DeleteEmployeeCommand { Id = id };
            try
            {
                await _mediator.Send(cmd);
                return NoContent();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
        
    }
}
