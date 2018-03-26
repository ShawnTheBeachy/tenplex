using Tenplex.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Tenplex.Views
{
    public sealed partial class ConnectionInfoPage : Page
    {
        private ConnectionInfoPageViewModel ViewModel =>
            this.DataContext as ConnectionInfoPageViewModel;

        public ConnectionInfoPage()
        {
            InitializeComponent();
        }
    }
}
