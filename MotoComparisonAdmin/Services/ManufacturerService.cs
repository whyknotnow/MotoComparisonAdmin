using Microsoft.EntityFrameworkCore;

using MotoComparisonAdmin.Contexts;

namespace MotoComparisonAdmin.Services
{
    using MotoComparisonAdmin.Contexts;
    using MotoComparisonAdmin.ViewModels;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    public class ManufacturerService
    {
        private readonly MotorcycleContext _context;

        public ManufacturerService(MotorcycleContext context)
        {
            _context = context;
        }

        public async Task<List<ManufacturerViewModel>> GetAllAsync()
        {
            return await _context.Manufacturers
                .Select(m => new ManufacturerViewModel
                {
                    Id = m.Id,
                    Name = m.Name
                }).ToListAsync();
        }

        public async Task<ManufacturerViewModel> GetByIdAsync(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer == null)
                return null;

            return new ManufacturerViewModel
            {
                Id = manufacturer.Id,
                Name = manufacturer.Name
            };
        }

        public async Task AddAsync(ManufacturerViewModel manufacturerViewModel)
        {
            var manufacturer = new ManufacturerContextModel
            {
                Name = manufacturerViewModel.Name
            };

            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ManufacturerViewModel manufacturerViewModel)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(manufacturerViewModel.Id);
            if (manufacturer != null)
            {
                manufacturer.Name = manufacturerViewModel.Name;
                _context.Manufacturers.Update(manufacturer);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteAsync(int id)
        {
            var manufacturer = await _context.Manufacturers.FindAsync(id);
            if (manufacturer != null)
            {
                _context.Manufacturers.Remove(manufacturer);
                await _context.SaveChangesAsync();
            }
        }
    }


}
