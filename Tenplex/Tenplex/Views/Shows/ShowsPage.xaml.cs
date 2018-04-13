using Tenplex.ViewModels;
using Windows.UI.Xaml.Controls;

namespace Tenplex.Views
{
    public sealed partial class ShowsPage : Page
    {
        private ShowsPageViewModel ViewModel => DataContext as ShowsPageViewModel;

        public ShowsPage()
        {
            InitializeComponent();
        }
    }
}
