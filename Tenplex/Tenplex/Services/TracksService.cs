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
        private readonly ServerConnectionInfoService _serverConnectionInfoService;
        private readonly IWebApiService _webApiService;
        
        public TracksService(ServerConnectionInfoService serverConnectionInfoService, IWebApiService webApiService)
        {
            _serverConnectionInfoService = serverConnectionInfoService ?? throw new ArgumentNullException(nameof(serverConnectionInfoService));
            _webApiService = webApiService ?? throw new ArgumentNullException(nameof(webApiService));
        }

        public async Task<IEnumerable<Track>> GetTracksAsync(string albumRatingKey)
        {
            // this._webApiService.AddHeader("Accept", "application/json");

            var url = $"http://{_serverConnectionInfoService.GetServerIpAddress()}:{_serverConnectionInfoService.GetServerPortNumber()}/library/metadata/{albumRatingKey}/children";
            var result = await _webApiService.GetAsync(new Uri(url));

            var jObj = JObject.Parse(result);
            var directory = jObj.SelectToken("MediaContainer.Metadata");
            var tracks = directory.ToObject<IEnumerable<Track>>();
            return tracks;
        }
    }
}
