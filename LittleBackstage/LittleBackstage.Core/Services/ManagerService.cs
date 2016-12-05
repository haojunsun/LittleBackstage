
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LittleBackstage.Core.Models;
using LittleBackstage.Core.Repositories;
using LittleBackstage.Infrastructure;

namespace LittleBackstage.Core.Services
{
    public interface IManagerService : IDependency
    {
        IEnumerable<Manager> List();
        void Add(Manager m);
        void Update(Manager m);
        void Delete(int id);
        Manager Get(int id);
        IEnumerable<Manager> GetPageList(int pageIndex, int pageSize, ref int totalCount);

        Manager LoginByPassword(string userName, string password);
    }

    public class ManagerService : IManagerService
    {
        private readonly AppDbContext _appDbContext;
        public ManagerService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Manager> List()
        {
            return _appDbContext.Managers.OrderByDescending(x => x.Register).ToList();
        }

        public void Add(Manager m)
        {
            _appDbContext.Managers.Add(m);
            _appDbContext.SaveChanges();
        }

        public void Update(Manager m)
        {
            //_appDbContext.Articles.Attach(article);
            _appDbContext.Entry(m).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var manager = _appDbContext.Managers.Find(id);

            if (manager == null)
            {
                return;
            }

            _appDbContext.Managers.Remove(manager);
            _appDbContext.SaveChanges();
        }

        public Manager Get(int id)
        {
            return _appDbContext.Managers.Find(id);
        }

        public IEnumerable<Manager> GetPageList(int pageIndex, int pageSize, ref int totalCount)
        {
            var list = (from p in _appDbContext.Managers
                        orderby p.Register descending
                        select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = _appDbContext.Managers.Count();
            return list.ToList();
        }

        public Manager LoginByPassword(string userName, string password)
        {
            var user = _appDbContext.Managers.FirstOrDefault(x => x.UserName == userName && x.PassWord == password);
            return user ?? null;
        }
    }
}
