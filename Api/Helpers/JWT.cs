namespace Api.Helpers
{
    public class JWT
    {
        public string Key { get; set; }
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public double DurationInMinutes { get; set; }
        public double DurationInMinutesRefreshToken { get; set; }
        public double DurationInMinutesCookie { get; set; }
    }
}
