
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MotoComparisonAdmin.Contexts;
using MotoComparisonAdmin.ViewModels;


namespace MotoComparisonAdmin.Services
{
    using MotoComparisonAdmin.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ModelService
    {
        private readonly MotorcycleContext _context;

        public ModelService(MotorcycleContext context)
        {
            _context = context;
        }

        public async Task<List<ModelViewModel>> GetAllAsync()
        {
            return await _context.Models
                .Include(m => m.Manufacturer)
                .Select(m => new ModelViewModel
                {
                    Id = m.Id,
                    Name = m.Name,
                    ManufacturerId = m.ManufacturerId,
                    ManufacturerName = m.Manufacturer.Name
                }).ToListAsync();
        }

        public async Task<ModelViewModel> GetByIdAsync(int id)
        {
            var model = await _context.Models
                .Include(m => m.Manufacturer)
                .FirstOrDefaultAsync(m => m.Id == id);

            if (model == null)
                return null;

            return new ModelViewModel
            {
                Id = model.Id,
                Name = model.Name,
                ManufacturerId = model.ManufacturerId,
                ManufacturerName = model.Manufacturer.Name
            };
        }

        public async Task AddAsync(ModelViewModel modelViewModel)
        {
            var model = new ModelContextModel
            {
                Name = modelViewModel.Name,
                ManufacturerId = modelViewModel.ManufacturerId
            };

            _context.Models.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ModelViewModel modelViewModel)
        {
            var model = await _context.Models.FindAsync(modelViewModel.Id);

            if (model != null)
            {
                model.Name = modelViewModel.Name;
                model.ManufacturerId = modelViewModel.ManufacturerId;

                _context.Models.Update(model);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var model = await _context.Models.FindAsync(id);
            if (model != null)
            {
                _context.Models.Remove(model);
                await _context.SaveChangesAsync();
            }
        }
    }



}
