using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;

namespace Tenplex.Views
{
    public sealed partial class SplashPage : Page
    {
        public SplashPage(Windows.ApplicationModel.Activation.SplashScreen splashScree)
        {
            InitializeComponent();
            Window.Current.Activate();
        }
    }
}
