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
            else if (model.Id > 0)
            {
                var rr = _context.EntryLogs.Any(r => r.Id == model.Id);
                if (!rr)
                {
                    return ResultHelper.Failure<EntryLogVM>("找不到指定的資料!", ResultHelper.StatusCode.Save);
                }
            }

            using (var trans = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var entryLog = _context.EntryLogs
                    .Where(r => r.Id == model.Id)
                    .FirstOrDefault();

                    if (entryLog != null)
                    {
                        _mapper.Map(model, entryLog);
                        entryLog.UpdateDate = DateTime.Now;
                        entryLog.ExitTime = DateTime.Now;

                        await _context.SaveChangesAsync();
                        await trans.CommitAsync();

                        var updatedModel = _mapper.Map<EntryLogVM>(entryLog);
                        return ResultHelper.Success<EntryLogVM>(updatedModel, ResultHelper.StatusCode.Get);
                    }
                    else
                    {
                        return ResultHelper.Failure<EntryLogVM>("找不到指定的資料!", ResultHelper.StatusCode.Save);
                    }

                }
                catch (Exception ex)
                {
                    await trans.RollbackAsync();
                    return ResultHelper.Failure<EntryLogVM>("異動資料失敗！！" + ex.Message, ResultHelper.StatusCode.Get);
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
                query = query.Where(el => el.Purpose == (byte)vm.Purpose.Value);

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

            var sql = query.ToQueryString();


            var result = query
                        .ProjectTo<EntryLogVM>(_mapper.ConfigurationProvider)
                        .ToList();

            return result;

        }
    }
}
