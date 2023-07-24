namespace YouYou.Business.ErrorNotifications
{
    public class ErrorNotification
    {
        public string Message { get; }

        public ErrorNotification(string message)
        {
            Message = message;
        }
    }
}
