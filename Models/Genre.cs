using Solution.TreeViewUtils;
using System.Collections.Generic;

namespace Solution.Models
{
    public class Genre : ITreeViewNodeContent
    {
        public List<Book> Books { get; set; } = new();

        public string Name { get; set; } = string.Empty;

        public string TreeViewNodeText => Name;
    }
}
