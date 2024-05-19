using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SecWagore.Models;
using SecWagore.Service;

public partial class AccountService : BaseService<Campus>
{
    private readonly IConfiguration _configuration;
    private readonly IHttpContextAccessor _httpContextAccessor;

    /// <param name="dbModel"></param>
    /// <param name="configuration"></param>
    public AccountService(SecDbContext dbContext,
        IConfiguration configuration,
        IHttpContextAccessor httpContextAccessor
        ) : base(dbContext)
    {
        _configuration = configuration;
        _httpContextAccessor = httpContextAccessor;
    }

    public bool ValidateCredentials(LoginModelVM model)
    {
        // 使用 LINQ 查詢檢查帳戶是否存在並驗證密碼
        var account = _context.Accounts.FirstOrDefault(a => a.Username == model.Username);
        return account != null && account.CampusId == model.Campus && account.Password == model.Password; 
    }

    public List<Account> GetAllAccounts()
    {
        return _context.Accounts.ToList();
    }

    public Account GetAccountByName(string userName)
    {
        return _context.Accounts.FirstOrDefault(account => account.Username == userName);
    }
    public Account GetAccountById(int userId)
    {
        return _context.Accounts.FirstOrDefault(account => account.Id == userId);
    }

    public async Task<Campus> GetCampusByIdAsync(int campusId)
    {
        // 使用Entity Framework Core來查詢校區
        return await _context.Campuses.FindAsync(campusId);
    }

    public void CreateUser(Account account)
    {
        _context.Accounts.Add(account);
        _context.SaveChanges();
    }

    public void DeleteAccount(int id)
    {
        var account = _context.Accounts.Find(id);
        if (account != null)
        {
            _context.Accounts.Remove(account);
            _context.SaveChanges();
        }
    }
}
