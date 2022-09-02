namespace TownSuite.DataExchange.ConsoleExample
{

    public class TokenResponse
    {
        public string authToken { get; set; }
        public string refreshToken { get; set; }
        public bool mustCompleteTwoFactor { get; set; }
    }
}
