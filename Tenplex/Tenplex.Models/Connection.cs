using System.Xml.Serialization;

namespace Tenplex.Models
{
    [XmlType("Connection")]
    public class Connection : BindableBase
    {
        #region Uri

        private string _uri;
        [XmlAttribute("uri")]
        public string Uri { get => _uri; set => Set(ref _uri, value); }

        #endregion Uri
    }
}
