using System.Threading.Tasks;
using Peent.Domain.Entities;
using AutoFixture;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Infrastructure
{
    public class CategoryBuilder
    {
        private string _name;
        private string _description;

        public CategoryBuilder WithRandomData()
        {
            _name = F.Create<string>();
            _description = F.Create<string>();
            return this;
        }

        public CategoryBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public CategoryBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public async Task<Category> Build()
        {
            var category = new Category(_name, _description);

            await InsertAsync(category);

            return category;
        }

        public static implicit operator Category(CategoryBuilder builder)
        {
            return builder.Build().GetAwaiter().GetResult();
        }
    }
}