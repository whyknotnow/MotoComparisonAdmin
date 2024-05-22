namespace MotoComparisonAdmin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using MotoComparisonAdmin.Contexts;
    using MotoComparisonAdmin.Services;

    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class SpecController : ControllerBase
    {
        private readonly SpecificationService _service;

        public SpecController(SpecificationService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _service.GetAllAsync());
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var spec = await _service.GetByIdAsync(id);
            if (spec == null)
            {
                return NotFound();
            }
            return Ok(spec);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] SpecificationContextModel spec)
        {
            if (spec == null)
            {
                return BadRequest();
            }
            await _service.AddAsync(spec);
            return CreatedAtAction(nameof(GetById), new { id = spec.Id }, spec);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SpecificationContextModel spec)
        {
            if (id != spec.Id)
            {
                return BadRequest();
            }
            await _service.UpdateAsync(spec);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _service.DeleteAsync(id);
            return NoContent();
        }
    }

}
