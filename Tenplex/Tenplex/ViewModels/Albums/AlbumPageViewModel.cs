using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.Serialization;
using Tenplex.Comparers;
using Tenplex.Controls;
using Tenplex.Models;
using Tenplex.Services;
using Tenplex.Views;

namespace Tenplex.ViewModels
{
    public class AlbumPageViewModel : ViewModelBase
    {
        private readonly AuthorizationService _authorizationService;
        private readonly ConnectionsService _connectionsService;
        private ISerializationService _serializationService;
        private readonly ShellPage _shell;
        private readonly TracksService _tracksService;

        #region Album

        private Album _album = default(Album);
        public Album Album { get => _album; set => SetProperty(ref _album, value); }

        #endregion Album

        #region Tracks

        private GroupedObservableCollection<string, Track> _tracks = new GroupedObservableCollection<string, Track>((track) => $"Disc {track.Disc}", new Track[] { }, (track) => track.TrackNumber);
        public GroupedObservableCollection<string, Track> Tracks => _tracks;

        #endregion Tracks

        public AlbumPageViewModel(AuthorizationService authorizationService, ConnectionsService connectionsService, ISerializationService serializationService, ShellPage shell, TracksService tracksService)
        {
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            _connectionsService = connectionsService ?? throw new ArgumentNullException(nameof(connectionsService));
            _serializationService = serializationService ?? throw new ArgumentNullException(nameof(serializationService));
            _shell = shell ?? throw new ArgumentNullException(nameof(shell));
            _tracksService = tracksService ?? throw new ArgumentNullException(nameof(tracksService));
        }

        public async override Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            var serializedAlbum = parameters.GetValue<string>("album");
            Album = _serializationService.Deserialize<Album>(serializedAlbum);

            var tracks = await _tracksService.GetTracksAsync(Album.RatingKey);
            Tracks.ReplaceWith(new GroupedObservableCollection<string, Track>((track) => $"Disc {track.Disc}", tracks, (track) => track.TrackNumber), new TrackComparer());
        }

        public void PlayTrack(Track track)
        {
            var tracks = Tracks.AllItems.ToList();
            _shell.ClearQueue();
            _shell.AddToQueue(tracks.Skip(tracks.IndexOf(track))
                .Take(tracks.Count)
                .Select(t => new PlaybackItem
                {
                    Artist = Album.ParentTitle,
                    PosterSource = $"{_connectionsService.CurrentConnection.Uri}{Album.Thumb}?X-Plex-Token={_authorizationService.GetAccessToken()}",
                    Source = $"{_connectionsService.CurrentConnection.Uri}{t.Media.First().Parts.First().Key}?X-Plex-Token={_authorizationService.GetAccessToken()}",
                    Title = t.Title
                }));
            _shell.Play();
        }
    }
}
