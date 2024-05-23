namespace MotoComparisonAdmin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;

    using MotoComparisonAdmin.Contexts;
    using MotoComparisonAdmin.Services;
    using MotoComparisonAdmin.ViewModels;

    using System.Threading.Tasks;

    public class SpecificationController : Controller
    {
        private readonly SpecificationService _service;
        private readonly ManufacturerService _manufacturerService;
        private readonly ModelService _modelService;

        public SpecificationController(SpecificationService service, ManufacturerService manufacturerService, ModelService modelService)
        {
            _service = service;
            _manufacturerService = manufacturerService;
            _modelService = modelService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.ManufacturerId = new SelectList(await _manufacturerService.GetAllAsync(), "Id", "Name");
            ViewBag.ModelId = new SelectList(await _modelService.GetAllAsync(), "Id", "Name");

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(SpecificationViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(model);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ManufacturerId = new SelectList(await _manufacturerService.GetAllAsync(), "Id", "Name");
            ViewBag.ModelId = new SelectList(await _modelService.GetAllAsync(), "Id", "Name");
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _service.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.ModelId = new SelectList(await _modelService.GetAllAsync(), "Id", "Name", model.ModelId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, SpecificationViewModel model)
        {
            if (id != model.Id)
            {
                return BadRequest();
            }
            if (ModelState.IsValid)
            {
                await _service.UpdateAsync(model);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ModelId = new SelectList(await _manufacturerService.GetAllAsync(), "Id", "Name", model.ModelId);
            return View(model);
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
        public async Task<IActionResult> Add([FromBody] SpecificationViewModel spec)
        {
            if (spec == null)
            {
                return BadRequest();
            }
            await _service.AddAsync(spec);
            return CreatedAtAction(nameof(GetById), new { id = spec.Id }, spec);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromBody] SpecificationViewModel spec)
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


        public async Task<IActionResult> Details(int id)
        {
            var model = await _service.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }
    }

}
