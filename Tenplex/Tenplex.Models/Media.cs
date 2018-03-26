using Newtonsoft.Json;
using System.Collections.Generic;

namespace Tenplex.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Media : BindableBase
    {
        #region Parts

        private ICollection<Part> _parts = new HashSet<Part>();
        [JsonProperty("Part")]
        public ICollection<Part> Parts { get => _parts; set => Set(ref _parts, value); }

        #endregion Parts
    }
}
