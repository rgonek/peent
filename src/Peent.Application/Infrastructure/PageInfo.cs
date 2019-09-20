namespace Peent.Application.Infrastructure
{
    public class PageInfo
    {
        public int Page { get; set; }
        public int PageSize { get; set; }
        public int SkipRecords => (Page - 1) * PageSize;
    }
}
