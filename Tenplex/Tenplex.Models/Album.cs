using Newtonsoft.Json;

namespace Tenplex.Models
{
    /// <summary>
    /// A music album.
    /// </summary>
    [JsonObject(MemberSerialization.OptIn)]
    public class Album : BindableBase
    {
        #region Art

        private string _art = default(string);
        /// <summary>
        /// The artwork for this album.
        /// </summary>
        [JsonProperty("art")]
        public string Art { get => _art; set => Set(ref _art, value); }

        #endregion Art

        #region LibrarySectionId

        private int _librarySectionId;
        [JsonProperty("librarySectionID")]
        public int LibrarySectionId { get => _librarySectionId; set => Set(ref _librarySectionId, value); }

        #endregion LibrarySectionId

        #region ParentTitle

        private string _parentTitle = default(string);
        /// <summary>
        /// The title of this album's parent resource.
        /// </summary>
        [JsonProperty("parentTitle")]
        public string ParentTitle { get => _parentTitle; set => Set(ref _parentTitle, value); }

        #endregion ParentTitle

        #region RatingKey

        private string _ratingKey = default(string);
        /// <summary>
        /// The key used in API requests.
        /// </summary>
        [JsonProperty("ratingKey")]
        public string RatingKey { get => _ratingKey; set => Set(ref _ratingKey, value); }

        #endregion RatingKey

        #region Thumb

        private string _thumb = default(string);
        /// <summary>
        /// The thumbnail for this album.
        /// </summary>
        [JsonProperty("thumb")]
        public string Thumb { get => _thumb; set => Set(ref _thumb, value); }

        #endregion Thumb

        #region Title

        private string _title = default(string);
        /// <summary>
        /// The title of this album.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get => _title; set => Set(ref _title, value); }

        #endregion Title

        #region Year

        private int _year = default(int);
        /// <summary>
        /// The year in which this album was released.
        /// </summary>
        [JsonProperty("year")]
        public int Year { get => _year; set => Set(ref _year, value); }

        #endregion Year
    }
}
