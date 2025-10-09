namespace MiniBrowser.Core
{    
    public class HttpResult                         // Holds details of an HTTP response
    {
        public int StatusCode { get; set; }
        public string Reason { get; set; } = "";
        public string FinalUrl { get; set; } = "";
        public string Body { get; set; } = "";
    }
}
