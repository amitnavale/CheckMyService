namespace MI9.CodingChallange.Models
{
    public class ErrorResponse
    {
        public string Error { get; set; }
    }

    public class FilteredEpisodeInfo
    {
        public string Image { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }

    }
    public class FilteredEpisodeInfoWrapper
    {
        public FilteredEpisodeInfo[] Response;
    }
    public class EpisodeInfoWrapper
    {
        public EpisodeInfo[] Payload;
        public int Skip { get; set; }
        public int Take { get; set; }
        public int TotalRecords { get; set; }
    }
    public class EpisodeInfo
    {
        public string Country { get; set; }
        public string Description { get; set; }
        public bool Drm { get; set; }
        public int EpisodeCount { get; set; }
        public string Genre { get; set; }
        public ImageUrl Image{ get; set; }
        public string Language { get; set; }
        public Episode NextEpisode { get; set; }
        public string PrimaryColour { get; set; }
        public Season[] Seasons { get; set; }
        public string Slug { get; set; }
        public string Title { get; set; }
        public string TvChannel { get; set; }
    }

    public class Episode
    {
        public string Channel { get; set; }
        public string ChannelLogo { get; set; }
        public string Date { get; set; }
        public string Html { get; set; }
        public string Url  { get; set; }
    }

    public class Season
    {
        public string Slug { get; set; }
    }

    public class ImageUrl
    {
        public string ShowImage { get; set; }
    }
}