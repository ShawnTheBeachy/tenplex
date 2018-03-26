using Newtonsoft.Json;

namespace Tenplex.Models
{
    /// <summary>
    /// An on-disk location for media.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Location : BindableBase
    {
        #region Id

        private int _id = default(int);
        /// <summary>
        /// The unique identifier of this location.
        /// </summary>
        [JsonProperty("id")]
        public int Id { get => _id; set => Set(ref _id, value); }

        #endregion Id

        #region Path

        private string _path = default(string);
        /// <summary>
        /// The path to this location on-disk.
        /// </summary>
        [JsonProperty("path")]
        public string Path { get => _path; set => Set(ref _path, value); }

        #endregion Path
    }
}
