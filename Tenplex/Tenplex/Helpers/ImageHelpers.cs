using Prism.Unity;
using System;
using Tenplex.Services;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;

namespace Tenplex.Helpers
{
    public static class ImageHelpers
    {
        public static ImageSource GetThumbUrl(string thumbnail)
        {
            if (string.IsNullOrWhiteSpace(thumbnail))
                return null;

            var authorizationService = Prism.PrismApplicationBase.Current.Container.Resolve<AuthorizationService>();
            var connectionsService = Prism.PrismApplicationBase.Current.Container.Resolve<ConnectionsService>();

            var bitmap = new BitmapImage(new Uri($"{connectionsService.CurrentConnection.Uri}{thumbnail}?X-Plex-Token={authorizationService.GetAccessToken()}"));
            return bitmap;
        }
    }
}
