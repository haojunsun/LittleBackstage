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
    public interface IUserService : IDependency
    {
        IEnumerable<User> List();
        void Add(User u);
        void Update(User u);
        void Delete(int id);
        User Get(int id);
        IEnumerable<User> GetPageList(int pageIndex, int pageSize, ref int totalCount);
    }

    public class UserService : IUserService
    {

        private readonly AppDbContext _appDbContext;
        public UserService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IEnumerable<User> List()
        {
            return _appDbContext.Users.OrderByDescending(x => x.Register).ToList();
        }

        public void Add(User u)
        {
            _appDbContext.Users.Add(u);
            _appDbContext.SaveChanges();
        }

        public void Update(User u)
        {
            _appDbContext.Entry(u).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _appDbContext.Users.Find(id);

            if (user == null)
            {
                return;
            }

            _appDbContext.Users.Remove(user);
            _appDbContext.SaveChanges();
        }

        public User Get(int id)
        {
            return _appDbContext.Users.Find(id);
        }

        public IEnumerable<User> GetPageList(int pageIndex, int pageSize, ref int totalCount)
        {
            var list = (from p in _appDbContext.Users
                        orderby p.Register descending
                        select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = _appDbContext.Users.Count();
            return list.ToList();
        }
    }
}
