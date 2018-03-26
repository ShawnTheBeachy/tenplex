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
        private ISerializationService _serializationService;
        private readonly ServerConnectionInfoService _serverConnectionInfoService;
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

        public AlbumPageViewModel(ISerializationService serializationService, ServerConnectionInfoService serverConnectionInfoService, ShellPage shell, TracksService tracksService)
        {
            _serializationService = serializationService ?? throw new ArgumentNullException(nameof(serializationService));
            _serverConnectionInfoService = serverConnectionInfoService ?? throw new ArgumentNullException(nameof(serverConnectionInfoService));
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
            var media = track.Media.First();
            var part = media.Parts.First();
            var url = $"http://{_serverConnectionInfoService.GetServerIpAddress()}:{_serverConnectionInfoService.GetServerPortNumber()}{part.Key}?X-Plex-Token={_serverConnectionInfoService.GetPlexAccessToken()}";
            _shell.Play(url, $"http://{_serverConnectionInfoService.GetServerIpAddress()}:{_serverConnectionInfoService.GetServerPortNumber()}{Album.Thumb}?X-Plex-Token={_serverConnectionInfoService.GetPlexAccessToken()}");
        }
    }
}
