using Tenplex.Models;
using Tenplex.ViewModels;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tenplex.Views
{
    public sealed partial class UsersPage : Page
    {
        private UsersPageViewModel ViewModel => DataContext as UsersPageViewModel;

        public UsersPage()
        {
            InitializeComponent();
        }

        private async void SubmitPasswordButton_Click(object sender, RoutedEventArgs e)
        {
            var user = PasswordStackPanel.DataContext as User;
            var password = PasswordBox.Password;
            await ViewModel.SelectUserAsync(user, password);
        }

        private async void UsersGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            var user = e.ClickedItem as User;

            if (!user.IsProtected)
                await ViewModel.SelectUserAsync(user);
            else
            {
                PasswordStackPanel.DataContext = user;
                PasswordFlyout.ShowAt(this);
            }
        }
    }
}
