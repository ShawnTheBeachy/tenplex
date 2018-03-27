using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Template10.Services.Settings;
using Tenplex.Models;
using Windows.Web.Http;

namespace Tenplex.Services
{
    public class ServersService
    {
        private readonly AuthorizationService _authorizationService;
        private readonly ISettingsHelper _settingsHelper;
        private List<Server> _servers = new List<Server>();

        public Server CurrentServer { get; set; }

        public ServersService(AuthorizationService authorizationService, ISettingsHelper settingsHelper)
        {
            _authorizationService = authorizationService ?? throw new ArgumentNullException(nameof(authorizationService));
            _settingsHelper = settingsHelper ?? throw new ArgumentNullException(nameof(settingsHelper));
        }

        public string GetDefaultServerId()
        {
            _settingsHelper.TryReadString("DEFAULT_SERVER_ID", out var id);
            return id;
        }

        public async Task InitializeAsync()
        {
            var defaultServerId = GetDefaultServerId();
            await LoadServersAsync();

            if (!string.IsNullOrWhiteSpace(defaultServerId))
                CurrentServer = _servers.FirstOrDefault(server => server.MachineIdentifier == defaultServerId);

            if (CurrentServer == null)
                CurrentServer = _servers.FirstOrDefault();
        }

        public async Task<IEnumerable<Server>> LoadServersAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Plex-Client-Identifier", "tenplex");
                client.DefaultRequestHeaders.Add("X-Plex-Product", "Tenplex");
                client.DefaultRequestHeaders.Add("X-Plex-Token", _authorizationService.GetAccessToken());
                client.DefaultRequestHeaders.Add("X-Plex-Version", "1.0.0");

                var response = await client.GetAsync(new Uri("https://plex.tv/pms/servers.xml"));
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return null;
                else
                {
                    var reader = new StringReader(content);
                    var deserializer = new XmlSerializer(typeof(List<Server>), new XmlRootAttribute("MediaContainer"));
                    _servers = (List<Server>)deserializer.Deserialize(reader);
                    return _servers;
                }
            }
        }

        public void SetDefaultServer(Server server)
        {
            _settingsHelper.WriteString("DEFAULT_SERVER_ID", server.MachineIdentifier);
        }
    }
}
