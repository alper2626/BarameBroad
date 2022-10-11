using System.Net;

namespace BaseEntities.Response
{
    public class DataResponse
    {
        public object Data { get; set; }

        public bool Success { get; set; }

        public string Message { get; set; }

        public HttpStatusCode StatusCode { get; set; }
    }
}