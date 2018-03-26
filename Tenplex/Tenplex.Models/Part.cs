using Newtonsoft.Json;

namespace Tenplex.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Part : BindableBase
    {
        #region Key

        private string _key = default(string);
        /// <summary>
        /// The key for this part.
        /// </summary>
        [JsonProperty("key")]
        public string Key { get => _key; set => Set(ref _key, value); }

        #endregion Key
    }
}
