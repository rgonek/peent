using System;
using System.Collections.Generic;

namespace Peent.Application.Common
{
    public abstract class PagedResultBase : IPagedResult
    {
        public int CurrentPage { get; set; }
        public int PageCount { get; set; }
        public int PageSize { get; set; }
        public int RowCount { get; set; }

        public int FirstRowOnPage => (CurrentPage - 1) * PageSize + 1;

        public int LastRowOnPage => Math.Min(CurrentPage * PageSize, RowCount);
    }

    public class PagedResult<T> : PagedResultBase, IPagedResult<T>
        where T : class
    {
        public IList<T> Results { get; set; }

        public PagedResult()
        {
            Results = new List<T>();
        }
    }

    public interface IPagedResult
    {
        int CurrentPage { get; }
        int PageCount { get; }
        int PageSize { get; }
        int RowCount { get; }
        int FirstRowOnPage { get; }
        int LastRowOnPage { get; }
    }

    public interface IPagedResult<T> : IPagedResult
    {
        IList<T> Results { get; }
    }
}
