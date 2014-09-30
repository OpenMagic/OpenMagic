using System;
using System.Net;
using System.Net.Http;

namespace OpenMagic.Extensions
{
    public static class UriExtensions
    {
        private static HttpResponseMessage GetResponse(this Uri uri)
        {
            using (var httpClient = new HttpClient())
            {
                return httpClient.GetAsync(uri).Result;
            }
        }

        public static HttpStatusCode GetResponseStatusCode(this Uri uri)
        {
            using (var response = uri.GetResponse())
            {
                return response.StatusCode;
            }
        }

        public static bool ResponseIsSuccessStatusCode(this Uri uri)
        {
            try
            {
                using (var httpResponseMessage = uri.GetResponse())
                {
                    return httpResponseMessage.IsSuccessStatusCode;
                }
            }
            catch (AggregateException aggregateException)
            {
                if (aggregateException.InnerException.GetType() == typeof(HttpRequestException))
                {
                    return false;
                }
                throw;
            }
        }
    }
}
