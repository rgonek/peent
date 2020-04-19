﻿using System;
using System.Collections.Generic;
using MediatR;
using Peent.Application.Common;
using Peent.Application.Infrastructure;
using Peent.Application.Tags.Models;

namespace Peent.Application.Tags.Queries.GetTagsList
{
    public class GetTagsListQuery : IRequest<PagedResult<TagModel>>,
        IHavePagination, IHaveFilters, IHaveSorts
    {
        public int PageSize { get; set; } = 10;

        public int PageIndex { get; set; } = 1;

        public IList<SortDto> Sort { get; } = new List<SortDto>();
        public IList<FilterDto> Filters { get; } = new List<FilterDto>();
    }
}
