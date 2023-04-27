using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Models;
using Server.Repository.Contracts;
using System.Net;

namespace Server.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Authorize]
    public class EducationsController : GeneralController<IEducationRepository, Education, int>
    {
        public EducationsController(IEducationRepository repository) : base(repository)
        {
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Insert([FromBody] Education education)
        {
            try
            {
                await _repository.InsertAsync(education);
                return CreatedAtAction(nameof(GetAsync), new { id = GetAsync() }, education);
            }
            catch
            {
                return BadRequest(new
                {
                    code = StatusCodes.Status400BadRequest,
                    status = HttpStatusCode.BadRequest.ToString(),
                    message = "Input Error"
                });
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Update([FromBody] Education employee)
        {
            try
            {
                await _repository.UpdateAsync(employee);
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound(new
                {
                    code = StatusCodes.Status404NotFound,
                    status = HttpStatusCode.NotFound.ToString(),
                    message = "Data Not Found."
                });
                throw;
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public override async Task<IActionResult> Delete(int id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound(new
                {
                    code = StatusCodes.Status404NotFound,
                    status = HttpStatusCode.NotFound.ToString(),
                    message = "Data Not Found."
                });
            }

            await _repository.DeleteAsync(id);
            return NoContent();
        }

    }
}
