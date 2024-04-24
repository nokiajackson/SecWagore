using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using SecWagore.Models;

public class AccountService : SecDbContext
{
    private readonly SecDbContext _db;

    public AccountService(SecDbContext dbContext)
    {
        _db = dbContext;
    }

    public bool ValidateCredentials(string username, string password)
    {
        // 使用 LINQ 查詢檢查帳戶是否存在並驗證密碼
        var account = _db.Accounts.FirstOrDefault(a => a.Username == username);
        if (account != null)
        {
            // 在這裡你可能會使用加密方式進行密碼比對
            // 這裡僅作為示例，使用明文比較
            if (account.Password == password)
            {
                return true; // 驗證成功
            }
        }
        return false; // 驗證失敗
    }


    public List<Account> GetAllAccounts()
    {
        return _db.Accounts.ToList();
    }

    public Account GetAccountById(int id)
    {
        return _db.Accounts.FirstOrDefault(account => account.Id == id);
    }

    public void AddAccount(Account account)
    {
        _db.Accounts.Add(account);
        _db.SaveChanges();
    }

    public void UpdateAccount(Account account)
    {
        _db.Entry(account).State = EntityState.Modified;
        _db.SaveChanges();
    }

    public void DeleteAccount(int id)
    {
        var account = _db.Accounts.Find(id);
        if (account != null)
        {
            _db.Accounts.Remove(account);
            _db.SaveChanges();
        }
    }
}
