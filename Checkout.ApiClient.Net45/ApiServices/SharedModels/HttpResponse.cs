using System.Net;

namespace Checkout.ApiServices.SharedModels
{
    using System.Net.Http.Headers;

    /// <summary>
    /// Holds the response model
    /// </summary>
    /// <typeparam name="T">generic model returned from the api</typeparam>
    public class HttpResponse<T>
    {
        public HttpResponseHeaders Headers { get; set; }
        public bool HasError { get { return Error != null; } }
        public HttpStatusCode HttpStatusCode { get; set; }
        public ResponseError Error { get; set; }
        public T Model { get; set; }

        public HttpResponse(T model)
        {
            this.Model = model;
        }
    }
}