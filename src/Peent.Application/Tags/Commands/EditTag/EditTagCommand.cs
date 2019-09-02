using System;
using MediatR;

namespace Peent.Application.Tags.Commands.EditTag
{
    public class EditTagCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
    }
}
