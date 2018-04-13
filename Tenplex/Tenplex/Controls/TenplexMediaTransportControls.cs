using Windows.UI.Xaml.Controls;

namespace Tenplex.Controls
{
    public sealed class TenplexMediaTransportControls : MediaTransportControls
    {
        private TextBlock _artist;
        private Button _clearButton;
        private Button _currentItemButton;
        private Image _posterImage;
        private TextBlock _title;

        protected override void OnApplyTemplate()
        {
            base.OnApplyTemplate();

            _artist = GetTemplateChild("CurrentItemArtist") as TextBlock;
            _clearButton = GetTemplateChild("ClearButton") as Button;
            _currentItemButton = GetTemplateChild("CurrentItemButton") as Button;
            _posterImage = GetTemplateChild("PosterImage") as Image;
            _title = GetTemplateChild("CurrentItemTitle") as TextBlock;
        }
    }
}
