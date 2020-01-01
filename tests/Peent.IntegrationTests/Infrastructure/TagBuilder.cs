using System;
using System.Threading.Tasks;
using Peent.Application.Infrastructure.Extensions;
using Peent.Domain.Entities;
using AutoFixture;
using static Peent.IntegrationTests.Infrastructure.DatabaseFixture;

namespace Peent.IntegrationTests.Infrastructure
{
    public class TagBuilder
    {
        private string _name;
        private string _description;
        private DateTime _date;

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

        public TagBuilder At(DateTime date)
        {
            _date = date;
            return this;
        }

        public async Task<Tag> Build()
        {
            var tag = new Tag
            {
                Name = _name,
                Description = _description,
                Date = _date,
                WorkspaceId = UserAccessor.User.GetWorkspaceId()
            };

            await InsertAsync(tag);

            return tag;
        }

        public static implicit operator Tag(TagBuilder builder)
        {
            return builder.Build().GetAwaiter().GetResult();
        }
    }
}
