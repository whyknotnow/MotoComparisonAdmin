using Microsoft.AspNetCore.Mvc;

namespace MotoComparisonAdmin.Controllers
{
    using Microsoft.AspNetCore.Mvc;

    using MotoComparisonAdmin.Contexts;
    using MotoComparisonAdmin.Services;


    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class ModelController : ControllerBase
    {
        private readonly ModelService _service;

        public ModelController(ModelService service)
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
            var model = await _service.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return Ok(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] ModelContextModel model)
        {
            if (model == null)
            {
                return BadRequest();
            }
            await _service.AddAsync(model);
            return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] ModelContextModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            await _service.UpdateAsync(model);
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
