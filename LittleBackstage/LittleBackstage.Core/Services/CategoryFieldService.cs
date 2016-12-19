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
    public interface ICategoryFieldService : IDependency
    {
        IEnumerable<CategoryField> List();
        IEnumerable<CategoryField> ListByCategoryId(int id);

        void Add(CategoryField c);
        void Update(CategoryField c);
        void Delete(int id);
        CategoryField Get(int id);
        //IEnumerable<CategoryField> GetPageList(int pageIndex, int pageSize, ref int totalCount);

        CategoryField FindByNameAndCategoryId(string name, int categoryId);
    }

    public class CategoryFieldService : ICategoryFieldService
    {
        private readonly AppDbContext _appDbContext;
        public CategoryFieldService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<CategoryField> List()
        {
            return _appDbContext.CategoryFields.OrderByDescending(x => x.CreateTime).ToList();
        }

        public IEnumerable<CategoryField> ListByCategoryId(int id)
        {
            return _appDbContext.CategoryFields.Where(x => x.Category.CategoryId == id).OrderByDescending(x => x.CreateTime).ToList();
        }

        public void Add(CategoryField c)
        {
            _appDbContext.CategoryFields.Add(c);
            _appDbContext.SaveChanges();
        }

        public void Update(CategoryField c)
        {
            _appDbContext.Entry(c).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var categoryFields = _appDbContext.CategoryFields.Find(id);

            if (categoryFields == null)
            {
                return;
            }

            _appDbContext.CategoryFields.Remove(categoryFields);
            _appDbContext.SaveChanges();
        }

        public CategoryField Get(int id)
        {
            return _appDbContext.CategoryFields.Find(id);
        }

        //public IEnumerable<CategoryField> GetPageList(int pageIndex, int pageSize, ref int totalCount)
        //{
        //    throw new NotImplementedException();
        //}

        public CategoryField FindByNameAndCategoryId(string name, int categoryId)
        {
            return _appDbContext.CategoryFields.FirstOrDefault(x => x.FieldName == name && x.Category.CategoryId == categoryId);
        }
    }
}
