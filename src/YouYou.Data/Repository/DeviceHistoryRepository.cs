using YouYou.Business.Interfaces;
using YouYou.Business.Models;
using YouYou.Data.Context;

namespace YouYou.Data.Repository
{
    public class DeviceHistoryRepository : Repository<DeviceHistory>, IDeviceHistoryRepository
    {
        public DeviceHistoryRepository(YouYouDbContext db) : base(db)
        {
        }
    }
}
