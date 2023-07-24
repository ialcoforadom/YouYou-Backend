using YouYou.Business.ErrorNotifications;

namespace YouYou.Business.Interfaces
{
    public interface IErrorNotifier
    {
        bool HasErrorNotification();
        List<ErrorNotification> GetErrorNotifications();
        void Handle(ErrorNotification errorNotification);
    }
}
