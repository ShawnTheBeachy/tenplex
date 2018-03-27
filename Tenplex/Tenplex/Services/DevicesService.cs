using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Template10.Utilities;
using Tenplex.Models;

namespace Tenplex.Services
{
    public class DevicesService
    {
        private readonly ServersService _serversService;
        private readonly UsersService _usersService;

        public Device CurrentDevice { get; set; }
        public ObservableCollection<Device> Devices { get; } = new ObservableCollection<Device>();

        public DevicesService(ServersService serversService, UsersService usersService)
        {
            _serversService = serversService ?? throw new ArgumentNullException(nameof(serversService));
            _usersService = usersService ?? throw new ArgumentNullException(nameof(usersService));
        }

        public IEnumerable<Device> GetServers()
        {
            return Devices.Where(device => device.Provides.Split(',').Contains("server"));
        }
        
        public async Task InitializeAsync()
        {
            await LoadAvailableDevicesAsync();
            CurrentDevice = Devices.FirstOrDefault(device => device.ClientIdentifier == _serversService.CurrentServer.MachineIdentifier);
        }

        public async Task<IEnumerable<Device>> LoadAvailableDevicesAsync()
        {
            using (var client = new HttpClient())
            {
                client.DefaultRequestHeaders.Add("X-Plex-Client-Identifier", "tenplex");
                client.DefaultRequestHeaders.Add("X-Plex-Product", "Tenplex");
                client.DefaultRequestHeaders.Add("X-Plex-Token", _usersService.CurrentUser.AuthToken);
                client.DefaultRequestHeaders.Add("X-Plex-Version", "1.0.0");

                var response = await client.GetAsync(new Uri("https://plex.tv/api/resources?includeHttps=1"));
                var content = await response.Content.ReadAsStringAsync();

                if (!response.IsSuccessStatusCode)
                    return null;
                else
                {
                    var reader = new StringReader(content);
                    var deserializer = new XmlSerializer(typeof(List<Device>), new XmlRootAttribute("MediaContainer"));
                    var devices = (List<Device>)deserializer.Deserialize(reader);
                    Devices.AddRange(devices, true);
                    return devices;
                }
            }
        }
    }
}
