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
    public interface ICategoryService : IDependency
    {
        IEnumerable<Category> List();
        void Add(Category c);
        void Update(Category c);
        void Delete(int id);
        Category Get(int id);
        IEnumerable<Category> GetPageList(int pageIndex, int pageSize, ref int totalCount);

        Category FindByName(string name);
    }

    public class CategoryService : ICategoryService
    {
        private readonly AppDbContext _appDbContext;
        public CategoryService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<Category> List()
        {
            return _appDbContext.Categories.OrderByDescending(x => x.CreateTime).ToList();
        }

        public void Add(Category c)
        {
            _appDbContext.Categories.Add(c);
            _appDbContext.SaveChanges();
        }

        public void Update(Category c)
        {
            _appDbContext.Entry(c).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var category = _appDbContext.Categories.Find(id);

            if (category == null)
            {
                return;
            }

            _appDbContext.Categories.Remove(category);
            _appDbContext.SaveChanges();
        }

        public Category Get(int id)
        {
            return _appDbContext.Categories.Find(id);
        }

        public IEnumerable<Category> GetPageList(int pageIndex, int pageSize, ref int totalCount)
        {
            var list = (from p in _appDbContext.Categories
                        orderby p.CreateTime descending
                        select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = _appDbContext.Categories.Count();
            return list.ToList();
        }

        public Category FindByName(string name)
        {
            return _appDbContext.Categories.FirstOrDefault(x => x.CategoryName == name);
        }
    }
}
