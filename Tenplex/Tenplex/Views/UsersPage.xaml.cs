using Tenplex.Models;
using Tenplex.ViewModels;
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

        private async void UsersGridView_ItemClick(object sender, ItemClickEventArgs e)
        {
            await ViewModel.SelectUserAsync(e.ClickedItem as User);
        }
    }
}
