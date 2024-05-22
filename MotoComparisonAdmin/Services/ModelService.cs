
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using MotoComparisonAdmin.Contexts;


namespace MotoComparisonAdmin.Services
{

    public class ModelService
    {
        private readonly MotorcycleContext _context;

        public ModelService(MotorcycleContext context)
        {
            _context = context;
        }

        public async Task<List<ModelContextModel>> GetAllAsync()
        {
            return await _context.Models.ToListAsync();
        }

        public async Task<ModelContextModel> GetByIdAsync(int id)
        {
            return await _context.Models.FindAsync(id);
        }

        public async Task AddAsync(ModelContextModel model)
        {
            _context.Models.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateAsync(ModelContextModel model)
        {
            _context.Models.Update(model);
            await _context.SaveChangesAsync();
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
