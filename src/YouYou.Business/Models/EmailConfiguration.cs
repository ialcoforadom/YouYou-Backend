namespace YouYou.Business.Models
{
    public class EmailConfiguration
    {
        public string From { get; set; } = null;
        public string SmtpServer { get; set; } = null;
        public int Port { get; set; }
        public bool UseSsl { get; set; } = false;
        public string UserName { get; set; } = null;
        public string Password { get; set; } = null;
    }
}
