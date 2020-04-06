namespace Peent.Application.Infrastructure
{
    public interface IHavePaginationInfo
    {
        int PageSize { get; set; }
        int PageIndex { get; set; }
    }
}
