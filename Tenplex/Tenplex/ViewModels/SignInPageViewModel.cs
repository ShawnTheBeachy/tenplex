using Prism.Commands;
using Prism.Mvvm;
using Prism.Navigation;
using System;
using Tenplex.Services;
using Tenplex.Views;

namespace Tenplex.ViewModels
{
    public class SignInPageViewModel : ViewModelBase
    {
        private readonly AuthorizationService _authorizationService;
        private NavigationService _navigationService;
        private ServersService _serversService;
        private UsersService _usersService;

        #region IsSigningIn

        private bool _isSigningIn = false;
        public bool IsSigningIn { get => _isSigningIn; set => SetProperty(ref _isSigningIn, value); }

        #endregion IsSigningIn

        #region Password

        private string _password = default(string);
        public string Password { get => _password; set => SetProperty(ref _password, value); }

        #endregion Password

        #region SignInCommand

        private DelegateCommand _signInCommand;
        public DelegateCommand SignInCommand =>
            _signInCommand ?? (_signInCommand = new DelegateCommand(async () =>
            {
                IsSigningIn = true;
                SignInCommand.RaiseCanExecuteChanged();

                var response = await _authorizationService.SignInAsync(Username, Password);

                if (response != null)
                {
                    _authorizationService.SetAccessToken(response.User.AuthToken);
                    await _serversService.InitializeAsync();
                    await _usersService.InitializeAsync();
                    var path = string.Empty;

                    if (_usersService.CurrentUser == null)
                        path = PathBuilder.Create(nameof(UsersPage)).ToString();
                    else
                        path = PathBuilder.Create(nameof(UsersPage)).ToString();

                    await _navigationService.NavigateAsync(path);
                }

                IsSigningIn = false;
                SignInCommand.RaiseCanExecuteChanged();
            }, () => !IsSigningIn));

        #endregion SignInCommand

        #region Username

        private string _username = default(string);
        public string Username { get => _username; set => SetProperty(ref _username, value); }

        #endregion Username

        public SignInPageViewModel(AuthorizationService authorizationService, ServersService serversService, UsersService usersService)
        {
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            _serversService = serversService ?? throw new ArgumentNullException(nameof(serversService));
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        public override void OnNavigatedTo(INavigationParameters parameters)
        {
            _navigationService = parameters.GetNavigationService();
            base.OnNavigatedTo(parameters);
        }
    }
}
