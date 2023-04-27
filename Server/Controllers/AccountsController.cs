using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Handlers.Interface;
using Server.Models;
using Server.Repository.Contracts;
using Server.ViewModels;
using System.Net;
using System.Security.Claims;

namespace Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AccountsController : GeneralController<IAccountRepository, Account, string>
    {
        private readonly ITokenService _tokenService;
        private readonly IAccountRoleRepository _accountRoleRepository;
        private readonly IEmployeeRepository _employeeRepository;

        public AccountsController(
            IAccountRepository repository,
            ITokenService tokenService,
            IAccountRoleRepository accountRoleRepository,
            IEmployeeRepository employeeRepository
            ) : base(repository)
        {
            _tokenService = tokenService;
            _accountRoleRepository = accountRoleRepository;
            _employeeRepository = employeeRepository;
        }

        [HttpPost("Register")]
        [AllowAnonymous]
        public async Task<IActionResult> Register([FromBody] RegisterVM registerVM)
        {
            try
            {
                await _repository.Register(registerVM);
                return Ok(new
                {
                    code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    message = "Account has been created."
                });
            }
            catch
            {
                return BadRequest(new
                {
                    code = StatusCodes.Status400BadRequest,
                    status = HttpStatusCode.BadRequest.ToString(),
                    message = "Account hasn't be created."
                });
            }
        }

        [HttpPost("Login")]
        [AllowAnonymous]
        public async Task<IActionResult> Login([FromBody] LoginVM loginVM)
        {
            try
            {
                var isLogin = await _repository.IsLogin(loginVM);
                if (!isLogin)
                {
                    return NotFound(new
                    {
                        code = StatusCodes.Status404NotFound,
                        status = HttpStatusCode.NotFound.ToString(),
                        message = "Email or password not match"
                    });
                }

                var userData = await _employeeRepository.GetUserDataByEmailAsync(loginVM.Email);
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Email, userData.Email),
                    new Claim(ClaimTypes.Name, userData.Email),
                    new Claim(ClaimTypes.NameIdentifier, userData.FullName)
                };

                var getRoles = await _accountRoleRepository.GetRolesByNikAsync(userData.Nik);

                foreach (var item in getRoles)
                {
                    claims.Add(new Claim(ClaimTypes.Role, item));
                }

                var accessToken = _tokenService.GenerateToken(claims);

                return Ok(accessToken);
                
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new {
                        code = StatusCodes.Status500InternalServerError,
                        status = HttpStatusCode.InternalServerError.ToString(),
                        Errors = new
                        {
                            message = "Invalid salt version"
                        }
                });
            }
        }

    }

}
