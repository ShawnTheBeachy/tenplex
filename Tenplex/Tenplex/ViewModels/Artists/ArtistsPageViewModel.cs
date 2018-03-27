using Prism.Mvvm;
using Prism.Navigation;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Tenplex.Models;
using Tenplex.Services;

namespace Tenplex.ViewModels
{
    public class ArtistsPageViewModel : ViewModelBase
    {
        private readonly ArtistsService _artistsService;

        public ObservableCollection<Artist> Artists { get; set; } = new ObservableCollection<Artist>();
        public string SectionKey = default(string);

        public ArtistsPageViewModel(ArtistsService artistsService)
        {
            _artistsService = artistsService ?? throw new ArgumentNullException(nameof(artistsService));
            Artists = _artistsService.Artists;
        }

        public async override Task OnNavigatedToAsync(INavigationParameters parameters)
        {
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
    }
}
