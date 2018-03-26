using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using Prism.Unity;
using Tenplex.Services;
using Tenplex.Views;
using Windows.UI.Xaml;

namespace Tenplex.ViewModels
{
    public class ConnectionInfoPageViewModel : ViewModelBase
    {
        private ServerConnectionInfoService _serverConnectionInfoService;

        #region ContinueCommand

        private DelegateCommand _continueCommand;
        public DelegateCommand ContinueCommand =>
            this._continueCommand ?? (this._continueCommand = new DelegateCommand(async () =>
            {
                this._serverConnectionInfoService.SetPlexAccessToken(this.PlexAccessToken);
                this._serverConnectionInfoService.SetServerIpAddress(this.ServerIpAddress);
                this._serverConnectionInfoService.SetServerPortNumber(this.ServerPortNumber);

                var shell = Prism.PrismApplicationBase.Current.Container.Resolve<ShellPage>();
                Window.Current.Content = shell;

                var navigationService = shell.ShellView.NavigationService;
                var navigationPath = PathBuilder.Create(nameof(ConnectionInfoPage)).ToString();
                await navigationService.NavigateAsync(navigationPath);

            }, () =>
                !string.IsNullOrWhiteSpace(this.ServerIpAddress) &&
                !string.IsNullOrWhiteSpace(this.ServerPortNumber) &&
                !string.IsNullOrWhiteSpace(this.PlexAccessToken)));

        #endregion ContinueCommand

        #region PlexAccessToken

        private string _plexAccessToken = default(string);
        public string PlexAccessToken
        {
            get => _plexAccessToken;
            set
            {
                if (SetProperty(ref this._plexAccessToken, value))
                    this.ContinueCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion PlexAccessToken

        #region ServerIpAddress

        private string _serverIpAddress = default(string);
        public string ServerIpAddress
        {
            get => _serverIpAddress;
            set
            {
                if (SetProperty(ref this._serverIpAddress, value))
                    this.ContinueCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion ServerIpAddress

        #region ServerPortNumber

        private string _serverPortNumber = default(string);
        public string ServerPortNumber
        {
            get => _serverPortNumber;
            set
            {
                if (SetProperty(ref this._serverPortNumber, value))
                    this.ContinueCommand.RaiseCanExecuteChanged();
            }
        }

        #endregion ServerPortNumber

        public ConnectionInfoPageViewModel(ServerConnectionInfoService serverConnectionInfoProvider)
        {
            this._serverConnectionInfoService = serverConnectionInfoProvider ?? throw new System.ArgumentNullException(nameof(serverConnectionInfoProvider));
        }
    }
}
