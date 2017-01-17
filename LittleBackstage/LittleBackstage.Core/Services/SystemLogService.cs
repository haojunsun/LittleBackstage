
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using LittleBackstage.Core.Models;
using LittleBackstage.Core.Repositories;
using LittleBackstage.Infrastructure;

namespace LittleBackstage.Core.Services
{
    public interface ISystemLogService : IDependency
    {
        IEnumerable<SystemLog> List();
        void Add(SystemLog log);
        void Update(SystemLog log);
        void Delete(int id);
        SystemLog Get(int id);
        IEnumerable<SystemLog> GetPageList(int pageIndex, int pageSize, ref int totalCount);
        IEnumerable<SystemLog> GetPageListByType(int logType, int pageIndex, int pageSize, ref int totalCount);

        /// <summary>
        /// 记录登录日志
        /// </summary>
        void LoginLog(string userName, int userId, string details, string operateType, int relevantId);

        /// <summary>
        /// 用户日志
        /// </summary>
        void UserLog(string userName, int userId, string details, string operateType, int relevantId);
    }

    public class SystemLogService : ISystemLogService
    {
        private readonly AppDbContext _appDbContext;
        public SystemLogService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public IEnumerable<SystemLog> List()
        {
            return _appDbContext.SystemLogs.OrderByDescending(x => x.LogTime).ToList();
        }

        public void Add(SystemLog log)
        {
            _appDbContext.SystemLogs.Add(log);
            _appDbContext.SaveChanges();
        }

        public void Update(SystemLog log)
        {
            //_appDbContext.Articles.Attach(article);
            _appDbContext.Entry(log).State = EntityState.Modified;
            _appDbContext.SaveChanges();
        }

        public void Delete(int id)
        {
            var article = _appDbContext.SystemLogs.Find(id);

            if (article == null)
            {
                return;
            }

            _appDbContext.SystemLogs.Remove(article);
            _appDbContext.SaveChanges();
        }

        public SystemLog Get(int id)
        {
            return _appDbContext.SystemLogs.Find(id);
        }

        public IEnumerable<SystemLog> GetPageList(int pageIndex, int pageSize, ref int totalCount)
        {
            var list = (from p in _appDbContext.SystemLogs
                        orderby p.LogTime descending
                        select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = _appDbContext.SystemLogs.Count();
            return list.ToList();
        }

        public IEnumerable<SystemLog> GetPageListByType(int logType, int pageIndex, int pageSize, ref int totalCount)
        {
            var list = (from p in _appDbContext.SystemLogs
                        where p.LogType == logType
                        orderby p.LogTime descending
                        select p).Skip((pageIndex - 1) * pageSize).Take(pageSize);
            totalCount = _appDbContext.SystemLogs.Count(x => x.LogType == logType);
            return list.ToList();
        }

        /// <summary>
        /// 记录登录日志
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <param name="details"></param>
        /// <param name="operateType"></param>
        /// <param name="relevantId"></param>
        public void LoginLog(string userName, int userId, string details, string operateType, int relevantId)
        {
            var log = new SystemLog();
            log.LogUserName = userName;
            log.LogUserId = userId;
            log.LogDetails = details;
            log.LogTime = DateTime.Now;
            log.LogType = 1;
            log.OperateType = operateType;
            log.RelevantId = relevantId;
            Add(log);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="userName"></param>
        /// <param name="userId"></param>
        /// <param name="details"></param>
        /// <param name="operateType"></param>
        /// <param name="relevantId"></param>
        public void UserLog(string userName, int userId, string details, string operateType, int relevantId)
        {
            var log = new SystemLog();
            log.LogUserName = userName;
            log.LogUserId = userId;
            log.LogDetails = details;
            log.LogTime = DateTime.Now;
            log.LogType = 2;
            log.OperateType = operateType;
            log.RelevantId = relevantId;
            Add(log);
        }
    }
}
