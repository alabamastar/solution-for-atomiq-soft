using Solution.TreeViewUtils;
using System;

namespace Solution.Models
{
    public class Book : ITreeViewNodeContent
    {
        public string Author { get; set; } = string.Empty;

        public string GenreName { get; set; } = string.Empty;

        public Guid Id { get; set; } = Guid.NewGuid();

        public string Title { get; set; } = string.Empty;

        public string TreeViewNodeText => Title;
    }
}
