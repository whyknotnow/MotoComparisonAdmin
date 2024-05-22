
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MotoComparisonAdmin.Contexts;

namespace MotoComparisonAdmin.Services
{

    public class SpecificationService
    {
        private readonly MotorcycleContext _context;

        public SpecificationService(MotorcycleContext context)
        {
            _context = context;
        }

        public async Task<List<SpecificationContextModel>> GetAllAsync()
        {
            return await _context.Specifications.ToListAsync();
        }

        public async Task<SpecificationContextModel> GetByIdAsync(int id)
        {
            return await _context.Specifications.FindAsync(id);
        }

        public async Task AddAsync(SpecificationContextModel spec)
        {
            _context.Specifications.Add(spec);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(SpecificationContextModel spec)
        {
            _context.Specifications.Update(spec);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var spec = await _context.Specifications.FindAsync(id);
            if (spec != null)
            {
                _context.Specifications.Remove(spec);
                await _context.SaveChangesAsync();
            }
        }
    }

}
