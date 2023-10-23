using Avalonia.Controls;
using Solution.TreeViewUtils;
using System;

namespace Solution.Views
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Opened += OnMainWindowInitialized;
        }

        private void OnMainWindowInitialized(object? sender, EventArgs e)
        {
            _genresTreeView.ExpandAllNodes();
        }
    }
}