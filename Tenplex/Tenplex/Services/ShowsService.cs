using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using Template10.Services.Web;
using Template10.Utilities;
using Tenplex.Models;

namespace Tenplex.Services
{
    public sealed class ShowsService
    {
        private readonly ConnectionsService _connectionsService;
        private readonly IWebApiService _webApiService;

        public ObservableCollection<Show> Shows { get; } = new ObservableCollection<Show>();

        public ShowsService(ConnectionsService connectionsService, IWebApiService webApiService)
        {
            _connectionsService = connectionsService ?? throw new ArgumentNullException(nameof(connectionsService));
            _webApiService = webApiService ?? throw new ArgumentNullException(nameof(webApiService));
        }

        public async Task LoadShowsAsync(string sectionKey)
        {
            var url = $"{_connectionsService.CurrentConnection.Uri}/library/sections/{sectionKey}/all";
            var result = await _webApiService.GetAsync(new Uri(url));

            var jObj = JObject.Parse(result);

            try
            {
                var directory = jObj.SelectToken("MediaContainer.Metadata");
                var shows = directory.ToObject<IEnumerable<Show>>();
                Shows.AddRange(shows, true);
            }

            catch
            {
                // This can happen if there are no shows; the Metadata token won't exist.
                Shows.Clear();
            }
        }
    }
}
