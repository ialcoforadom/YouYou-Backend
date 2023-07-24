using YouYou.Business.Models;

namespace YouYou.Business.Interfaces
{
    public interface ILogExceptionRepository
    {
        Task Add(LogException logException);
    }
}
