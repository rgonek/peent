using MediatR;
using Peent.Application.Tags.Models;

namespace Peent.Application.Tags.Queries.GetTag
{
    public class GetTagQuery : IRequest<TagModel>
    {
        public int Id { get; set; }
    }
}
