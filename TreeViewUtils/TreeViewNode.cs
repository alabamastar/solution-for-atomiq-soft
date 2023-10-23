using System.Collections.Generic;

namespace Solution.TreeViewUtils
{
    public class TreeViewNode
    {
        public ITreeViewNodeContent Content { get; set; }

        public List<TreeViewNode> SubNodes { get; set; } = new();

        public string Text => Content.TreeViewNodeText;

        public TreeViewNode(ITreeViewNodeContent content)
        {
            Content = content;
        }

        public TreeViewNode(ITreeViewNodeContent content, List<ITreeViewNodeContent> subNodesContent)
        {
            Content = content;

            foreach (var subNodeContent in subNodesContent)
            {
                SubNodes.Add(new TreeViewNode(subNodeContent));
            }
        }
    }
}
