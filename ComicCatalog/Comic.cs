
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicCatalog
{
    [Serializable]
    public class Comic
    {
        public string Name { get; init; }
        public int Issue { get; init; }
        public Comic(string name, int issue)
        {
            Name = name;
            Issue = issue;
        }
        public override string ToString() => $"{Name} (Issue #{Issue})";
    }
}
