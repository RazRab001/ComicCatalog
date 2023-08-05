using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicCatalog
{
    internal class SortByIssue : IComparer<Comic>
    {
        public int Compare(Comic? x, Comic? y)
        {
            if(x.Issue > y.Issue) return 1;
            else if(x.Issue < y.Issue) return -1;
            else return 0;
        }
    }
}
