using System;
using Peent.Domain.Entities;

namespace Peent.Application.Tags.Models
{
    public class TagModel
    {
        public int Id { get; set; }

        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime? Date { get; set; }

        public TagModel(Tag tag)
        {
            Id = tag.Id;
            Name = tag.Name;
            Description = tag.Description;
            Date = tag.Date;
        }
    }
}
