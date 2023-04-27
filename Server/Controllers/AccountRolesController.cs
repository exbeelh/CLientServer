using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repository.Contracts;
using Server.Repository.Data;

namespace Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class AccountRolesController : GeneralController<IAccountRoleRepository, AccountRole, int>
    {
        public AccountRolesController(IAccountRoleRepository repository) : base(repository)
        {
        }
    }
}
