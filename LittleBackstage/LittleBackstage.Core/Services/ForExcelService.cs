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
    public interface IForExcelService : IDependency
    {
        IEnumerable<ForExcel> List();

        IEnumerable<ForExcel> List(int pageIndex, int pageSize, ref int totalCount);

        void Add(ForExcel excel);
        void Update(ForExcel excel);
        void Delete(int id);
        ForExcel Get(int id);

        IEnumerable<ForExcel> Search(string key, int pageIndex, int pageSize, ref int totalCount);

        ForExcel GetFirstByRenGongBianMa(string renGongBianMa);
        void Import(List<ForExcel> excels);
    }

    public class ForExcelService : IForExcelService
    {
        private readonly AppDbContext _appDbContext;
        public ForExcelService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<ForExcel> List()
        {
            return _appDbContext.ForExcels.OrderByDescending(x => x.CreatedUtc).ToList();
        }

        public IEnumerable<ForExcel> List(int pageIndex, int pageSize, ref int totalCount)
        {
            throw new NotImplementedException();
        }

        public void Add(ForExcel excel)
        {
            _appDbContext.ForExcels.Add(excel);
            _appDbContext.SaveChanges();
        }

        public void Update(ForExcel excel)
        {
            _appDbContext.Entry(excel).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var excel = _appDbContext.ForExcels.Find(id);

            if (excel == null)
            {
                return;
            }

            _appDbContext.ForExcels.Remove(excel);
            _appDbContext.SaveChanges();
        }

        public ForExcel Get(int id)
        {
            return _appDbContext.ForExcels.Find(id);
        }

        public IEnumerable<ForExcel> Search(string key, int pageIndex, int pageSize, ref int totalCount)
        {
            var list = (from p in _appDbContext.ForExcels
                        where p.RenGongBianMa.Contains(key)
                        orderby p.CreatedUtc descending
                        select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = _appDbContext.ForExcels.Count(x => x.RenGongBianMa.Contains(key));
            return list.ToList();
        }



        public ForExcel GetFirstByRenGongBianMa(string renGongBianMa)
        {
            return _appDbContext.ForExcels.FirstOrDefault(x => x.RenGongBianMa == renGongBianMa);
        }

        public void Import(List<ForExcel> excels)
        {
            _appDbContext.ForExcels.AddRange(excels);
            _appDbContext.SaveChanges();
        }
        //在批量导入之前 需要做一个 重复对比 暂时 搁置
    }
}
