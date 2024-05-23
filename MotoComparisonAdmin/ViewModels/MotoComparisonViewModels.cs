namespace MotoComparisonAdmin.ViewModels
{
    public class ManufacturerViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class ModelViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int ManufacturerId { get; set; }
        public string ManufacturerName { get; set; }
    }

    public class SpecificationViewModel
    {
        public int Id { get; set; }
        public string Details { get; set; }
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public string ManufacturerName { get; set; }
        public string Key { get; internal set; }
        public string Value { get; internal set; }
    }
}


