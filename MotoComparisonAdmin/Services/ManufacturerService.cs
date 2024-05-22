using Microsoft.EntityFrameworkCore;

using MotoComparisonAdmin.Contexts;

namespace MotoComparisonAdmin.Services
{
    public class ManufacturerService
    {
        private readonly MotorcycleContext _context;

        public ManufacturerService(MotorcycleContext context)
        {
            _context = context;
        }

        public async Task<List<ManufacturerContextModel>> GetAllAsync()
        {
            return await _context.Manufacturers.ToListAsync();
        }

        public async Task<ManufacturerContextModel> GetByIdAsync(int id)
        {
            return await _context.Manufacturers.FindAsync(id);
        }

        public async Task AddAsync(ManufacturerContextModel manufacturer)
        {
            _context.Manufacturers.Add(manufacturer);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ManufacturerContextModel manufacturer)
        {
            _context.Manufacturers.Update(manufacturer);
            await _context.SaveChangesAsync();
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
