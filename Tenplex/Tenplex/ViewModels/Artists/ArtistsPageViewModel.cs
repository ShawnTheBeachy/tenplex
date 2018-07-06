using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Services.Serialization;
using Tenplex.Models;
using Tenplex.Services;
using Tenplex.Views;

namespace Tenplex.ViewModels
{
    public class ArtistsPageViewModel : ViewModelBase
    {
        private readonly ArtistsService _artistsService;
        private INavigationService _navigationService;
        private readonly ISerializationService _serializationService;

        public ObservableCollection<Artist> Artists { get; set; } = new ObservableCollection<Artist>();
        public string SectionKey = default(string);

        public ArtistsPageViewModel(ArtistsService artistsService, ISerializationService serializationService)
        {
            _artistsService = artistsService ?? throw new ArgumentNullException(nameof(artistsService));
            _serializationService = serializationService ?? throw new ArgumentNullException(nameof(serializationService));
            Artists = _artistsService.Artists;
        }

        public async override Task OnNavigatedToAsync(INavigationParameters parameters)
        {
            _navigationService = parameters.GetNavigationService();
            var sectionKey = SectionKey;

            if (parameters.ContainsKey("sectionKey"))
                sectionKey = parameters.GetValue<string>("sectionKey");

            if (Artists.Count == 0 || sectionKey != SectionKey)
            {
                _artistsService.Artists.Clear();
                await _artistsService.LoadArtistsAsync(sectionKey);
            }

            SectionKey = sectionKey;
        }

        public async Task SelectArtistAsync(Artist artist)
        {
            var path = PathBuilder.Create(nameof(ArtistPage), ("artist", _serializationService.Serialize(artist))).ToString();
            await _navigationService.NavigateAsync(path);
        }
    }
}
