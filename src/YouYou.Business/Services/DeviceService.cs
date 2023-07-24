using System.Transactions;
using YouYou.Business.Interfaces;
using YouYou.Business.Models;
using YouYou.Business.Models.Dtos;
using YouYou.Business.Models.Pagination;

namespace YouYou.Business.Services
{
    public class DeviceService : BaseService, IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly IDeviceHistoryRepository _deviceHistoryRepository;

        public DeviceService(IErrorNotifier errorNotifier, IDeviceRepository deviceRepository,
            IDeviceHistoryRepository deviceHistoryRepository) : base(errorNotifier)
        {
            _deviceRepository = deviceRepository;
            _deviceHistoryRepository = deviceHistoryRepository;
        }
        public async Task CreateDevice(Device device, Guid userId)
        {
            using (TransactionScope tr = new TransactionScope(TransactionScopeAsyncFlowOption.Enabled))
            {
                await _deviceRepository.Add(device);
                if (device.PersonId != null)
                {
                    var history = new DeviceHistory(device.Id, (int)device.PersonId, userId, true);
                    await _deviceHistoryRepository.Add(history);
                }
                tr.Complete();
            }
        }

        public async Task<IEnumerable<Device>> GetAllDevices(DeviceFilter filter)
        {
            return await _deviceRepository.GetAllDevices(filter);
        }
        public async Task<int> GetTotalRecords(DeviceFilter filter)
        {
            return await _deviceRepository.GetTotalRecords(filter);
        }
        public async Task<Device> GetDeviceById(int id)
        {
            return await _deviceRepository.GetDeviceById(id);
        }
        public async Task<DeviceDetailDTO> GetDetailDevice(int id)
        {
            return await _deviceRepository.GetDetailDevice(id);
        }
        public async Task Disable(int id)
        {
            var device = await _deviceRepository.GetById(id);
            device.IsActive = false;
            await _deviceRepository.Update(device);
        }
        public async Task Enable(int id)
        {
            var device = await _deviceRepository.GetById(id);
            device.IsActive = true;
            await _deviceRepository.Update(device);
        }

        public async Task UpdateDevice(Device device, Guid userId)
        {
            if (!device.Histories.Any(h => h.Status && h.PersonId == device.PersonId))
            {
                device.Histories.FirstOrDefault(h => h.Status).UnbindingDate = DateTime.Now;
                device.Histories.FirstOrDefault(h => h.Status).Status = false;
                var historico = new DeviceHistory(device.Id, (int)device.PersonId, userId, true);
                await _deviceHistoryRepository.Add(historico);
            }
            await _deviceRepository.Update(device);
        }

        public async Task DeleteDevice(Device device)
        {
            device.IsDeleted = true;
            await _deviceRepository.Update(device);
        }

        public void Dispose()
        {
            _deviceRepository?.Dispose();
        }
    }
}
