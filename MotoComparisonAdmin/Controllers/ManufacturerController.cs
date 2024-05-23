using Microsoft.AspNetCore.Mvc;

using MotoComparisonAdmin.Services;
using MotoComparisonAdmin.ViewModels;

using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ManufacturerController : Controller
{
    private readonly ManufacturerService _service;

    public ManufacturerController(ManufacturerService service)
    {
        _service = service;
    }

    // API Actions
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var manufacturer = await _service.GetByIdAsync(id);
        if (manufacturer == null)
        {
            return NotFound();
        }
        return Ok(manufacturer);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ManufacturerViewModel manufacturer)
    {
        if (manufacturer == null)
        {
            return BadRequest();
        }
        await _service.AddAsync(manufacturer);
        return CreatedAtAction(nameof(GetById), new { id = manufacturer.Id }, manufacturer);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ManufacturerViewModel manufacturer)
    {
        if (id != manufacturer.Id)
        {
            return BadRequest();
        }
        await _service.UpdateAsync(manufacturer);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteApi(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }

    // MVC Actions
    [HttpGet("/Manufacturer")]
    public async Task<IActionResult> Index(string searchTerm = "", int pageIndex = 1, int pageSize = 10)
    {
        var manufacturers = await _service.GetAllAsync(searchTerm, pageIndex, pageSize);
        ViewData["SearchTerm"] = searchTerm;
        return View(manufacturers);
    }

    [HttpGet("/Manufacturer/Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("/Manufacturer/Create")]
    public async Task<IActionResult> Create(ManufacturerViewModel manufacturer)
    {
        if (ModelState.IsValid)
        {
            await _service.AddAsync(manufacturer);
            return RedirectToAction(nameof(Index));
        }
        return View(manufacturer);
    }

    [HttpGet("/Manufacturer/Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var manufacturer = await _service.GetByIdAsync(id);
        if (manufacturer == null)
        {
            return NotFound();
        }
        return View(manufacturer);
    }

    [HttpPost("/Manufacturer/Edit/{id}")]
    public async Task<IActionResult> Edit(int id, ManufacturerViewModel manufacturer)
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

    [HttpGet("/Manufacturer/Delete/{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var manufacturer = await _service.GetByIdAsync(id);
        if (manufacturer == null)
        {
            return NotFound();
        }
        return View(manufacturer);
    }

    [HttpPost("/Manufacturer/Delete/{id}"), ActionName("Delete")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("/Manufacturer/Details/{id}")]
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
