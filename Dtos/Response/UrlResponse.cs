namespace UrlShortener.Dtos.Response
{
    public record UrlResponse
    {
        public string Url { get; set; } = string.Empty;
        public bool Status { get; set; }
        public string Message { get; set; } = string.Empty;
    }
}
