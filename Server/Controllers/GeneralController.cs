using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Server.Repository.Contracts;
using System.Net;

namespace Server.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class GeneralController<TIRepository, TEntity, TKey> : ControllerBase 
        where TEntity : class
        where TIRepository : IGeneralRepository<TEntity, TKey>
    {
        protected TIRepository _repository;

        public GeneralController(TIRepository repository)
        {
            _repository = repository;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAsync()
        {
            var entity = await _repository.GetAllAsync();
            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(),
                data = entity
            });
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> GetAsync(TKey id)
        {
            var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
            {
                return NotFound(new
                {
                    code = StatusCodes.Status404NotFound,
                    status = HttpStatusCode.NotFound.ToString(),
                    data = new
                    {
                        message = "Data Not Found!"
                    }
                });
            }

            return Ok(new
            {
                code = StatusCodes.Status200OK,
                status = HttpStatusCode.OK.ToString(), 
                data = entity
            });
        }

        [HttpPost]
        public virtual async Task<IActionResult> Insert([FromBody] TEntity employee)
        {
            try
            {
                await _repository.InsertAsync(employee);
                return CreatedAtAction(nameof(GetAsync), new { id = GetAsync() }, employee);
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
        public virtual async Task<IActionResult> Update([FromBody] TEntity employee)
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
        public virtual async Task<IActionResult> Delete(TKey id)
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
