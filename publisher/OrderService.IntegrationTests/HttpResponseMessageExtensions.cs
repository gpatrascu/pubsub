using System.Text.Json;

namespace OrderService.IntegrationTests;

public static class HttpResponseMessageExtensions
{
    private static readonly JsonSerializerOptions JsonSerializerOptions =
        new() { PropertyNameCaseInsensitive = true };

    public static async Task<T> ReadAsJson<T>(this HttpResponseMessage httpResponseMessage)
    {
        var content = await httpResponseMessage.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<T>(content,
            JsonSerializerOptions);
    }
}
