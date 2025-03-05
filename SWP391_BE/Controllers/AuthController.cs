using Data.Response.AuthDTOs;
using Data.Response;
using Microsoft.AspNetCore.Mvc;
using Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Google;
using Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace SWP391_BE.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly ILogger<AuthController> _logger;

        public AuthController(IAuthService authService, ILogger<AuthController> logger)
        {
            _authService = authService ?? throw new ArgumentNullException(nameof(authService));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<ServiceResponse<string>>> Register(RegisterDTO model)
        {
            var response = await _authService.Register(model);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<ServiceResponse<string>>> Login(LoginDTO model)
        {
            var response = await _authService.Login(model);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpPost("login-google")]
        public async Task<IActionResult> LoginWithGoogle([FromBody] GoogleLoginDTO model)
        {
            var response = await _authService.LoginWithGoogle(model.IdToken);
            if (!response.Success)
            {
                return BadRequest(response);
            }
            return Ok(response);
        }

        [HttpGet("verify-email")]
        public async Task<ActionResult<ServiceResponse<string>>> VerifyEmail([FromQuery] string token)
        {
            var response = await _authService.VerifyEmail(token);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [AllowAnonymous]
        [HttpGet("login-facebook")]
        public IActionResult LoginFacebook()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action(nameof(FacebookCallback))
            };
            return Challenge(properties, "Facebook");
        }

        [HttpGet("facebook-callback")]
        public async Task<IActionResult> FacebookCallback()
        {
            var authenticateResult = await HttpContext.AuthenticateAsync("Facebook");

            if (!authenticateResult.Succeeded)
            {
                return BadRequest(new { message = "Facebook authentication failed." });
            }

            var claims = authenticateResult.Principal.Identities.FirstOrDefault()?.Claims;
            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);

            return Ok(new { message = "Đăng nhập Facebook thành công!", claims });
        }

        [HttpPost("forgot-password")]
        public async Task<ActionResult<ServiceResponse<string>>> ForgotPassword([FromBody] string email)
        {
            var response = await _authService.ForgotPassword(email);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }

        [HttpPost("reset-password")]
        public async Task<ActionResult<ServiceResponse<string>>> ResetPassword(ResetPasswordDTO model)
        {
            var response = await _authService.ResetPassword(model);
            if (!response.Success)
                return BadRequest(response);
            return Ok(response);
        }
    }

    public class GoogleLoginDTO
    {
        public string IdToken { get; set; }
    }
}