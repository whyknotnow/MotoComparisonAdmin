namespace MotoComparisonAdmin.Models
{
        public class ManufacturerViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            // other properties
        }

        public class ModelViewModel
        {
            public int Id { get; set; }
            public string Name { get; set; }
            public int ManufacturerId { get; set; }
            public ManufacturerViewModel Manufacturer { get; set; }
            // other properties
        }

        public class SpecificationViewModel
        {
            public int Id { get; set; }
            public string Details { get; set; }
            public int ModelId { get; set; }
            public ModelViewModel Model { get; set; }
            // other properties
        }
    }


