using Microsoft.Extensions.Logging;
using System.Data.Entity;
using Microsoft.EntityFrameworkCore;
using SecWagore.Models;
using SecWagore.Models.ViewModel;
using System.Security.Claims;
using SecWagore.Helpers;
using System.Data.Entity.Infrastructure;
using static SecWagore.Helpers.ResultHelper;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using System.Diagnostics;


namespace SecWagore.Service
{
    /// <summary>
    /// 共通性服務
    /// </summary>
    public class EntryLogService : BaseService<EntryLog>
    {
        private readonly IConfiguration _configuration;
        private readonly IMapper _mapper;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="dbModel"></param>
        /// <param name="configuration"></param>
        /// <param name="mapper"></param>
        public EntryLogService(SecDbContext dbModel,
            IConfiguration configuration,
            IMapper mapper) : base(dbModel)
        {
            _configuration = configuration;
            _mapper = mapper;
        }


        public async Task<Result<EntryLogVM>> SaveEntryLogAsync(EntryLogVM model)
        {
            try
            {
                // 使用 AutoMapper 將 EntryLogVM 映射到 EntryLog
                var entryLog = _mapper.Map<EntryLog>(model);
                entryLog.CreateDate = DateTime.Now;
                entryLog.UpdateDate = DateTime.Now;

                _context.EntryLogs.Add(entryLog);

                var result = await _context.SaveChangesAsync();
                return ResultHelper.Success<EntryLogVM>(model, ResultHelper.StatusCode.Save);
            }
            catch (Exception ex)
            {
                return ResultHelper.Failure<EntryLogVM>("異動資料失敗！！" + ex.Message, ResultHelper.StatusCode.Save);
            }
        }


        public async Task<Result<EntryLogVM>> UpateEntryLogAsync(EntryLogVM model)
        {
            if (model.Id == 0)
            {
                return await SaveEntryLogAsync(model);
            }
            using (var trans = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var entryLog = await _context.EntryLogs.FindAsync(model.Id);
                    if (entryLog == null)
                    {
                        return ResultHelper.Failure<EntryLogVM>("找不到指定的資料!", ResultHelper.StatusCode.Save);
                    }

                    _mapper.Map(model, entryLog);
                    entryLog.UpdateDate = DateTime.Now;
                    entryLog.ExitTime = model.UpdateDate ?? DateTime.Now;

                    var result = await _context.SaveChangesAsync();

                    await trans.CommitAsync();

                    //var updatedModel = _mapper.Map<EntryLogVM>(entryLog);
                    return ResultHelper.Success<EntryLogVM>(model, ResultHelper.StatusCode.Save);
                }
                catch (Exception ex)
                {
                    // 日誌或調試輸出異常信息
                    Debug.WriteLine(ex.Message);
                    return ResultHelper.Failure<EntryLogVM>("查找資料時發生錯誤: " + ex.Message, ResultHelper.StatusCode.Save);
                }
            }

        }


        public List<EntryLogVM> GetEntryLogsList(SearchEntryLogVM vm)
        {
            var query = _context.EntryLogs.AsQueryable();

            if (vm.CampusId.HasValue && vm.CampusId!=0)
            {
                query = query.Where(el => el.CampusId == vm.CampusId.Value);
            }

            if (!string.IsNullOrEmpty(vm.FullName))
            {
                query = query.Where(el => el.FullName.Contains(vm.FullName));
            }

            if (vm.Purpose.HasValue && vm.Purpose.Value != 0)
            {
                string purposeString = vm.Purpose.Value.ToString();
                query = query.Where(el => el.Purpose == purposeString);
            }

            if (vm.EntryTimeStart.HasValue || vm.EntryTimeEnd.HasValue)
            {
                if (vm.EntryTimeStart.HasValue)
                {
                    query = query.Where(el => el.EntryTime >= vm.EntryTimeStart.Value);
                }

                if (vm.EntryTimeEnd.HasValue)
                {
                    query = query.Where(el => el.EntryTime <= vm.EntryTimeEnd.Value);
                }
            }


            if (vm.ExitTimeStart.HasValue || vm.ExitTimeEnd.HasValue)
            {
                query = query.Where(el =>
                    (!vm.ExitTimeStart.HasValue || el.ExitTime >= vm.ExitTimeStart.Value) &&
                    (!vm.ExitTimeEnd.HasValue || el.ExitTime <= vm.ExitTimeEnd.Value));
            }

            //var sql = query.ToQueryString();


            var result = query
                        .ProjectTo<EntryLogVM>(_mapper.ConfigurationProvider)
                        .ToList();

            return result;

        }
    }
}
