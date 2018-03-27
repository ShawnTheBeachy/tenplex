using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Template10.Services.Web;
using Tenplex.Models;

namespace Tenplex.Services
{
    public sealed class TracksService
    {
        private readonly ConnectionsService _connectionsService;
        private readonly IWebApiService _webApiService;
        
        public TracksService(ConnectionsService connectionsService, IWebApiService webApiService)
        {
            _connectionsService = connectionsService ?? throw new ArgumentNullException(nameof(connectionsService));
            _webApiService = webApiService ?? throw new ArgumentNullException(nameof(webApiService));
        }

        public async Task<IEnumerable<Track>> GetTracksAsync(string albumRatingKey)
        {
            // this._webApiService.AddHeader("Accept", "application/json");

            var url = $"{_connectionsService.CurrentConnection.Uri}/library/metadata/{albumRatingKey}/children";
            var result = await _webApiService.GetAsync(new Uri(url));

            var jObj = JObject.Parse(result);
            var directory = jObj.SelectToken("MediaContainer.Metadata");
            var tracks = directory.ToObject<IEnumerable<Track>>();
            return tracks;
        }
    }
}
