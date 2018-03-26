using Prism.Ioc;
using Prism.Navigation;
using Prism.Unity;
using System;
using Template10.Services.Serialization;
using Tenplex.Services;
using Tenplex.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Tenplex.Views
{
    public sealed partial class AlbumsPage : Page
    {
        private readonly ISerializationService _serializationService;
        private AlbumsPageViewModel ViewModel => (DataContext as AlbumsPageViewModel);

        public AlbumsPage()
        {
            InitializeComponent();
            _serializationService = Prism.PrismApplicationBase.Current.Container.Resolve<ISerializationService>();
        }

        private async void AlbumsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            // Prepare connected artwork animation.
            AlbumsGridView.PrepareConnectedAnimation("albumImage", e.ClickedItem, "AlbumArtworkImage");

            var path = PathBuilder.Create(nameof(AlbumPage), ("album", _serializationService.Serialize(e.ClickedItem))).ToString();
            var container = Prism.PrismApplicationBase.Current.Container as UnityContainerExtension;
            await container.Resolve<ShellPage>().ShellView.NavigationService.NavigateAsync(path);
        }

        private async void ArtistsButton_Click(object sender, RoutedEventArgs e)
        {
            PageRegistry.RemoveRegistration("MusicPage");
            var container = Prism.PrismApplicationBase.Current.Container as UnityContainerExtension;

            container.RegisterForNavigation<ArtistsPage, ConnectionInfoPageViewModel>("MusicPage");
            await container.Resolve<ShellPage>().ShellView.NavigationService.NavigateAsync(PathBuilder.Create("MusicPage").ToString());
        }

        public static ImageSource GetAlbumArtworkUrl(string thumbnail)
        {
            var connectionInfoService = Prism.PrismApplicationBase.Current.Container.Resolve<ServerConnectionInfoService>();
            var bitmap = new BitmapImage(new Uri($"http://{connectionInfoService.GetServerIpAddress()}:{connectionInfoService.GetServerPortNumber()}{thumbnail}?X-Plex-Token={connectionInfoService.GetPlexAccessToken()}"));
            return bitmap;
        }
    }
}
