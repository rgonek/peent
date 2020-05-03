namespace Peent.Application.Common.DynamicQuery.Contracts
{
    public interface IHavePagination
    {
        int PageSize { get; set; }
        int PageIndex { get; set; }
    }
}
