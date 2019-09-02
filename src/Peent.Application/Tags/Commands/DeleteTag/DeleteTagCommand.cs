using MediatR;

namespace Peent.Application.Tags.Commands.DeleteTag
{
    public class DeleteTagCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
