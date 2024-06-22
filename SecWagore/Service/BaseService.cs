using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SecWagore.Models;
using System.Data.Entity.Infrastructure;

namespace SecWagore.Service
{
    public class BaseService<T> : IDisposable where T : class
    {

        /// <summary>
        /// NLOG 紀錄控制器
        /// </summary>
        //protected Logger Logger => LogManager.GetCurrentClassLogger();

        /// <summary>
        /// DbModel
        /// </summary>
        protected SecDbContext _context { get; set; }

        /// <summary>
        /// DbSet
        /// </summary>
        protected DbSet<T> EntitySet;

        /// <summary>
        /// BaseService 建構子
        /// </summary>
        /// <param name="dbModel"></param>
        protected BaseService(SecDbContext dbModel)
        {
            //_context = dbModel ?? throw new ArgumentNullException(nameof(dbModel));
            _context = dbModel;
            EntitySet = dbModel.Set<T>();
        }

        /// <summary>
        /// 新增 with SaveChanges(); 請別再呼叫 SaveChanges();
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Create(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            EntitySet.Add(instance);
            return SaveChanges();
        }

        /// <summary>
        /// 刪除 with SaveChanges(); 請別再呼叫SaveChanges();
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Delete(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            _context.Entry(instance).State = EntityState.Deleted;
            return SaveChanges();
        }

        /// <summary>
        /// 修改 with SaveChanges(); 請別再呼叫SaveChanges();
        /// </summary>
        /// <param name="instance"></param>
        /// <returns></returns>
        public int Update(T instance)
        {
            if (instance == null)
            {
                throw new ArgumentNullException(nameof(instance));
            }
            //_context.Entry(instance).State = EntityState.Modified;
            return SaveChanges();
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <returns></returns>
        private int SaveChanges()
        {
            return _context.SaveChanges();
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <returns></returns>
        //private int SaveChanges()
        //{
        //    //SetAuditableValues();

        //    try
        //    {
        //        return _context.SaveChanges();
        //    }
        //    catch (Exception ex)
        //    {
        //        //var logger = LogManager.GetCurrentClassLogger();
        //        //logger.Error(ex, ex.Message);
        //        throw;
        //    }
        //}

        /// <summary>
        /// Dispose
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose
        /// </summary>
        /// <param name="disposing"></param>
        protected virtual void Dispose(bool disposing)
        {
            if (!disposing || _context == null)
            {
                return;
            }
            _context.Dispose();
        }
    }
}