using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SecWagore.Models;

public class CampusService
{
    private readonly SecDbContext _context;

    public CampusService(SecDbContext context)
    {
        _context = context;
    }

    public Account ValidateCredentials(string username, string password)
    {
        return _context.Accounts.FirstOrDefault(a => a.Username == username && a.Password == password);
    }

    // Create
    public void CreateCampus(Campus campus)
    {
        _context.Campuses.Add(campus);
        _context.SaveChanges();
    }

    // Read
    public IEnumerable<Campus> GetAllCampuses()
    {
        return _context.Campuses.ToList();
    }

    public Campus GetCampusById(int id)
    {
        return _context.Campuses.Find(id);
    }

    // Update
    public void UpdateCampus(Campus campus)
    {
        _context.Entry(campus).State = EntityState.Modified;
        _context.SaveChanges();
    }

    // Delete
    public void DeleteCampus(int id)
    {
        var campus = _context.Campuses.Find(id);
        if (campus != null)
        {
            _context.Campuses.Remove(campus);
            _context.SaveChanges();
        }
    }
}
