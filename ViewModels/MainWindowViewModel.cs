using Avalonia.Controls;
using DynamicData;
using ReactiveUI;
using Solution.Models;
using Solution.TreeViewUtils;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reactive;
using System.Reactive.Linq;

namespace Solution.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private ObservableCollection<TreeViewNode> _genreTreeViewNodes;
        private bool _isSearchPerformed = false;
        private string _searchString = string.Empty;

        public ReactiveCommand<TreeViewNode, Unit> AddBookOrGenreCommand { get; }

        public ObservableCollection<Book> Books { get; } = new();

        public ReactiveCommand<TreeView, Unit> CollapseAllNodesCommand { get; }

        public ReactiveCommand<Guid, Unit> DeleteBookCommand { get; }

        public ReactiveCommand<TreeView, Unit> ExpandAllNodesCommand { get; }

        public ObservableCollection<TreeViewNode> GenreTreeViewNodes
        {
            get => _genreTreeViewNodes;
            set => this.RaiseAndSetIfChanged(ref _genreTreeViewNodes, value);
        }

        public ReactiveCommand<TreeView, Unit> SearchBooksCommand { get; }

        public string SearchString
        {
            get => _searchString;
            set => this.RaiseAndSetIfChanged(ref _searchString, value);
        }

        public ReactiveCommand<TreeView, Unit> TreeViewPointerPressedCommand { get; }

        public MainWindowViewModel()
        {
            _genreTreeViewNodes = GetGenresInfo();

            AddBookOrGenreCommand = ReactiveCommand.Create<TreeViewNode>(AddBookOrGenre);
            CollapseAllNodesCommand = ReactiveCommand.Create<TreeView>(CollapseAllNodes);
            DeleteBookCommand = ReactiveCommand.Create<Guid>(DeleteBook);
            ExpandAllNodesCommand = ReactiveCommand.Create<TreeView>(ExpandAllNodes);
            SearchBooksCommand = ReactiveCommand.Create<TreeView>(SearchBooks);
            TreeViewPointerPressedCommand = ReactiveCommand.Create<TreeView>(TreeViewPointerPressed);
        }

        private void AddBookOrGenre(TreeViewNode selectedNode)
        {
            if (selectedNode is null)
                return;

            if (selectedNode.Content is Genre genre)
            {
                if (_isSearchPerformed)
                    return;

                foreach (var b in genre.Books)
                {
                    if (!Books.Contains(b))
                        Books.Add(b);
                }

                return;
            }

            if (selectedNode.Content is Book book)
            {
                if (!Books.Contains(book))
                    Books.Add(book);
            }
        }

        private static void CollapseAllNodes(TreeView treeView) => treeView?.CollapseAllNodes();

        private void DeleteBook(Guid bookId)
        {
            var itemForDelete = Books.FirstOrDefault(book => book.Id == bookId);
            if (itemForDelete is not null)
                Books.Remove(itemForDelete);
        }

        private static void ExpandAllNodes(TreeView treeView) => treeView?.ExpandAllNodes();

        private static ObservableCollection<TreeViewNode> GetGenresInfo()
        {
            var fantasyGenre = new Genre { Name = "Фэнтези" };
            var detectiveGenre = new Genre { Name = "Детективы" };
            var horrorGenre = new Genre { Name = "Ужасы" };
            var dystopiaGenre = new Genre { Name = "Антиутопии" };

            var fantasyGenreBooks = new List<Book>
            {
                new Book { Author = "Анджей Сапковский", GenreName = fantasyGenre.Name, Title = "Ведьмак" },
                new Book { Author = "Джордж Мартин", GenreName = fantasyGenre.Name, Title = "Игра престолов" },
                new Book { Author = "Урсула ле Гуин", GenreName = fantasyGenre.Name, Title = "Волшебник Земноморья" },
                new Book { Author = "Стивен Кинг", GenreName = fantasyGenre.Name, Title = "Стрелок" },
                new Book { Author = "Макс Фрай", GenreName = fantasyGenre.Name, Title = "Лабиринты Ехо" },
            };

            var detectiveGenreBooks = new List<Book>
            {
                new Book { Author = "Эдгар Аллан По", GenreName = detectiveGenre.Name, Title = "Убийство на улице Морг" },
                new Book { Author = "Уилки Коллинз", GenreName = detectiveGenre.Name, Title = "Лунный камень" },
                new Book { Author = "Артур Конан Дойл", GenreName = detectiveGenre.Name, Title = "Рассказы о Шерлоке Холмсе" },
                new Book { Author = "Гилберт Кийт Честертон", GenreName = detectiveGenre.Name, Title = "Тайна отца Брауна" },
                new Book { Author = "Дэшил Хэммет", GenreName = detectiveGenre.Name, Title = "Мальтийский сокол" },
            };

            var horrorGenreBooks = new List<Book>
            {
                new Book { Author = "Стивен Кинг", GenreName = horrorGenre.Name, Title = "Мизери" },
                new Book { Author = "Нил Гейман", GenreName = horrorGenre.Name, Title = "Коралина" },
                new Book { Author = "Джеральд Бром", GenreName = horrorGenre.Name, Title = "Косиног. История о колдовстве" },
                new Book { Author = "Стивен Кинг", GenreName = horrorGenre.Name, Title = "Куджо" },
                new Book { Author = "Владимир Короткевич", GenreName = horrorGenre.Name, Title = "Дикая охота короля Стаха" },
            };

            var dystopiaGenreBooks = new List<Book>
            {
                new Book { Author = "Олдос Хаксли", GenreName = dystopiaGenre.Name, Title = "О дивный новый мир" },
                new Book { Author = "Джордж Оруэлл", GenreName = dystopiaGenre.Name, Title = "Скотный двор" },
                new Book { Author = "Кен Кизи", GenreName = dystopiaGenre.Name, Title = "Пролетая над гнездом кукушки" },
                new Book { Author = "Энтони Берджесс", GenreName = dystopiaGenre.Name, Title = "Заводной Апельсин" },
                new Book { Author = "Евгений Замятин", GenreName = dystopiaGenre.Name, Title = "Мы" },
            };

            fantasyGenre.Books = fantasyGenreBooks;
            detectiveGenre.Books = detectiveGenreBooks;
            horrorGenre.Books = horrorGenreBooks;
            dystopiaGenre.Books = dystopiaGenreBooks;

            var fantasyGenreTreeViewNode = new TreeViewNode(fantasyGenre, fantasyGenre.Books.Cast<ITreeViewNodeContent>().ToList());
            var detectiveGenreTreeViewNode = new TreeViewNode(detectiveGenre, detectiveGenre.Books.Cast<ITreeViewNodeContent>().ToList());
            var horrorGenreTreeViewNode = new TreeViewNode(horrorGenre, horrorGenre.Books.Cast<ITreeViewNodeContent>().ToList());
            var dystopiaGenreTreeViewNode = new TreeViewNode(dystopiaGenre, dystopiaGenre.Books.Cast<ITreeViewNodeContent>().ToList());

            return new()
            {
                fantasyGenreTreeViewNode,
                detectiveGenreTreeViewNode,
                horrorGenreTreeViewNode,
                dystopiaGenreTreeViewNode,
            };
        }

        private void SearchBooks(TreeView treeView)
        {
            if (treeView is null)
                return;

            if (string.IsNullOrEmpty(SearchString))
            {
                _isSearchPerformed = false;
                treeView.ResetAllNodesVisibility();
                return;
            }

            _isSearchPerformed = true;
            bool searchPredicate(string nodeText) => nodeText.ToLower().Contains(SearchString.ToLower());
            treeView.PerformNodesFiltering(searchPredicate);
        }

        private static void TreeViewPointerPressed(TreeView treeView) => treeView?.UnselectAll();
    }
}