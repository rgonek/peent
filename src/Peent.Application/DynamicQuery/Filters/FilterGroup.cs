using System.Collections.Generic;
using System.Linq;
using EnsureThat;

namespace Peent.Application.DynamicQuery.Filters
{
    public class FilterGroup : FilterNode
    {
        public Logic Logic { get; }
        private readonly IList<FilterNode> _filterNodes;

        public FilterGroup(Logic logic)
            : this(logic, Enumerable.Empty<FilterNode>())
        {
        }

        public FilterGroup(Logic logic, IEnumerable<FilterNode> filterNodes)
        {
            Ensure.That(filterNodes, nameof(filterNodes)).IsNotNull();

            Logic = logic;
            _filterNodes = filterNodes.ToList();
        }

        public void Add(FilterNode filterNode)
        {
            Ensure.That(filterNode, nameof(filterNode)).IsNotNull();

            _filterNodes.Add(filterNode);
        }

        public override string GetPredicate()
        {
            return "(" +
                   string.Join(
                       $" {Logic} ",
                       _filterNodes.Select(f => f.GetPredicate())) +
                   ")";
        }
    }
}
