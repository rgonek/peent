using Peent.Domain.Entities;

namespace Peent.Application.Categories.Models
{
    public class CategoryModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }

        public CategoryModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
            Description = category.Description;
        }
    }
}
