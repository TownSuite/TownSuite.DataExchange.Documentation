using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using TownSuite.DataExchange.ConsoleExample;


var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.Development.json", optional: true);

IConfiguration config = builder.Build();

// This example console application expects that the account has already been bootstrapped
// by TownSuite and an api account, access and refresh tokens have been given to the developer.
// Alternatively TownSuite has given a full account and the developer has bootstrapped an
// access and refresh tokens.

var authSettings = config.GetSection("Auth").Get<AuthSettings>();
var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Add("User-Agent",
    "TSX-console-example/1.0 Mozilla/5.0 (Macintosh; Intel Mac OS X 10.15; rv:102.0) Gecko/20100101 Firefox/102.0");

// Decode token and view contents such as expires in
if (!String.IsNullOrWhiteSpace(authSettings.AuthToken))
{
    var handler = new JwtSecurityTokenHandler();
    var jsonToken = handler.ReadToken(authSettings.AuthToken);
    var tokenS = jsonToken as JwtSecurityToken;
    Console.WriteLine(jsonToken.ToString());
}

// Try to fetch data from an endpoint
var client = new Client(httpClient);
var result = await client.Get(
    authSettings.BaseUrl,
    authSettings.AuthToken,
    $"/api/v1/Property/Assessment/{authSettings.ApiAccount}");
if (result.statusCode != 200)
{
    // Assume the auth token expired and use the refresh token to get a new one
    var refreshResult = await client.Get(
        authSettings.BaseUrl,
        authSettings.RefreshToken,
        $"/api/v1/Auth/Refresh");
    if (refreshResult.statusCode != 200)
    {
        Console.WriteLine("Something went wrong.");
        return;
    }
    Console.WriteLine(refreshResult.json);

    // Use the new auth token to fetch data from the endpoint
    var tokenResp = JsonExtensions.DeserializeFromJson<TokenResponse>(refreshResult.json);
    result = await client.Get(
    authSettings.BaseUrl,
    tokenResp.authToken,
    $"/api/v1/Property/Assessment/{authSettings.ApiAccount}");
}

Console.WriteLine(result.json);



