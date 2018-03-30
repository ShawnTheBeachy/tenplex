using Prism.Unity;
using System;
using System.Net;
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

        public static ImageSource GetThumbUrl(string thumbnail, int width, int height)
        {
            if (string.IsNullOrWhiteSpace(thumbnail))
                return null;

            var authorizationService = Prism.PrismApplicationBase.Current.Container.Resolve<AuthorizationService>();
            var connectionsService = Prism.PrismApplicationBase.Current.Container.Resolve<ConnectionsService>();

            var bitmap = new BitmapImage(new Uri($"{connectionsService.CurrentConnection.Uri}/photo/:/transcode?width={width}&height={height}&minSize=1&url={WebUtility.UrlEncode($"{thumbnail}?X-Plex-Token={authorizationService.GetAccessToken()}")}&X-Plex-Token={authorizationService.GetAccessToken()}"));
            return bitmap;
        }
    }
}
