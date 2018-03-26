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
        private readonly ServerConnectionInfoService _connectionInfoService;
        private AlbumPageViewModel ViewModel => DataContext as AlbumPageViewModel;

        public AlbumPage()
        {
            InitializeComponent();
            _connectionInfoService = Prism.PrismApplicationBase.Current.Container.Resolve<ServerConnectionInfoService>();
        }

        public ImageSource GetImageSource(string url)
        {
            var bitmap = new BitmapImage(new Uri($"http://{_connectionInfoService.GetServerIpAddress()}:{_connectionInfoService.GetServerPortNumber()}{url}?X-Plex-Token={_connectionInfoService.GetPlexAccessToken()}"));
            return bitmap;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            var animation = ConnectedAnimationService.GetForCurrentView().GetAnimation("albumImage");
            animation?.TryStart(AlbumImage);
        }

        private void TracksListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var selectedTrack = TracksListView.SelectedItem as Track;
            ViewModel.PlayTrack(selectedTrack);
        }
    }
}
