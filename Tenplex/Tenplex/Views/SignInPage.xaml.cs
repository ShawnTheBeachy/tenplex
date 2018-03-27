using Tenplex.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Tenplex.Views
{
    public sealed partial class SignInPage : Page
    {
        private SignInPageViewModel ViewModel => DataContext as SignInPageViewModel;

        public SignInPage()
        {
            InitializeComponent();
        }
    }
}
