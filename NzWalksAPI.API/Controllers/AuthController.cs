using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using NzWalksAPI.Domain.DTO;
using NzWalksAPI.Repositories.Repositories;

namespace NzWalksAPI.Controllers
{
    [ApiController]
    [Route("/api/[controller]")]
    public class AuthController(
        UserManager<IdentityUser> userManager,
        ITokenRepository tokenRepository
        ) : ControllerBase
    {
        private readonly UserManager<IdentityUser> userManager = userManager;
        private readonly ITokenRepository tokenRepository = tokenRepository;

        [HttpPost("register")]
        public async Task<IActionResult> Register(RegisterRequestDto registerRequestDto)
        {
            var identityUser = new IdentityUser
            {
                UserName = registerRequestDto.Username,
                Email = registerRequestDto.Username,
            };

            var identityResult = await userManager.CreateAsync(identityUser, registerRequestDto.Password);

            if (identityResult.Succeeded)

            {
                if (registerRequestDto.Roles != null && registerRequestDto.Roles.Any())
                {
                    identityResult = await userManager.AddToRolesAsync(identityUser, registerRequestDto.Roles);

                    if (identityResult.Succeeded)
                        return Created("user", registerRequestDto.Username);
                }
            };

            return BadRequest("Something went wrong.");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDto loginRequestDto)
        {
            var identityUser = await userManager.FindByEmailAsync(loginRequestDto.Username);

            if (identityUser != null)
            {
                var isMatch = await userManager.CheckPasswordAsync(identityUser, loginRequestDto.Password);

                if (isMatch)
                {
                    var roles = await userManager.GetRolesAsync(identityUser);
                    if (roles != null)
                    {
                        var jwtToken = tokenRepository.CreateJwtToken(identityUser, roles.ToList());

                        var reponse = new LoginResponseDto
                        {
                            JwtToken = jwtToken,
                            Username = identityUser.Email,
                        };

                        return Ok(reponse);
                    }
                }
            }

            return BadRequest("Invalid username or password");
        }
    }
}