using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repository.Contracts;

namespace Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize(Roles = "User")]
    public class UniversitiesController : GeneralController<IUniversityRepository, University, int>
    {
        public UniversitiesController(IUniversityRepository repository) : base(repository)
        {
        }
    }
}
