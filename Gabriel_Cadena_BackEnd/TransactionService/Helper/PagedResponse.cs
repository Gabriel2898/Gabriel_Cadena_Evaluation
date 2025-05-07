namespace ProductService.Helpers
{
    public class PagedResponse<T>
    {
        public List<T> Data { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
        public int TotalRecords { get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalRecords / (double)PageSize);

        public PagedResponse()
        {
            Data = new List<T>();
        }
    }
}