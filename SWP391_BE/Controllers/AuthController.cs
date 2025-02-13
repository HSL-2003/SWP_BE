using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Mime;
using SWP391_BE.DTOs.Auth.DTO;
using SWP391_BE.DTOs.Auth.LoginUser;
using SWP391_BE.DTOs.Auth.LoginAdmin;
using SWP391_BE.DTOs.Auth.Register;

namespace SWP391_BE.Controllers
{
    [Route("api/auth")]
    [Authorize]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;
        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("login")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> login(
            [FromBody] LoginUser loginUser,CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(loginUser, cancellationToken);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("admin")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(AuthResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> loginadmin(
            [FromBody] LoginAdmin loginAdmin,CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(loginAdmin, cancellationToken);
            return Ok(result);
        }
        [AllowAnonymous]
        [HttpPost]
        [Route("register")]
        [Produces(MediaTypeNames.Application.Json)]
        [ProducesResponseType(typeof(Unit), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Register(
            [FromBody] Register register,CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(register, cancellationToken);
            return Created(string.Empty, result);
        }
    }
}
