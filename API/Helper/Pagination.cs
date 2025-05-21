namespace API.Helper
{
    public class Pagination<T> where T : class
    {
        public Pagination(int pageNumber, int pageSize, int totalCount, IEnumerable<T> data)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            TotalCount = totalCount;
            Data = data;
        }

        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 3;
        public int TotalCount { get; set; }
        public IEnumerable<T> Data { get; set; }

    }
}
