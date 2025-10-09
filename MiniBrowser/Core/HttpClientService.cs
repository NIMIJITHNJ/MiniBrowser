using System.Net.Http;
using System.Threading.Tasks;

namespace MiniBrowser.Core
{
    public class HttpClientService
    {
        private static readonly HttpClient client = new HttpClient                  // To create one shared http client for the whole app
        {
            Timeout = System.TimeSpan.FromSeconds(15)                               // Stop waiting forever – cancel if no response within 15 sec
        };

        public async Task<HttpResult> GetAsync(string url)                          // Method to send GET request and return response details
        {
            var cleanurl = UrlTools.CleanUrl(url);                              

            using var req = new HttpRequestMessage(HttpMethod.Get, cleanurl);       // Preparing the GET request
            req.Headers.UserAgent.ParseAdd("MiniBrowser_1.0");                      // To identify the app when connecting to websites

            try
            {
                using var res = await client.SendAsync(req);                        // Send the request and wait for the response
                var body = await res.Content.ReadAsStringAsync();                   // Read the full page content as text

                return new HttpResult                                               // Returning everything neatly in our custom result object
                {
                    StatusCode = (int)res.StatusCode,
                    Reason = res.ReasonPhrase ?? "",
                    FinalUrl = res.RequestMessage!.RequestUri!.ToString(),
                    Body = body
                };
            }
            catch (HttpRequestException ex)                                         // For handling network errors (e.g. no internet connection, DNS failure, server not found)
            {
                return new HttpResult
                {
                    StatusCode = 0,
                    Reason = $"Network error: {ex.Message}",
                    FinalUrl = cleanurl,
                    Body = ""
                };
            }
            catch (TaskCanceledException)                                           // For handling request timeout
            {
                return new HttpResult
                {
                    StatusCode = 0,
                    Reason = "Request timed out",
                    FinalUrl = cleanurl,
                    Body = ""
                };
            }
            catch (Exception ex)                                                    // For handling any other unexpected errors
            {
                return new HttpResult
                {
                    StatusCode = 0,
                    Reason = $"Unexpected error: {ex.Message}",
                    FinalUrl = cleanurl,
                    Body = ""
                };
            }
        }
    }
}
