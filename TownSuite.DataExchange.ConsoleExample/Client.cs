namespace TownSuite.DataExchange.ConsoleExample;

public class Client
{
    private readonly HttpClient _httpClient;

    public Client(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    public async Task<(string json, int statusCode)> Get(
        string baseUrl,
        string authToken,
        string endpoint)
    {
        using var request = new HttpRequestMessage()
        {
            Method = HttpMethod.Get,
            RequestUri = new Uri(Utils.MakeSafeUrl(baseUrl,
                endpoint))
        };
        request.Headers.Add("Authorization", $"Bearer {authToken}");

        HttpResponseMessage response = await _httpClient.SendAsync(request).ConfigureAwait(false);
        if ((int)response.StatusCode >= 300 || (int)response.StatusCode < 200)
        {
            // Assuming token has expired 
            return ("", (int)response.StatusCode);
        }

        var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
        return (content, (int)response.StatusCode);
    }
}