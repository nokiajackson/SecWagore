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
        var account = DbModel.Accounts.FirstOrDefault(a => a.Username == model.Username);
        if (account != null)
        {
            // 在這裡你可能會使用加密方式進行密碼比對
            // 這裡僅作為示例，使用明文比較
            return account.Password == model.Password; // 驗證成功
        }
        return false; // 驗證失敗
    }


    public List<Account> GetAllAccounts()
    {
        return DbModel.Accounts.ToList();
    }

    public Account GetAccountById(string userName)
    {
        return DbModel.Accounts.FirstOrDefault(account => account.Username == userName);
    }
    public async Task<Campus> GetCampusByIdAsync(int campusId)
    {
        // 使用Entity Framework Core來查詢校區
        return await DbModel.Campuses.FindAsync(campusId);
    }

    public void CreateUser(Account account)
    {
        DbModel.Accounts.Add(account);
        DbModel.SaveChanges();
    }

    

    public void DeleteAccount(int id)
    {
        var account = DbModel.Accounts.Find(id);
        if (account != null)
        {
            DbModel.Accounts.Remove(account);
            DbModel.SaveChanges();
        }
    }
}
