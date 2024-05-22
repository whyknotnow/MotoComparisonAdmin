using Microsoft.AspNetCore.Mvc;

using MotoComparisonAdmin.Contexts;
using MotoComparisonAdmin.Services;

public class ManufacturerController : Controller
{
    private readonly ManufacturerService _service;

    public ManufacturerController(ManufacturerService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index()
    {
        return View(await _service.GetAllAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(ManufacturerContextModel manufacturer)
    {
        if (ModelState.IsValid)
        {
            await _service.AddAsync(manufacturer);
            return RedirectToAction(nameof(Index));
        }
        return View(manufacturer);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var manufacturer = await _service.GetByIdAsync(id);
        if (manufacturer == null)
        {
            return NotFound();
        }
        return View(manufacturer);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, ManufacturerContextModel manufacturer)
    {
        if (id != manufacturer.Id)
        {
            return BadRequest();
        }
        if (ModelState.IsValid)
        {
            await _service.UpdateAsync(manufacturer);
            return RedirectToAction(nameof(Index));
        }
        return View(manufacturer);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var manufacturer = await _service.GetByIdAsync(id);
        if (manufacturer == null)
        {
            return NotFound();
        }
        return View(manufacturer);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var manufacturer = await _service.GetByIdAsync(id);
        if (manufacturer == null)
        {
            return NotFound();
        }
        return View(manufacturer);
    }
}
