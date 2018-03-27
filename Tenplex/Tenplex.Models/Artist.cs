using Newtonsoft.Json;

namespace Tenplex.Models
{
    [JsonObject(MemberSerialization.OptIn)]
    public class Artist : BindableBase
    {
        #region Thumb

        private string _thumb;
        [JsonProperty("thumb")]
        public string Thumb { get => _thumb; set => Set(ref _thumb, value); }

        #endregion Thumb

        #region Title

        private string _title;
        [JsonProperty("title")]
        public string Title { get => _title; set => Set(ref _title, value); }

        #endregion Title
    }
}
