namespace UrlShortener.Utilities
{
    public static  class Utils
    {
        public static string GenerateShortUrl(int length)
        {
            var chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public static bool IsValidUrl(string url)
        {
            if (string.IsNullOrWhiteSpace(url))
            {
                return false;
            }

            // Check if the URL is well-formed and absolute
            if (Uri.IsWellFormedUriString(url, UriKind.Absolute))
            {
                // Try to create a Uri object
                return Uri.TryCreate(url, UriKind.Absolute, out Uri uriResult)
                       && (uriResult.Scheme == Uri.UriSchemeHttp || uriResult.Scheme == Uri.UriSchemeHttps);
            }

            return false;
        }
    }
}
