using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SecWagore.Models;
using SecWagore.Service;

public class CampusService : BaseService<Campus>
{
    private readonly IConfiguration _configuration;

    /// <param name="dbModel"></param>
    /// <param name="configuration"></param>
    public CampusService(SecDbContext dbContext,
        IConfiguration configuration
        ) : base(dbContext)
    {
        _configuration = configuration;
    }

}
