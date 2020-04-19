namespace Peent.Application.Infrastructure
{
    public interface IHavePagination
    {
        int PageSize { get; set; }
        int PageIndex { get; set; }
    }
}
