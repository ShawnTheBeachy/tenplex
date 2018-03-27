using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Services.Web;
using Template10.Utilities;
using Tenplex.Models;
using Tenplex.Services;
using Tenplex.Views;
using Windows.UI.Xaml;

namespace Tenplex.ViewModels
{
    public class UsersPageViewModel : ViewModelBase
    {
        private readonly IWebApiService _apiService;
        private readonly ConnectionsService _connectionsService;
        private readonly DevicesService _devicesService;
        private readonly LibrarySectionsService _librarySectionsService;
        private NavigationService _navigationService;
        private readonly ShellPage _shell;
        private readonly UsersService _usersService;

        #region RememberSelection

        private bool _rememberSelection = true;
        public bool RememberSelection { get => _rememberSelection; set => SetProperty(ref _rememberSelection, value); }

        #endregion RememberSelection

        #region Users

        private ObservableCollection<User> _users = new ObservableCollection<User>();
        public ObservableCollection<User> Users => _users;

        #endregion Users

        public UsersPageViewModel(IWebApiService apiService, ConnectionsService connectionsService, DevicesService devicesService, LibrarySectionsService librarySectionsService, ShellPage shell, UsersService usersService)
        {
            _apiService = apiService ?? throw new ArgumentNullException(nameof(apiService));
            _connectionsService = connectionsService ?? throw new ArgumentNullException(nameof(connectionsService));
            _devicesService = devicesService ?? throw new ArgumentNullException(nameof(devicesService));
            _librarySectionsService = librarySectionsService ?? throw new ArgumentNullException(nameof(librarySectionsService));
            _shell = shell ?? throw new ArgumentNullException(nameof(shell));
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        public async override Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            _navigationService = parameters.GetNavigationService();
            Users.AddRange(await _usersService.LoadUsersAsync());
        }

        public async Task SelectUserAsync(User user)
        {
            if (RememberSelection)
                _usersService.SetDefaultUserId(user.Id);

            var switchedUser = await _usersService.SwitchUserAsync(user);

            if (switchedUser != null)
            {
                await _devicesService.InitializeAsync();
                await _connectionsService.InitializeAsync();
                
                _apiService.AddHeader("X-Plex-Platform", "Windows");
                _apiService.AddHeader("X-Plex-Platform-Version", "1709");       //TODO: Actually get Windows version.
                _apiService.AddHeader("X-Plex-Provides", "player");
                _apiService.AddHeader("X-Plex-Client-Identifier", "tenplex");   //TODO: Get device UUID.
                _apiService.AddHeader("X-Plex-Product", "Tenplex");
                _apiService.AddHeader("X-Plex-Version", "1.0.0.0");
                _apiService.AddHeader("X-Plex-Device", "Unknown");              //TODO: Get manufacturer info.
                _apiService.AddHeader("X-Plex-Token", _devicesService.CurrentDevice.AccessToken);
                
                await _librarySectionsService.InitializeAsync();

                Window.Current.Content = _shell;
                var path = PathBuilder.Create(nameof(UsersPage)).ToString();
                await _shell.ShellView.NavigationService.NavigateAsync(path);
            }
        }
    }
}
