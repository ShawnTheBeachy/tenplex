using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Template10.Services.Serialization;
using Template10.Utilities;
using Tenplex.Comparers;
using Tenplex.Controls;
using Tenplex.Models;
using Tenplex.Services;
using Tenplex.Views;

namespace Tenplex.ViewModels
{
    public class AlbumPageViewModel : ViewModelBase
    {
        private readonly AlbumsService _albumsService;
        private readonly AuthorizationService _authorizationService;
        private readonly ConnectionsService _connectionsService;
        private ISerializationService _serializationService;
        private readonly ShellPage _shell;
        private readonly TracksService _tracksService;

        #region Album

        private Album _album = new Album();
        public Album Album { get => _album; set => SetProperty(ref _album, value); }

        #endregion Album

        #region Posters

        private ObservableCollection<Poster> _posters = new ObservableCollection<Poster>();
        public ObservableCollection<Poster> Posters => _posters;

        #endregion Posters

        #region Properties

        private AlbumProperties _properties = new AlbumProperties();
        public AlbumProperties Properties { get => _properties; set => SetProperty(ref _properties, value); }

        #endregion Properties
        
        #region Tracks

        private GroupedObservableCollection<string, Track> _tracks = new GroupedObservableCollection<string, Track>((track) => $"Disc {track.Disc}", new Track[] { }, (track) => track.TrackNumber);
        public GroupedObservableCollection<string, Track> Tracks => _tracks;

        #endregion Tracks

        public AlbumPageViewModel(AlbumsService albumsService, AuthorizationService authorizationService, ConnectionsService connectionsService, 
            ISerializationService serializationService, ShellPage shell, TracksService tracksService)
        {
            _albumsService = albumsService ?? throw new ArgumentNullException(nameof(albumsService));
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            _connectionsService = connectionsService ?? throw new ArgumentNullException(nameof(connectionsService));
            _serializationService = serializationService ?? throw new ArgumentNullException(nameof(serializationService));
            _shell = shell ?? throw new ArgumentNullException(nameof(shell));
            _tracksService = tracksService ?? throw new ArgumentNullException(nameof(tracksService));
        }

        public async override Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            var ratingKey = parameters.GetValue<string>("ratingKey");
            Album = await _albumsService.GetAlbumAsync(ratingKey);
            var tracks = await _tracksService.GetTracksAsync(Album.RatingKey);
            Tracks.ReplaceWith(new GroupedObservableCollection<string, Track>((track) => $"Disc {track.Disc}", tracks, (track) => track.TrackNumber), new TrackComparer());
        }

        public async Task EditAlbumAsync()
        {
            Properties = await _albumsService.GetAlbumPropertiesAsync(Album);
            Properties.StartTracking();
        }

        public async Task LoadPostersAsync()
        {
            Posters.Clear();
            Posters.AddRange(await _albumsService.GetPostersAsync(Album));
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

        public async Task UpdateAlbumAsync()
        {
            await _albumsService.UpdateAlbumPropertiesAsync(Album, Properties);

            if (Posters.Any(poster => poster.IsSelected))
                await _albumsService.SetPosterAsync(Album, Posters.Single(poster => poster.IsSelected));

            Album = await _albumsService.GetAlbumAsync(Album.RatingKey);
        }
    }
}
