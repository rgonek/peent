using System.Collections.Generic;
using MediatR;
using Peent.Application.Tags.Models;

namespace Peent.Application.Tags.Queries.GetTagsList
{
    public class GetTagsListQuery : IRequest<IList<TagModel>>
    {
    }
}
