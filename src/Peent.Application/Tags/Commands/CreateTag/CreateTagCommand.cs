using System;
using MediatR;

namespace Peent.Application.Tags.Commands.CreateTag
{
    public class CreateTagCommand : IRequest<int>
    {
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
