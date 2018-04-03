using ColorThiefDotNet;
using Prism.Navigation;
using Prism.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using Template10.Controls;
using Tenplex.Models;
using Tenplex.Services;
using Windows.ApplicationModel.Core;
using Windows.Graphics.Imaging;
using Windows.Media.Core;
using Windows.Media.Playback;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media.Imaging;
using Windows.Web.Http;

namespace Tenplex.Views
{
    public sealed partial class ShellPage : Page
    {
        private readonly AuthorizationService _authorizationService;
        private readonly ConnectionsService _connectionsService;
        private readonly DevicesService _devicesService;
        private readonly IGestureService _gestureService;
        private readonly LibrarySectionsService _librarySectionsService;
        private readonly MediaPlaybackList _mediaPlaybackList;
        
        public ObservableCollection<Device> Devices { get; set; }

        public ShellPage(AuthorizationService authorizationService, ConnectionsService connectionsService, DevicesService devicesService, LibrarySectionsService librarySectionsService)
        {
            InitializeComponent();

            _gestureService = GestureService.GetForCurrentView();
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            _connectionsService = connectionsService ?? throw new ArgumentNullException(nameof(connectionsService));
            _devicesService = devicesService ?? throw new ArgumentNullException(nameof(devicesService));
            _librarySectionsService = librarySectionsService ?? throw new ArgumentNullException(nameof(librarySectionsService));

            _mediaPlaybackList = new MediaPlaybackList
            {
                // ShuffleEnabled = true
            };
            Player.Source = _mediaPlaybackList;

            ShellView.Initialize();
            ShellView.Loaded += (s, e) =>
            {
                SetupGestures();
            };

            Devices = _devicesService.Devices;
            ServerMenuItem.Content = _devicesService.CurrentDevice?.Name;

            foreach (var device in _devicesService.GetServers())
            {
                var item = new MenuFlyoutItem { DataContext = device, Text = device.Name };
                item.Click += DeviceMenuItem_Clicked;
                DevicesFlyout.Items.Add(item);
            }

            _mediaPlaybackList.CurrentItemChanged += async (s, e) =>
            {
                await CoreApplication.MainView.CoreWindow.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, async () =>
                {
                    if (s.CurrentItem == null)
                    {
                        PosterImage.Source = null;
                    }

                    else
                    {
                        var props = s.CurrentItem.GetDisplayProperties();
                        CurrentItemTitle.Text = props.MusicProperties.Title;
                        CurrentItemArtist.Text = props.MusicProperties.Artist;

                        using (var streamRef = await props.Thumbnail.OpenReadAsync())
                        {
                            var stream = streamRef.AsStream().AsRandomAccessStream();
                            var bitmap = new BitmapImage();
                            bitmap.SetSource(stream);
                            PosterImage.Source = bitmap;

                            var decoder = await BitmapDecoder.CreateAsync(stream);
                            var colorThief = new ColorThief();
                            var color = await colorThief.GetColor(decoder);
                            TransportControlsBackgroundBrush.TintColor = Windows.UI.Color.FromArgb(color.Color.A, color.Color.R, color.Color.G, color.Color.B);
                        }
                    }
                });
            };
        }

        public void AddToQueue(params PlaybackItem[] items)
        {
            AddToQueue(items);
        }

        public async void AddToQueue(IEnumerable<PlaybackItem> items)
        {
            foreach (var item in items)
            {
                var source = MediaSource.CreateFromUri(new Uri(item.Source));
                var playbackItem = new MediaPlaybackItem(source);

                var props = playbackItem.GetDisplayProperties();
                props.Type = Windows.Media.MediaPlaybackType.Music;

                using (var client = new HttpClient())
                {
                    var response = await client.GetAsync(new Uri(item.PosterSource));

                    var stream = new InMemoryRandomAccessStream();
                    await response.Content.WriteToStreamAsync(stream);
                    props.Thumbnail = RandomAccessStreamReference.CreateFromStream(stream);
                }

                props.MusicProperties.Title = item.Title;
                props.MusicProperties.Artist = item.Artist;
                // props.MusicProperties.Genres.Add("Polka");
                playbackItem.ApplyDisplayProperties(props);

                _mediaPlaybackList.Items.Add(playbackItem);
            }
            PlayerGrid.Visibility = Visibility.Visible;

        }

        private void ClearButton_Click(object sender, RoutedEventArgs e)
        {
            ClearQueue();
        }

        public void ClearQueue()
        {
            _mediaPlaybackList.Items.Clear();
            PlayerGrid.Visibility = Visibility.Collapsed;
        }

        private async void DeviceMenuItem_Clicked(object sender, RoutedEventArgs e)
        {
            var device = (sender as FrameworkElement).DataContext as Device;
            _devicesService.CurrentDevice = device;
            await _connectionsService.InitializeAsync();
            await _librarySectionsService.InitializeAsync();
        }

        public static IList<NavigationViewItem> GetLibrarySections(ObservableCollection<LibrarySection> sections)
        {
            var items = new List<NavigationViewItem>();

            foreach (var section in sections)
            {
                var item = new NavigationViewItem
                {
                    Content = section.Title
                };
                var pageName = section.Type == LibrarySectionType.Artist ? "MusicPage" : "ShowsPage";
                NavViewProps.SetNavigationUri(item, PathBuilder.Create(pageName, ("sectionKey", section.Key)).ToString());
                items.Add(item);
            }

            return items;
        }

        public void Play()
        {
            Player.MediaPlayer.Play();
        }
        
        private void ServerMenuItem_Tapped(object sender, TappedRoutedEventArgs e)
        {
            FlyoutBase.ShowAttachedFlyout(sender as FrameworkElement);
        }

        private void SetupGestures()
        {
            _gestureService.BackRequested += async (s, e) => await ShellView.NavigationService.GoBackAsync();
            _gestureService.ForwardRequested += async (s, e) => await ShellView.NavigationService.GoForwardAsync();
            _gestureService.RefreshRequested += async (s, e) => await ShellView.NavigationService.RefreshAsync();
            _gestureService.MenuRequested += (s, e) => ShellView.IsPaneOpen = true;
            _gestureService.SearchRequested += (s, e) =>
            {
                ShellView.IsPaneOpen = true;
                ShellView.AutoSuggestBox?.Focus(FocusState.Programmatic);
            };
        }
    }
}
