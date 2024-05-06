using System.Text.Json;

namespace OrderService.IntegrationTests;

public static class HttpResponseMessageExtensions
{
    public static async Task<T> ReadAsJson<T>(this HttpResponseMessage httpResponseMessage)
    {
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content, 
            new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
    }
}