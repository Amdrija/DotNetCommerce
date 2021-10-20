using System.Net;

namespace Logeecom.DemoProjekat.PL.ViewModels.RepsonseModels
{
    public class ErrorResponse
    {
        public string Title { get; set; }

        public HttpStatusCode Status { get; set; }

        public string Message { get; set; }
    }
}
