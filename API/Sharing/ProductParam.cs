namespace API.Sharing
{
    public class ProductParam
    {
        //int pageSize, int pageNumber, int? CategoryId, string? sort = null
        private int _pageSize = 3;
        public int PageSize
        {
            get => _pageSize;
            set => _pageSize = (value > MaxPageSize) ? MaxPageSize : value;
        }
        public int MaxPageSize { get; set; } = 6;

        public int PageNumber { get; set; } = 1;

        public int? CategoryId { get; set; } = null;
        public string? Sort { get; set; } = null;
        public string? Search { get; set; }

    }
}
