using System.Net.Http.Headers;
using System.Text.Json;

namespace GeekShopping.FrontEnd.Utils
{
    public static class HttpClientExtension
    {
        private static MediaTypeHeaderValue contentType = new MediaTypeHeaderValue("application/json");

        public static async Task<T> ReadContentAs<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
                throw new ApplicationException($"Something went wrong, calling api: {response.ReasonPhrase}");

            var dataAsString = await response.Content.ReadAsStringAsync().ConfigureAwait(false);

            if (response.Content.Headers.ContentType.ToString().Contains("application/json"))
            {
                return JsonSerializer.Deserialize<T>(dataAsString,
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
            }

            return (T)(object)dataAsString;

        }

        public static Task<HttpResponseMessage> PostAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsJsonString = JsonSerializer.Serialize(data);

            var content = new StringContent(dataAsJsonString);
            content.Headers.ContentType = contentType;

            return httpClient.PostAsync(url, content);
        }

        public static Task<HttpResponseMessage> PutAsJson<T>(this HttpClient httpClient, string url, T data)
        {
            var dataAsJsonString = JsonSerializer.Serialize(data);

            var content = new StringContent(dataAsJsonString);
            content.Headers.ContentType = contentType;

            return httpClient.PutAsync(url, content);
        }
    }
}
