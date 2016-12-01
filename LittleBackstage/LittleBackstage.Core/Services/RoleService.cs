using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LittleBackstage.Core.Models;
using LittleBackstage.Core.Repositories;
using LittleBackstage.Infrastructure;

namespace LittleBackstage.Core.Services
{
    public interface IRoleService : IDependency
    {
        IEnumerable<Role> List();
        void Add(Role r);
        void Update(Role r);
        void Delete(int id);
        Role Get(int id);
        IEnumerable<Role> GetPageList(int pageIndex, int pageSize, ref int totalCount);

    }
    public class RoleService : IRoleService
    {
         private readonly AppDbContext _appDbContext;
         public RoleService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Role> List()
        {
            return _appDbContext.Roles.OrderByDescending(x => x.CreateTime).ToList();
        }

        public void Add(Role r)
        {
            _appDbContext.Roles.Add(r);
            _appDbContext.SaveChanges();
        }

        public void Update(Role r)
        {
            _appDbContext.Entry(r).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var role = _appDbContext.Roles.Find(id);

            if (role == null)
            {
                return;
            }

            _appDbContext.Roles.Remove(role);
            _appDbContext.SaveChanges();
        }

        public Role Get(int id)
        {
            return _appDbContext.Roles.Find(id);
        }

        public IEnumerable<Role> GetPageList(int pageIndex, int pageSize, ref int totalCount)
        {
            var list = (from p in _appDbContext.Roles
                        orderby p.CreateTime descending
                        select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = _appDbContext.Roles.Count();
            return list.ToList();
        }
    }
}
