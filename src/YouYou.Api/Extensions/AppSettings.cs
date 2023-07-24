namespace YouYou.Api.Extensions
{
    public class AppSettings
    {
        public string Secret { get; set; }
        public int ExpiryHours { get; set; }
        public int FinalExpiration { get; set; }
        public string Emitter { get; set; }
        public string ValidIn { get; set; }
    }
}
