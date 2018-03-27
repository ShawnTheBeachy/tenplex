using System;
using System.Threading.Tasks;
using Tenplex.Models;
using Windows.Web.Http;

namespace Tenplex.Services
{
    public class ConnectionsService
    {
        private readonly DevicesService _devicesService;

        public Connection CurrentConnection { get; set; }

        public ConnectionsService(DevicesService devicesService)
        {
            _devicesService = devicesService ?? throw new ArgumentNullException(nameof(devicesService));
        }

        public async Task InitializeAsync()
        {
            using (var client = new HttpClient())
            {
                foreach (var connection in _devicesService.CurrentDevice.Connections)
                {
                    try
                    {
                        var response = await client.GetAsync(new Uri(connection.Uri));
                        CurrentConnection = connection;
                        return;
                    }

                    catch
                    {
                        continue;
                    }
                }
            }
        }
    }
}
