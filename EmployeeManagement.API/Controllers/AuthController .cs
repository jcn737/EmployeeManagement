using EmployeeManagement.Application.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace EmployeeManagement.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("generate-token")]
        public async Task<IActionResult> GenerateToken([FromBody] string username)
        {
            var token = await _mediator.Send(new GenerateTokenRequest { Username = username });
            return Ok(new { Token = token });
        }

        [HttpPost("validate-token")]
        public async Task<IActionResult> ValidateToken([FromBody] string token)
        {
            var principal = await _mediator.Send(new ValidateTokenRequest { Token = token });
            if (principal == null)
                return Unauthorized("Invalid token.");

            return Ok(new { Message = "Token is valid." });
        }
    }
}
