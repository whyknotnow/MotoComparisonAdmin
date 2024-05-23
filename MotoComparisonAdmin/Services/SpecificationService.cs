
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MotoComparisonAdmin.Contexts;

namespace MotoComparisonAdmin.Services
{

    using MotoComparisonAdmin.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;

    public class SpecificationService
    {
        private readonly MotorcycleContext _context;

        public SpecificationService(MotorcycleContext context)
        {
            _context = context;
        }

        public async Task<List<SpecificationViewModel>> GetAllAsync()
        {
            return await _context.Specifications
                .Include(s => s.Model)
                .ThenInclude(m => m.Manufacturer)
                .Select(s => new SpecificationViewModel
                {
                    Id = s.Id,
                    ModelId = s.ModelId,
                    ModelName = s.Model.Name,
                    ManufacturerName = s.Model.Manufacturer.Name,
                    Key= s.Key,
                    Value = s.Value
                }).ToListAsync();
        }

        public async Task<SpecificationViewModel> GetByIdAsync(int id)
        {
            var specification = await _context.Specifications
                .Include(s => s.Model)
                .ThenInclude(m => m.Manufacturer)
                .FirstOrDefaultAsync(s => s.Id == id);

            if (specification == null)
                return null;

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

        public async Task UpdateAsync(SpecificationViewModel specificationViewModel)
        {
            var specification = await _context.Specifications.FindAsync(specificationViewModel.Id);

            if (specification != null)
            {
                specification.Key = specificationViewModel.Key;
                specification.Value = specificationViewModel.Value;

                specification.ModelId = specificationViewModel.ModelId;

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


    }


}
