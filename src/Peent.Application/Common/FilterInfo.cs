using System.Collections.Generic;

namespace Peent.Application.Common
{
    public class FilterInfo
    {
        public string Field { get; set; }
        public IList<string> Values { get; set; } = new List<string>();

        public const string Global = "Q";
    }
}
