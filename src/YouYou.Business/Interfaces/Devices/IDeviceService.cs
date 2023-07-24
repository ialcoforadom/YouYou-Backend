using YouYou.Business.Models;
using YouYou.Business.Models.Dtos;
using YouYou.Business.Models.Pagination;

namespace YouYou.Business.Interfaces
{
    public interface IDeviceService
    {
        Task CreateDevice(Device device, Guid userId);
        Task<IEnumerable<Device>> GetAllDevices(DeviceFilter filter);
        Task<int> GetTotalRecords(DeviceFilter filter);
        Task<Device> GetDeviceById(int id); 
        Task<DeviceDetailDTO> GetDetailDevice(int id);
        Task Disable(int id);
        Task Enable(int id);
        Task UpdateDevice(Device device, Guid userId);
        Task DeleteDevice(Device device);
    }
}
