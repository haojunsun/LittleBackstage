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
    public interface ILiteratureExcelServices : IDependency
    {
        IEnumerable<LiteratureExcel> List();

        IEnumerable<LiteratureExcel> List(int pageIndex, int pageSize, ref int totalCount);

        void Add(LiteratureExcel excel);
        void Update(LiteratureExcel excel);
        void Delete(int id);
        LiteratureExcel Get(int id);

        //IEnumerable<LiteratureExcel> Search(string key, int pageIndex, int pageSize, ref int totalCount);

        /// <summary>
        /// 全文检索
        /// </summary>
        /// <param name="key"></param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalCount"></param>
        /// <returns></returns>
       // IEnumerable<LiteratureExcel> SearchFull(string key, string yzfs, string mz, int pageIndex, int pageSize, ref int totalCount);

        /// <summary>
        /// 高级检索
        /// </summary>
        //IEnumerable<LiteratureExcel> SeniorSearch(int state, string key, string yzfs, string mz, int pageIndex, int pageSize, ref int totalCount);
       // LiteratureExcel GetFirstByRenGongBianMa(string renGongBianMa);
        void Import(List<LiteratureExcel> excels);
    }
    public class LiteratureExcelServices : ILiteratureExcelServices
    {
          private readonly AppDbContext _appDbContext;
          public LiteratureExcelServices(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<LiteratureExcel> List()
        {
            return _appDbContext.LiteratureExcels.OrderByDescending(x => x.CreatedUtc).ToList();
        }

        public IEnumerable<LiteratureExcel> List(int pageIndex, int pageSize, ref int totalCount)
        {
            var list = (from p in _appDbContext.LiteratureExcels
                        orderby p.CreatedUtc descending
                        select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = _appDbContext.ForExcels.Count();
            return list.ToList();
        }

        public void Add(LiteratureExcel excel)
        {
            _appDbContext.LiteratureExcels.Add(excel);
            _appDbContext.SaveChanges();
        }

        public void Update(LiteratureExcel excel)
        {
            _appDbContext.Entry(excel).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var excel = _appDbContext.LiteratureExcels.Find(id);

            if (excel == null)
            {
                return;
            }

            _appDbContext.LiteratureExcels.Remove(excel);
            _appDbContext.SaveChanges();
        }

        public LiteratureExcel Get(int id)
        {
            return _appDbContext.LiteratureExcels.Find(id);
        }

        //public IEnumerable<LiteratureExcel> Search(string key, int pageIndex, int pageSize, ref int totalCount)
        //{
            //var list = (from p in _appDbContext.LiteratureExcels
            //            where p.RenGongBianMa.Contains(key)
            //            orderby p.CreatedUtc descending
            //            select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            //totalCount = _appDbContext.ForExcels.Count(x => x.RenGongBianMa.Contains(key));
            //return list.ToList();
        //}

        //public IEnumerable<ForExcel> SearchFull(string key, string yzfs, string mz, int pageIndex, int pageSize, ref int totalCount)
        //{
        //    var list = (from p in _appDbContext.ForExcels
        //                where (p.TiMing_ZhengTiMing.Contains(key) || p.TiMing_QiTaTiMing.Contains(key) || p.WenHuaBeiJing_LiShiYuanLiu.Contains(key) || p.WenHuaBeiJing_LiuBuDiYu.Contains(key) || p.WenHuaBeiJing_ShiYuongChangSuo.Contains(key) || p.CaiLuXinXi_DiDian.Contains(key) || p.YueQiJianJie.Contains(key) || p.ShiFanYuQu_QuMuMing.Contains(key)) && (p.LeiBie_YanZouFangShi.Contains(yzfs) && p.MinZuShuXing.Contains(mz))
        //                orderby p.CreatedUtc descending
        //                select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //    totalCount = _appDbContext.ForExcels.Count(p => (p.TiMing_ZhengTiMing.Contains(key) || p.TiMing_QiTaTiMing.Contains(key) || p.WenHuaBeiJing_LiShiYuanLiu.Contains(key) || p.WenHuaBeiJing_LiuBuDiYu.Contains(key) || p.WenHuaBeiJing_ShiYuongChangSuo.Contains(key) || p.CaiLuXinXi_DiDian.Contains(key) || p.YueQiJianJie.Contains(key) || p.ShiFanYuQu_QuMuMing.Contains(key)) && (p.LeiBie_YanZouFangShi.Contains(yzfs) && p.MinZuShuXing.Contains(mz)));
        //    return list.ToList();
        //}

        //public IEnumerable<ForExcel> SeniorSearch(int state, string key, string yzfs, string mz, int pageIndex, int pageSize, ref int totalCount)
        //{
        //    switch (state)
        //    {
        //        case 0:
        //            return SearchFull(key, yzfs, mz, pageIndex, pageSize, ref totalCount);
        //        case 1:
        //            {
        //                var list = (from p in _appDbContext.ForExcels
        //                            where p.TiMing_ZhengTiMing.Contains(key) && p.LeiBie_YanZouFangShi.Contains(yzfs) && p.MinZuShuXing.Contains(mz)
        //                            orderby p.CreatedUtc descending
        //                            select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
        //                totalCount = _appDbContext.ForExcels.Count(p => p.TiMing_ZhengTiMing.Contains(key) && p.LeiBie_YanZouFangShi.Contains(yzfs) && p.MinZuShuXing.Contains(mz));
        //                return list.ToList();
        //            }
        //    }
        //    return List(pageIndex, pageSize, ref totalCount);
        //}

        //public ForExcel GetFirstByRenGongBianMa(string renGongBianMa)
        //{
        //    return _appDbContext.ForExcels.FirstOrDefault(x => x.RenGongBianMa == renGongBianMa);
        //}

        public void Import(List<LiteratureExcel> excels)
        {
            _appDbContext.LiteratureExcels.AddRange(excels);
            _appDbContext.SaveChanges();
        }
    }
}
