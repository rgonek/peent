using MediatR;

namespace Peent.Application.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommand : IRequest<Unit>
    {
        public int Id { get; }
        
        public DeleteCategoryCommand(int id)
        {
            Id = id;
        }
    }
}
