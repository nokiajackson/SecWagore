using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SecWagore.Models;

namespace SecWagore.Service
{
    public abstract class BaseService<T> : IDisposable
       where T : class
    {

        /// <summary>
        /// NLOG 紀錄控制器
        /// </summary>
        //protected Logger Logger => LogManager.GetCurrentClassLogger();

        /// <summary>
        /// DbModel
        /// </summary>
        protected SecDbContext DbModel { get; set; }

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
            DbModel = dbModel;
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
            DbModel.Entry(instance).State = EntityState.Deleted;
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
            DbModel.Entry(instance).State = EntityState.Modified;
            return SaveChanges();
        }

        /// <summary>
        /// 儲存
        /// </summary>
        /// <returns></returns>
        private int SaveChanges()
        {
            return DbModel.SaveChanges();
        }

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
            if (!disposing || DbModel == null)
            {
                return;
            }
            DbModel.Dispose();
        }
    }
}