using Prism.Ioc;
using Prism.Navigation;
using Prism.Unity;
using Tenplex.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tenplex.Views
{
    public sealed partial class ArtistsPage : Page
    {
        private ArtistsPageViewModel ViewModel => DataContext as ArtistsPageViewModel;

        public ArtistsPage()
        {
            InitializeComponent();
        }

        private async void AlbumsButton_Click(object sender, RoutedEventArgs e)
        {
            PageRegistry.RemoveRegistration("MusicPage");
            var container = Prism.PrismApplicationBase.Current.Container as UnityContainerExtension;

            container.RegisterForNavigation<AlbumsPage, AlbumsPageViewModel>("MusicPage");
            await container.Resolve<ShellPage>().ShellView.NavigationService.NavigateAsync(PathBuilder.Create("MusicPage", ("sectionKey", ViewModel.SectionKey)).ToString());
        }

        private void ArtistsGridView_ItemClick(object sender, ItemClickEventArgs e)
        {

        }
    }
}
