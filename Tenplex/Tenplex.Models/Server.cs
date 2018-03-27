using System.Xml.Serialization;

namespace Tenplex.Models
{
    [XmlType("Server")]
    public class Server : BindableBase
    {
        #region AccessToken

        private string _accessToken;
        [XmlAttribute("accessToken")]
        public string AccessToken { get => _accessToken; set => Set(ref _accessToken, value); }

        #endregion AccessToken

        #region Address

        private string _address;
        [XmlAttribute("address")]
        public string Address { get => _address; set => Set(ref _address, value); }

        #endregion Address

        #region MachineIdentifier

        private string _machineIdentifier;
        [XmlAttribute("machineIdentifier")]
        public string MachineIdentifier { get => _machineIdentifier; set => Set(ref _machineIdentifier, value); }

        #endregion MachineIdentifier

        #region Name

        private string _name;
        [XmlAttribute("name")]
        public string Name { get => _name; set => Set(ref _name, value); }

        #endregion Name

        #region Port

        private string _port;
        [XmlAttribute("port")]
        public string Port { get => _port; set => Set(ref _port, value); }

        #endregion Port

        #region Scheme

        private string _scheme;
        [XmlAttribute("scheme")]
        public string Scheme { get => _scheme; set => Set(ref _scheme, value); }

        #endregion Scheme

        public string Url => $"{Scheme}://{Address}:{Port}";

        #region Version

        private string _version;
        [XmlAttribute("version")]
        public string Version { get => _version; set => Set(ref _version, value); }

        #endregion Version
    }
}
