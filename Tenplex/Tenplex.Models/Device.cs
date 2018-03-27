using System.Collections.Generic;
using System.Xml.Serialization;

namespace Tenplex.Models
{
    [XmlRoot("Device")]
    public class Device : BindableBase
    {
        #region AccessToken

        private string _accessToken;
        [XmlAttribute("accessToken")]
        public string AccessToken { get => _accessToken; set => Set(ref _accessToken, value); }

        #endregion AccessToken

        #region ClientIdentifier

        private string _clientIdentifier;
        [XmlAttribute("clientIdentifier")]
        public string ClientIdentifier { get => _clientIdentifier; set => Set(ref _clientIdentifier, value); }

        #endregion ClientIdentifier

        #region Connections

        private List<Connection> _connections = new List<Connection>();
        [XmlElement("Connection")]
        public List<Connection> Connections { get => _connections; set => Set(ref _connections, value); }

        #endregion Connections

        #region Name

        private string _name;
        [XmlAttribute("name")]
        public string Name { get => _name; set => Set(ref _name, value); }

        #endregion Name

        #region Provides

        private string _provides;
        [XmlAttribute("provides")]
        public string Provides { get => _provides; set => Set(ref _provides, value); }

        #endregion Provides
    }
}
