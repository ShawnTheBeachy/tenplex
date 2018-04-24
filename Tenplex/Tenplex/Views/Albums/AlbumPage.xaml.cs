using Prism.Unity;
using Tenplex.Models;
using Tenplex.Services;
using Tenplex.ViewModels;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Animation;
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
