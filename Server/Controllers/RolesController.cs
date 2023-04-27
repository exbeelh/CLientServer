using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repository.Contracts;

namespace Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class RolesController : GeneralController<IRoleRepository, Role, int>
    {
        public RolesController(IRoleRepository repository) : base(repository)
        {
        }
    }
}
