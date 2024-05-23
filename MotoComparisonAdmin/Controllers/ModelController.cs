using Microsoft.AspNetCore.Mvc;

namespace MotoComparisonAdmin.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using MotoComparisonAdmin.Services;
    using MotoComparisonAdmin.ViewModels;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Rendering;

    public class ModelController : Controller
    {
        private readonly ModelService _service;
        private readonly ManufacturerService _manufacturerService;

        public ModelController(ModelService service, ManufacturerService manufacturerService)
        {
            _service = service;
            _manufacturerService = manufacturerService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await _service.GetAllAsync());
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.ManufacturerId = new SelectList(await _manufacturerService.GetAllAsync(), "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(ModelViewModel model)
        {
            if (ModelState.IsValid)
            {
                await _service.AddAsync(model);
                return RedirectToAction(nameof(Index));
            }
            ViewBag.ManufacturerId = new SelectList(await _manufacturerService.GetAllAsync(), "Id", "Name", model.ManufacturerId);
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var model = await _service.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            ViewBag.ManufacturerId = new SelectList(await _manufacturerService.GetAllAsync(), "Id", "Name", model.ManufacturerId);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, ModelViewModel model)
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
            ViewBag.ManufacturerId = new SelectList(await _manufacturerService.GetAllAsync(), "Id", "Name", model.ManufacturerId);
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var model = await _service.GetByIdAsync(id);
            if (model == null)
            {
                return NotFound();
            }
            return View(model);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
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
