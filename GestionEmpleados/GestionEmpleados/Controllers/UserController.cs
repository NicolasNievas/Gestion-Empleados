using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using static GestionEmpleados.CQRS.Commands.Login;
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
        [Route("Sing_Up")]
        public async Task<ActionResult> SingUp(NewUserCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { Message = "Usuario creado exitosamente" });
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = "Error al crear el usuario", Error = e.Message });
            }
        }
        [HttpPost]
        [Route("Login")]
        public async Task<ActionResult> Login(LoginCommand command)
        {
            try
            {
                var result = await _mediator.Send(command);
                return Ok(new { Message = "Inicio de sesión exitoso"});
            }
            catch (Exception e)
            {
                return BadRequest(new { Message = "Error en el login", Error = e.Message });
            }
        }
    }
}
