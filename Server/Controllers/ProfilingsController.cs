using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Server.Models;
using Server.Repository.Contracts;
using Server.ViewModels;
using System.Net;

namespace Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class ProfilingsController : GeneralController<IProfilingReopsitory, Profiling, string>
    {
        public ProfilingsController(IProfilingReopsitory repository) : base(repository)
        {
        }

        [HttpGet("WorkPeriod")]
        public async Task<ActionResult<IEnumerable<EmployeeProfileVM>>> WorkPeriod()
        {
            try
            {
                var data = await _repository.GetWorkPeriodeAsync();
                return Ok(data);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        code = StatusCodes.Status500InternalServerError,
                        status = HttpStatusCode.InternalServerError.ToString(),
                        Errors = new
                        {
                            message = "Internal server error."
                        }
                    });
            }
        }

        [HttpGet("TotalByMajor")]
        public async Task<ActionResult<IEnumerable<EmployeeProfileVM>>> TotalByMajor()
        {
            try
            {
                var entity = await _repository.GetEducationStatisticAsync();
                return Ok(new
                {
                    code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    data = entity
                });
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        code = StatusCodes.Status500InternalServerError,
                        status = HttpStatusCode.InternalServerError.ToString(),
                        Errors = new
                        {
                            message = "Internal server error."
                        }
                    });
            }
        }

        [HttpGet("AvgGPA/{year:int}")]
        public async Task<ActionResult<IEnumerable<EmployeeProfileVM>>> AvgByYear(int year)
        {
            try
            {
                var entity = await _repository.GetHighThanAverageGpaAsync(year);
                return Ok(new
                {
                    code = StatusCodes.Status200OK,
                    status = HttpStatusCode.OK.ToString(),
                    data = entity
                });

            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new
                    {
                        code = StatusCodes.Status500InternalServerError,
                        status = HttpStatusCode.InternalServerError.ToString(),
                        Errors = new
                        {
                            message = "Internal server error."
                        }
                    });
            }
        }
    }
}
