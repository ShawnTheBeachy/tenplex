using Prism.Unity;
using System;
using System.Reactive;
using Tenplex.Models;
using Tenplex.Services;
using Tenplex.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Navigation;

namespace Tenplex.Views
{
    public sealed partial class AlbumPage : Page
    {
        private readonly AuthorizationService _authorizationService;
        private readonly ConnectionsService _connectionsService;
        private readonly UsersService _usersService;
        private AlbumPageViewModel ViewModel => DataContext as AlbumPageViewModel;

        private IDisposable _currentUserSwitchedSubscription;

        public AlbumPage()
        {
            InitializeComponent();
            _authorizationService = Prism.PrismApplicationBase.Current.Container.Resolve<AuthorizationService>();
            _connectionsService = Prism.PrismApplicationBase.Current.Container.Resolve<ConnectionsService>();
            _usersService = Prism.PrismApplicationBase.Current.Container.Resolve<UsersService>();
        }
        
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("albumImage");
            animation?.TryStart(AlbumImage, new[] { AlbumTitle, AlbumArtist, AlbumYear });

            UpdateUserPermissions(_usersService.CurrentUser);
            _currentUserSwitchedSubscription = _usersService.CurrentUserSwitched.Subscribe(UpdateUserPermissions);
        }

        protected override void OnNavigatedFrom(NavigationEventArgs e)
        {
            base.OnNavigatedFrom(e);
            _currentUserSwitchedSubscription.Dispose();
        }

        private void ClearPosterHyperlink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            ViewModel.Properties.Thumb = null;

            foreach (var poster in ViewModel.Posters)
                poster.IsSelected = false;
        }

        private async void EditButton_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.EditAlbumAsync();
            AlbumEditPopup.ShowAt(this);
        }

        private void PostersGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            foreach (var poster in ViewModel.Posters)
                poster.IsSelected = false;

            (e.ClickedItem as Poster).IsSelected = true;
            // ViewModel.Album.Thumb = (e.ClickedItem as Poster).Thumb;
        }

        private async void PostersGridView_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadPostersAsync();
        }

        private async void SavePropertiesButton_Click(object sender, RoutedEventArgs e)
        {
            await ViewModel.UpdateAlbumAsync();
            AlbumEditPopup.Hide();
        }

        private void TracksListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedTrack = e.ClickedItem as Track;
            ViewModel.PlayTrack(selectedTrack);
        }

        private void UpdateUserPermissions(User user)
        {
            if (user.IsAdmin)
                EditButton.Visibility = Visibility.Visible;
            else
                EditButton.Visibility = Visibility.Collapsed;
        }
    }
}
