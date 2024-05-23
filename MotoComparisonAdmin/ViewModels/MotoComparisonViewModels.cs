namespace MotoComparisonAdmin.ViewModels
{
    public class SpecificationGroupedViewModel
    {
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public string ManufacturerName { get; set; }
        public List<SpecificationViewModel> Specifications { get; set; }
    }

    public class PaginatedList<T>
    {
        public List<T> Items { get; private set; }
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            Items = items;
        }

        public bool HasPreviousPage
        {
            get { return PageIndex > 1; }
        }

        public bool HasNextPage
        {
            get { return PageIndex < TotalPages; }
        }
    }


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
        public int ModelId { get; set; }
        public string ModelName { get; set; }
        public string ManufacturerName { get; set; }
        public string Key { get; internal set; }
        public string Value { get; internal set; }
    }


}


