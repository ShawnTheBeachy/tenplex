using Prism.Unity;
using System;
using Tenplex.Models;
using Tenplex.Services;
using Tenplex.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Animation;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

namespace Tenplex.Views
{
    public sealed partial class AlbumPage : Page
    {
        private readonly AuthorizationService _authorizationService;
        private readonly ConnectionsService _connectionsService;
        private AlbumPageViewModel ViewModel => DataContext as AlbumPageViewModel;

        public AlbumPage()
        {
            InitializeComponent();
            _authorizationService = Prism.PrismApplicationBase.Current.Container.Resolve<AuthorizationService>();
            _connectionsService = Prism.PrismApplicationBase.Current.Container.Resolve<ConnectionsService>();
        }

        public ImageSource GetImageSource(string url)
        {
            var bitmap = new BitmapImage(new Uri($"{_connectionsService.CurrentConnection.Uri}{url}?X-Plex-Token={_authorizationService.GetAccessToken()}"));
            return bitmap;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("albumImage");
            animation?.TryStart(AlbumImage, new[] { AlbumTitle, AlbumArtist, AlbumYear });
        }
        
        private void TracksListView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var selectedTrack = e.ClickedItem as Track;
            ViewModel.PlayTrack(selectedTrack);
        }
    }
}
