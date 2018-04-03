using Newtonsoft.Json;

namespace Tenplex.Models
{
    /// <summary>
    /// A TV show.
    /// </summary>
    public sealed class Show : BindableBase
    {
        #region Art

        private string _art = default(string);
        /// <summary>
        /// Artwork for the show.
        /// </summary>
        [JsonProperty("art")]
        public string Art { get => _art; set => Set(ref _art, value); }

        #endregion Art

        #region ChildCount

        private int _childCount = default(int);
        /// <summary>
        /// The number of seasons in the show.
        /// </summary>
        [JsonProperty("childCount")]
        public int ChildCount { get => _childCount; set => Set(ref _childCount, value); }

        #endregion ChildCount

        #region Studio

        private string _studio = default(string);
        /// <summary>
        /// The studio which publishes the show.
        /// </summary>
        [JsonProperty("studio")]
        public string Studio { get => _studio; set => Set(ref _studio, value); }

        #endregion Studio

        #region Summary

        private string _summary = default(string);
        /// <summary>
        /// A summary of the show.
        /// </summary>
        [JsonProperty("summary")]
        public string Summary { get => _summary; set => Set(ref _summary, value); }

        #endregion Summary

        #region Thumb

        private string _thumb = default(string);
        /// <summary>
        /// A thumbnail for the show.
        /// </summary>
        [JsonProperty("thumb")]
        public string Thumb { get => _thumb; set => Set(ref _thumb, value); }

        #endregion Thumb

        #region Title

        private string _title = default(string);
        /// <summary>
        /// The title of the show.
        /// </summary>
        [JsonProperty("title")]
        public string Title { get => _title; set => Set(ref _title, value); }

        #endregion Title
    }
}
