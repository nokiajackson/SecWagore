﻿using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using SecWagore.Models;

namespace SecWagore.Service
{
    /// <summary>
    /// 共通性服務
    /// </summary>
    public class CommonService : BaseService<ApplicationUser>
    {
        private readonly IConfiguration _configuration;

        /// <summary>
        /// 建構子
        /// </summary>
        /// <param name="dbModel"></param>
        /// <param name="configuration"></param>
        /// <param name="userManageService"></param>
        public CommonService(SecDbContext dbModel,
            IConfiguration configuration) : base(dbModel)
        {
        }

    }
}
