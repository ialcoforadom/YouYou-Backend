using Microsoft.EntityFrameworkCore;
using YouYou.Business.Interfaces;
using YouYou.Business.Models;
using YouYou.Business.Models.Pagination;
using YouYou.Data.Context;

namespace YouYou.Data.Repository
{
    public class PersonRepository : Repository<Person>, IPersonRepository
    {
        public PersonRepository(YouYouDbContext db) : base(db) { }
        public async Task<IEnumerable<Person>> GetPersonsToSelect()
        {
            return await Db.Persons.AsNoTracking().Include(p => p.PhysicalPerson).Include(p => p.JuridicalPerson).ToListAsync();
        }
        public async Task<IEnumerable<Person>> GetAllPersons(PersonFilter filter)
        {
            return await DeviceFilter(filter).AsNoTracking()
                .Include(v => v.PhysicalPerson)
                    .ThenInclude(pp => pp.Address)
                        .ThenInclude(a => a.City)
                .Include(v => v.PhysicalPerson)
                    .ThenInclude(pp => pp.Gender)
                .Include(v => v.JuridicalPerson)
                    .ThenInclude(pp => pp.Address)
                        .ThenInclude(a => a.City)
                .Include(v => v.JuridicalPerson)
                    .ThenInclude(pp => pp.User)
                .Include(v => v.PhysicalPerson)
                    .ThenInclude(pp => pp.User)
                .Skip((filter.PageNumber - 1) * filter.PageSize)
                .Take(filter.PageSize).ToListAsync();
        }
        public async Task<int> GetTotalRecords(PersonFilter filter)
        {
            return await DeviceFilter(filter).CountAsync();
        }
        private IQueryable<Person> DeviceFilter(PersonFilter filter)
        {
            IQueryable<Person> query = Db.Persons.Where(d => !d.IsDeleted)
                .Include(v => v.PhysicalPerson)
                .Include(v => v.JuridicalPerson);
            if(filter.RoleId != null)
            {
                query = query.Where(d => d.PhysicalPersonId != null ? d.PhysicalPerson.User.UserRoles.Any(u => u.RoleId == filter.RoleId) : d.JuridicalPerson.User.UserRoles.Any(u => u.RoleId == filter.RoleId));
            }
            if (!string.IsNullOrEmpty(filter.General))
                query = query.Where(x => (x.PhysicalPersonId != null ? x.PhysicalPerson.Name : x.JuridicalPerson.CompanyName).ToLower().Contains(filter.General));

            return query;
        }
        public async Task<Person> GetByIdWithIncludes(int id)
        {
            return await Db.Persons.AsNoTracking()
                .Include(p => p.PhysicalPerson)
                    .ThenInclude(pp => pp.User)
                .Include(p => p.JuridicalPerson)
                    .ThenInclude(pp => pp.User)
                .Include(p => p.PhysicalPerson)
                    .ThenInclude(pp => pp.Address)
                        .ThenInclude(a => a.City)
                .Include(p => p.JuridicalPerson)
                    .ThenInclude(pp => pp.Address)
                        .ThenInclude(a => a.City)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<Person> GetPersonByIdWithIncludesTracked(int id)
        {
            return await Db.Persons.AsTracking()
                .Include(p => p.PhysicalPerson)
                    .ThenInclude(pp => pp.User)
                .Include(p => p.JuridicalPerson)
                    .ThenInclude(pp => pp.User)
                .Include(p => p.PhysicalPerson)
                    .ThenInclude(pp => pp.Address)
                        .ThenInclude(a => a.City)
                .Include(p => p.JuridicalPerson)
                    .ThenInclude(pp => pp.Address)
                        .ThenInclude(a => a.City)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public virtual async Task<Person> GetPersonById(int id)
        {
            return await Db.Persons.AsNoTracking().FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
