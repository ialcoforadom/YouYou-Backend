using Microsoft.EntityFrameworkCore;
using YouYou.Business.Interfaces;
using YouYou.Business.Models;
using YouYou.Business.Models.Dtos;
using YouYou.Business.Models.Pagination;
using YouYou.Data.Context;

namespace YouYou.Data.Repository
{
    public class DeviceRepository : Repository<Device>, IDeviceRepository
    {
        public DeviceRepository(YouYouDbContext db) : base(db)
        {
        }
        public async Task<IEnumerable<Device>> GetAllDevices(DeviceFilter filter)
        {
            return await DeviceFilter(filter).AsNoTracking()
                .Include(p => p.Person).ThenInclude(v => v.PhysicalPerson)
                .Include(p => p.Person).ThenInclude(v => v.JuridicalPerson)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
        }
        public async Task<int> GetTotalRecords(DeviceFilter filter)
        {
            return await DeviceFilter(filter).CountAsync();
        }

        private IQueryable<Device> DeviceFilter(DeviceFilter filter)
        {
            IQueryable<Device> query = Db.Devices.Where(d => !d.IsDeleted)
                .Include(p => p.Person).ThenInclude(v => v.PhysicalPerson)
                .Include(p => p.Person).ThenInclude(v => v.JuridicalPerson);
            if (filter.PersonId != null)
                query = query.Where(p => p.PersonId == filter.PersonId);
            if (filter.Status == true)
                query = query.Where(p => p.IsActive == true);
            if (filter.Status == false)
                query = query.Where(p => p.IsActive == false);
            if (!string.IsNullOrEmpty(filter.General))
                query = query.Where(x => (x.Model + (x.Person.PhysicalPersonId != null ? x.Person.PhysicalPerson.Name : x.Person.JuridicalPerson.CompanyName) + x.Code).ToLower().Contains(filter.General));

            return query;
        }
        public virtual async Task<Device> GetDeviceById(int id)
        {
            return await Db.Devices.AsNoTracking().Include(d => d.Histories).FirstOrDefaultAsync(d => d.Id == id);
        }
        public virtual async Task<DeviceDetailDTO> GetDetailDevice(int id)
        {
            return await Db.Devices.AsNoTracking()
                .Include(d => d.Histories)
                    .ThenInclude(d => d.Person)
                        .ThenInclude(v => v.PhysicalPerson)
                            .ThenInclude(pf => pf.Address)
                                .ThenInclude(e => e.City)
                .Include(d => d.Histories)
                    .ThenInclude(d => d.Person)
                        .ThenInclude(v => v.JuridicalPerson)
                            .ThenInclude(pf => pf.Address)
                                .ThenInclude(e => e.City)
                .Where(d => d.Id == id)
                .Select(d => new DeviceDetailDTO()
                {
                    Id = d.Id,
                    Model = d.Model,
                    Code = d.Code,
                    Status = d.IsActive,
                    PersonId = d.PersonId,
                    History = d.Histories.Select(h => new DeviceHistoryDetailDTO(h))
                }).FirstOrDefaultAsync();
        }
    }
}
