namespace Tenplex.Models
{
    public class PlaybackItem : BindableBase
    {
        #region Artist

        private string _artist;
        public string Artist { get => _artist; set => Set(ref _artist, value); }

        #endregion Artist

        #region PosterSource

        private string _posterSource;
        public string PosterSource { get => _posterSource; set => Set(ref _posterSource, value); }

        #endregion PosterSource

        #region Source

        private string _source;
        public string Source { get => _source; set => Set(ref _source, value); }

        #endregion Source

        #region Title

        private string _title;
        public string Title { get => _title; set => Set(ref _title, value); }

        #endregion Title
    }
}
