using Avalonia.Controls;
using System;

namespace Solution.TreeViewUtils
{
    public static class TreeViewExtensions
    {
        public static void CollapseAllNodes(this TreeView treeView)
            => ChangeNodesExpandingFlag(treeView, false);

        public static void ExpandAllNodes(this TreeView treeView) => ChangeNodesExpandingFlag(treeView, true);

        public static void PerformNodesFiltering(this TreeView treeView, Predicate<string> predicate)
        {
            if (treeView.Items is null)
                return;

            foreach (var item in treeView.Items)
            {
                var treeViewItem = (TreeViewItem)treeView.ItemContainerGenerator.Index.ContainerFromItem(item);
                foreach (TreeViewNode subItem in treeViewItem.Items)
                {
                    var treeViewSubItem
                        = (TreeViewItem)treeViewItem.ItemContainerGenerator.Index.ContainerFromItem(subItem);
                    if (treeViewSubItem is not null)
                        treeViewSubItem.IsVisible = predicate(subItem.Text);
                }
            }
        }

        public static void ResetAllNodesVisibility(this TreeView treeView)
        {
            if (treeView.Items is null)
                return;

            foreach (var item in treeView.Items)
            {
                var treeViewItem = (TreeViewItem)treeView.ItemContainerGenerator.Index.ContainerFromItem(item);
                foreach (var subItem in treeViewItem.Items)
                {
                    var treeViewSubItem
                        = (TreeViewItem)treeViewItem.ItemContainerGenerator.Index.ContainerFromItem(subItem);
                    if (treeViewSubItem is not null)
                        treeViewSubItem.IsVisible = true;
                }
            }
        }

        private static void ChangeNodesExpandingFlag(TreeView treeView, bool isExpanded)
        {
            if (treeView.Items is null)
                return;

            foreach (var item in treeView.Items)
            {
                var treeViewItem = (TreeViewItem)treeView.ItemContainerGenerator.Index.ContainerFromItem(item);
                if (treeViewItem is not null)
                    treeViewItem.IsExpanded = isExpanded;
            }
        }
    }
}
