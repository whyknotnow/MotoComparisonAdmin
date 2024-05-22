
using System.Collections.Generic;

using Microsoft.EntityFrameworkCore;

namespace MotoComparisonAdmin.Contexts
{

    public class MotorcycleContext : DbContext
    {
        public MotorcycleContext(DbContextOptions<MotorcycleContext> options) : base(options) { }

        public DbSet<ManufacturerContextModel> Manufacturers { get; set; }
        public DbSet<ModelContextModel> Models { get; set; }
        public DbSet<SpecificationContextModel> Specifications { get; set; }
    }

    public class ManufacturerContextModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public List<ModelContextModel> Models { get; set; }
    }

    public class ModelContextModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public int ManufacturerId { get; set; }
        public ManufacturerContextModel Manufacturer { get; set; }
        public List<SpecificationContextModel> Specifications { get; set; }
    }

    public class SpecificationContextModel
    {
        public int Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
        public int ModelId { get; set; }
        public ModelContextModel Model { get; set; }
    }

}
