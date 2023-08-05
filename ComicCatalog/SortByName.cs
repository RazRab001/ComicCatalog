using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComicCatalog
{
    internal class SortByName : IComparer<Comic>
    {
        public int Compare(Comic? x, Comic? y)
        {
            return string.Compare(x.Name, y.Name);
        }
    }
}
