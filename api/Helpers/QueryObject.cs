namespace api.Helpers
{
    public class QueryObject
    {
        public string? Symbol { get; set; } = null;
        public string? CompanyName { get; set; } = null;

        public string? SortBy { get; set; } = null;
        public bool IsDecending { get; set; } = false;

        public int pageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 20;
    }
}