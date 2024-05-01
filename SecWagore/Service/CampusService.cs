using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SecWagore.Models;

public class CampusService
{
    private readonly SecDbContext _db;

    public CampusService(SecDbContext dbContext)
    {
        _db = dbContext;
    }

    public List<Campus> GetAllCampus()
    {
        return _db.Campuses.ToList();
    }

}
