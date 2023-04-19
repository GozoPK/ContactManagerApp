using System.Text.Json;

namespace ContactManager.Extensions
{
    public static class HttpExtensions
    {
        public static async Task<T> ReadResponse<T>(this HttpResponseMessage response)
        {
            if (!response.IsSuccessStatusCode)
            {
                throw new Exception($"Something went wrong - {response.ReasonPhrase}");
            }

            var contentAsString = await response.Content.ReadAsStringAsync();

            var options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };

            return JsonSerializer.Deserialize<T>(contentAsString, options);
        }
    }
}
