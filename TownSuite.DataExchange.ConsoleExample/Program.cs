using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;
using TownSuite.DataExchange.ConsoleExample;


var builder = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json", optional: false)
    .AddJsonFile("appsettings.Development.json", optional: true);

IConfiguration config = builder.Build();

// This example console application expects that the account has already been bootstrapped
// by TownSuite and an access and refresh token has been given to the developer.
// Alternatively TownSuite has given a full account and the developer has bootstrapped an
// access and refresh tokens.

var authSettings = config.GetSection("Auth").Get<AuthSettings>();
var httpClient = new HttpClient();
httpClient.DefaultRequestHeaders.Add("User-Agent",
    "TSX-console-example/1.0 Mozilla/5.0 (Macintosh; Intel Mac OS X 10.15; rv:102.0) Gecko/20100101 Firefox/102.0");

// Decode token and view contents such as expires in
var handler = new JwtSecurityTokenHandler();
var jsonToken = handler.ReadToken(authSettings.AuthToken);
var tokenS = jsonToken as JwtSecurityToken;
Console.WriteLine(jsonToken.ToString());


using var request = new HttpRequestMessage()
{
    Method = HttpMethod.Get,
    RequestUri = new Uri(Utils.MakeSafeUrl(authSettings.BaseUrl, $"/api/v1/Property/Assessment/{authSettings.ApiAccount}"))
};
request.Headers.Add("Authorization", $"Bearer {authSettings.AuthToken}");

HttpResponseMessage response = await httpClient.SendAsync(request).ConfigureAwait(false);

var content = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
Console.WriteLine(content);


// TODO:  Show auth refresh


