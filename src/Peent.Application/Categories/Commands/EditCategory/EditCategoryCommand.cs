using MediatR;

namespace Peent.Application.Categories.Commands.EditCategory
{
    public class EditCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
    }
}
