using System.Threading.Tasks;
using Peent.Domain.Entities;
using AutoFixture;
using static Peent.CommonTests.Infrastructure.TestFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Infrastructure
{
    public class TagBuilder
    {
        private string _name;
        private string _description;

        public TagBuilder WithRandomData()
        {
            _name = F.Create<string>();
            _description = F.Create<string>();
            return this;
        }

        public TagBuilder WithName(string name)
        {
            _name = name;
            return this;
        }

        public TagBuilder WithDescription(string description)
        {
            _description = description;
            return this;
        }

        public async Task<Tag> Build()
        {
            var tag = new Tag(_name, _description);

            await InsertAsync(tag);

            return tag;
        }

        public static implicit operator Tag(TagBuilder builder)
        {
            return builder.Build().GetAwaiter().GetResult();
        }
    }
}