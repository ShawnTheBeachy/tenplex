using Prism;
using Prism.Ioc;
using Prism.Navigation;
using Prism.Unity;
using System.Threading.Tasks;
using Template10.Services.Compression;
using Template10.Services.Dialog;
using Template10.Services.File;
using Template10.Services.Marketplace;
using Template10.Services.Nag;
using Template10.Services.Network;
using Template10.Services.Resources;
using Template10.Services.Secrets;
using Template10.Services.Serialization;
using Template10.Services.Settings;
using Template10.Services.Web;
using Tenplex.Services;
using Tenplex.ViewModels;
using Tenplex.Views;
using Windows.ApplicationModel.Activation;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tenplex
{
    sealed partial class App : PrismApplication
    {
        public App()
        {
            InitializeComponent();
        }

        public override void RegisterTypes(IContainerRegistry container)
        {
            // Standard Template10 services.

            container.RegisterSingleton<ICompressionService, CompressionService>();
            container.RegisterSingleton<IDialogService, DialogService>();
            container.RegisterSingleton<IFileService, FileService>();
            container.RegisterSingleton<IMarketplaceService, MarketplaceService>();
            container.RegisterSingleton<INagService, NagService>();
            container.RegisterSingleton<INetworkService, NetworkService>();
            container.RegisterSingleton<IResourceService, ResourceService>();
            container.RegisterSingleton<ISecretService, SecretService>();
            container.RegisterSingleton<ISettingsHelper, SettingsHelper>();
            container.RegisterSingleton<IWebApiService, WebApiService>();
            container.RegisterSingleton<ISettingsAdapter, LocalSettingsAdapter>();

            // Custom services.

            container.RegisterSingleton<ISerializationService, NewtonsoftSerializationService>();
            container.RegisterSingleton<ServerConnectionInfoService, ServerConnectionInfoService>();
            container.RegisterSingleton<LibrarySectionsService, LibrarySectionsService>();
            container.Register<AlbumsService, AlbumsService>();
            container.Register<TracksService, TracksService>();

            // Pages and view-models.

            container.RegisterSingleton<ShellPage, ShellPage>();
            container.RegisterForNavigation<ConnectionInfoPage, ConnectionInfoPageViewModel>(nameof(ConnectionInfoPage));
            container.RegisterForNavigation<AlbumsPage, AlbumsPageViewModel>("MusicPage");
            container.RegisterForNavigation<AlbumPage, AlbumPageViewModel>(nameof(AlbumPage));
        }

        public override async Task OnStartAsync(StartArgs args)
        {
            var navigationPath = string.Empty;
            INavigationService navigationService = null;

            switch (args.StartKind)
            {
                case StartKinds.Launch when (args.Arguments is ILaunchActivatedEventArgs e):
                    Window.Current.Content = new SplashPage(e.SplashScreen);

                    var connectionInfoService = Container.Resolve<ServerConnectionInfoService>();
                    var hasConnectionInfo = connectionInfoService.HasConnectionInfo();

                    if (hasConnectionInfo)
                    {
                        var apiService = Container.Resolve<IWebApiService>();

                        apiService.AddHeader("X-Plex-Platform", "Windows");
                        apiService.AddHeader("X-Plex-Platform-Version", "1709");       //TODO: Actually get Windows version.
                        apiService.AddHeader("X-Plex-Provides", "player");
                        apiService.AddHeader("X-Plex-Client-Identifier", "tenplex");   //TODO: Get device UUID.
                        apiService.AddHeader("X-Plex-Product", "Tenplex");
                        apiService.AddHeader("X-Plex-Version", "1.0.0.0");
                        apiService.AddHeader("X-Plex-Device", "Unknown");              //TODO: Get manufacturer info.
                        apiService.AddHeader("X-Plex-Token", connectionInfoService.GetPlexAccessToken());

                        var shell = Container.Resolve<ShellPage>();
                        Window.Current.Content = shell;

                        navigationService = shell.ShellView.NavigationService;
                        navigationPath = PathBuilder.Create(nameof(ConnectionInfoPage)).ToString();

                        var librarySectionsService = Container.Resolve<LibrarySectionsService>();
                        await librarySectionsService.InitializeAsync();
                    }

                    else
                    {
                        var frame = new Frame();
                        Window.Current.Content = frame;

                        navigationService = (IPlatformNavigationService)NavigationService.Create(frame, Gestures.Back, Gestures.Forward, Gestures.Refresh);
                        navigationPath = PathBuilder.Create(nameof(ConnectionInfoPage)).ToString();
                    }

                    await navigationService.NavigateAsync(navigationPath);
                    break;
                case StartKinds.Prelaunch:
                case StartKinds.Activate:
                case StartKinds.Background:
                    return;
            }
        }
    }
}
