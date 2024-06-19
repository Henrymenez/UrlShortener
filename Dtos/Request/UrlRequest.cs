namespace UrlShortener.Dtos.Request;

public record UrlRequest
{
  
    public string Url { get; set; } = string.Empty;
}
