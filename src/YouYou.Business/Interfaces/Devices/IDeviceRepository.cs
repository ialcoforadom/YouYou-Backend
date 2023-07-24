using YouYou.Business.Models;
using YouYou.Business.Models.Dtos;
using YouYou.Business.Models.Pagination;

namespace YouYou.Business.Interfaces
{
    public interface IDeviceRepository : IRepository<Device>
    {
        Task<IEnumerable<Device>> GetAllDevices(DeviceFilter filtro);
        Task<int> GetTotalRecords(DeviceFilter filtro); 
        Task<DeviceDetailDTO> GetDetailDevice(int id);
        Task<Device> GetDeviceById(int id);
    }
}
