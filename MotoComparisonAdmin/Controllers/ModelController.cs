using Microsoft.AspNetCore.Mvc;

using MotoComparisonAdmin.Services;
using MotoComparisonAdmin.ViewModels;

using System.Threading.Tasks;

[ApiController]
[Route("api/[controller]")]
public class ModelController : Controller
{
    private readonly ModelService _service;

    public ModelController(ModelService service)
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
        var model = await _service.GetByIdAsync(id);
        if (model == null)
        {
            return NotFound();
        }
        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> Add([FromBody] ModelViewModel model)
    {
        if (model == null)
        {
            return BadRequest();
        }
        await _service.AddAsync(model);
        return CreatedAtAction(nameof(GetById), new { id = model.Id }, model);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, [FromBody] ModelViewModel model)
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

    // MVC Actions
    [HttpGet("/Model")]
    public async Task<IActionResult> Index(string searchTerm = "", int pageIndex = 1, int pageSize = 10)
    {
        var models = await _service.GetAllAsync(searchTerm, pageIndex, pageSize);
        ViewData["SearchTerm"] = searchTerm;
        return View(models);
    }


    [HttpGet("/Model/Create")]
    public IActionResult Create()
    {
        return View();
    }

    [HttpPost("/Model/Create")]
    public async Task<IActionResult> Create(ModelViewModel model)
    {
        if (ModelState.IsValid)
        {
            await _service.AddAsync(model);
            return RedirectToAction(nameof(Index));
        }
        return View(model);
    }

    [HttpGet("/Model/Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var model = await _service.GetByIdAsync(id);
        if (model == null)
        {
            return NotFound();
        }
        return View(model);
    }

    [HttpPost("/Model/Edit/{id}")]
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
        return View(model);
    }

    [HttpGet("/Model/Delete/{id}")]
    public async Task<IActionResult> DeleteMvc(int id)
    {
        var model = await _service.GetByIdAsync(id);
        if (model == null)
        {
            return NotFound();
        }
        return View(model);
    }

    [HttpPost("/Model/Delete/{id}"), ActionName("DeleteMvc")]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        await _service.DeleteAsync(id);
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("/Model/Details/{id}")]
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
