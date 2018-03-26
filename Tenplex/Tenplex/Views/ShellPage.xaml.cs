using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Template10.Controls;
using Tenplex.Models;
using Tenplex.Services;
using Windows.Media.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tenplex.Views
{
    public sealed partial class ShellPage : Page
    {
        private readonly IGestureService _gestureService;
        private readonly LibrarySectionsService _librarySectionsService;

        public ShellPage(LibrarySectionsService librarySectionsService)
        {
            InitializeComponent();

            _gestureService = GestureService.GetForCurrentView();
            _librarySectionsService = librarySectionsService ?? throw new ArgumentNullException(nameof(librarySectionsService));

            ShellView.Initialize();
            ShellView.Loaded += (s, e) =>
            {
                SetupGestures();
            };
        }

        private void SetupGestures()
        {
            _gestureService.BackRequested += async (s, e) => await ShellView.NavigationService.GoBackAsync();
            _gestureService.ForwardRequested += async (s, e) => await ShellView.NavigationService.GoForwardAsync();
            _gestureService.RefreshRequested += async (s, e) => await ShellView.NavigationService.RefreshAsync();
            _gestureService.MenuRequested += (s, e) => ShellView.IsPaneOpen = true;
            _gestureService.SearchRequested += (s, e) =>
            {
                ShellView.IsPaneOpen = true;
                ShellView.AutoSuggestBox?.Focus(FocusState.Programmatic);
            };
        }

        public static IList<NavigationViewItem> GetLibrarySections(ObservableCollection<LibrarySection> sections)
        {
            var items = new List<NavigationViewItem>();

            foreach (var section in sections)
            {
                var item = new NavigationViewItem
                {
                    Content = section.Title
                };
                NavViewProps.SetNavigationUri(item, PathBuilder.Create("MusicPage", ("sectionKey", section.Key)).ToString());
                items.Add(item);
            }

            return items;
        }

        public void Play(string url, string posterUrl)
        {
            Player.Source = MediaSource.CreateFromUri(new Uri(url));
            // Player.PosterSource = new BitmapImage(new Uri(posterUrl));
        }
    }
}
