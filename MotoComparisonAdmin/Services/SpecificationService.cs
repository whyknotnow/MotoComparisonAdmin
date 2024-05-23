using Microsoft.EntityFrameworkCore;

using MotoComparisonAdmin.Contexts;
using MotoComparisonAdmin.ViewModels;

public class SpecificationService
{
    private readonly MotorcycleContext _context;

    public SpecificationService(MotorcycleContext context)
    {
        _context = context;
    }

    public async Task<PaginatedList<SpecificationGroupedViewModel>> GetAllGroupedAsync(string searchTerm, int pageIndex, int pageSize)
    {
        var query = _context.Models
            .Include(m => m.Manufacturer)
            .Include(m => m.Specifications)
            .AsQueryable();

        if (!string.IsNullOrEmpty(searchTerm))
        {
            query = query.Where(m => m.Name.Contains(searchTerm));
        }

        var count = await query.CountAsync();
        var items = await query
            .Skip((pageIndex - 1) * pageSize)
            .Take(pageSize)
            .Select(m => new SpecificationGroupedViewModel
            {
                ModelId = m.Id,
                ModelName = m.Name,
                ManufacturerName = m.Manufacturer.Name,
                Specifications = m.Specifications.Select(s => new SpecificationViewModel
                {
                    Id = s.Id,
                    ModelId = s.ModelId,
                    ModelName = s.Model.Name,
                    ManufacturerName = s.Model.Manufacturer.Name,
                    Key = s.Key,
                    Value = s.Value
                }).ToList()
            })
            .ToListAsync();

        return new PaginatedList<SpecificationGroupedViewModel>(items, count, pageIndex, pageSize);
    }

    public async Task<SpecificationViewModel> GetByIdAsync(int id)
    {
        var specification = await _context.Specifications
            .Include(s => s.Model)
            .ThenInclude(m => m.Manufacturer)
            .FirstOrDefaultAsync(s => s.Id == id);

        if (specification == null)
        {
            return null;
        }

        return new SpecificationViewModel
        {
            Id = specification.Id,
            ModelId = specification.ModelId,
            ModelName = specification.Model.Name,
            ManufacturerName = specification.Model.Manufacturer.Name,
            Key = specification.Key,
            Value = specification.Value
        };
    }

    public async Task UpdateAsync(SpecificationViewModel specificationViewModel)
    {
        var specification = await _context.Specifications.FindAsync(specificationViewModel.Id);

        if (specification != null)
        {
            specification.Key = specificationViewModel.Key;
            specification.Value = specificationViewModel.Value;
            _context.Specifications.Update(specification);
            await _context.SaveChangesAsync();
        }
    }

    public async Task DeleteAsync(int id)
    {
        var specification = await _context.Specifications.FindAsync(id);
        if (specification != null)
        {
            _context.Specifications.Remove(specification);
            await _context.SaveChangesAsync();
        }
    }

    public async Task AddAsync(SpecificationViewModel specificationViewModel)
    {
        var specification = new SpecificationContextModel
        {
            ModelId = specificationViewModel.ModelId,
            Key = specificationViewModel.Key,
            Value = specificationViewModel.Value
        };

        _context.Specifications.Add(specification);
        await _context.SaveChangesAsync();
    }
}
