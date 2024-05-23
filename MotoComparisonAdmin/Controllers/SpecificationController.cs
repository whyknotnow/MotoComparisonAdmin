using Microsoft.AspNetCore.Mvc;

using MotoComparisonAdmin.Services;
using MotoComparisonAdmin.ViewModels;

using System.Threading.Tasks;

[Route("[controller]/[action]")]
public class SpecificationController : Controller
{
    private readonly SpecificationService _service;

    public SpecificationController(SpecificationService service)
    {
        _service = service;
    }

    public async Task<IActionResult> Index(string searchTerm = "", int pageIndex = 1, int pageSize = 10)
    {
        var specifications = await _service.GetAllGroupedAsync(searchTerm, pageIndex, pageSize);
        ViewData["SearchTerm"] = searchTerm;
        return View(specifications);
    }

    public async Task<IActionResult> Edit(int id)
    {
        var specification = await _service.GetByIdAsync(id);
        if (specification == null)
        {
            return NotFound();
        }
        return View(specification);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(int id, SpecificationViewModel specification)
    {
        if (id != specification.Id)
        {
            return BadRequest();
        }
        if (ModelState.IsValid)
        {
            await _service.UpdateAsync(specification);
            return RedirectToAction(nameof(Index));
        }
        return View(specification);
    }

    public async Task<IActionResult> Delete(int id)
    {
        var specification = await _service.GetByIdAsync(id);
        if (specification == null)
        {
            return NotFound();
        }
        return View(specification);
    }

    [HttpPost, ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Details(int id)
    {
        var specification = await _service.GetByIdAsync(id);
        if (specification == null)
        {
            return NotFound();
        }
        return View(specification);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(SpecificationViewModel specification)
    {
        if (ModelState.IsValid)
        {
            await _service.AddAsync(specification);
            return RedirectToAction(nameof(Index));
        }
        return View(specification);
    }
}
