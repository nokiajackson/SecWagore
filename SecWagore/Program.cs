using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using SecWagore.Models;
using SecWagore.Service;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
try {
    // Add services to the container.
    builder.Services.AddControllersWithViews();
    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(options => {

    });

    builder.Services.AddMvc();
    builder.Services.AddHttpContextAccessor();
    // Configuration
    var configuration = builder.Configuration;
    var connectionString = configuration.GetConnectionString("SecWagoreContext");
    builder.Services.AddDbContext<SecDbContext>(options =>
        options.UseSqlServer(connectionString));

    builder.Services.AddScoped<AccountService>();
    builder.Services.AddScoped<CampusService>();
    builder.Services.AddScoped<CommonService>();


    //�W�[���Ҥ覡�A�ϥ� cookie ����
    builder.Services.AddAuthentication(options =>
        {
            options.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        })
        .AddCookie(options => {
        //�s��������cookie �u��g��HTTP(S) ��w�Ӧs��
        options.Cookie.HttpOnly = true;
        //���n�J�ɷ|�۰ʾɨ�n�J��
        options.LoginPath = new PathString("/Home/Login");
        //���v�������ڵ��X�ݷ|�۰ʾɨ즹���|
        options.AccessDeniedPath = new PathString("/Home/AccessDenied");
    });

    var app = builder.Build();

    // Configure the HTTP request pipeline.
    if (!app.Environment.IsDevelopment())
    {
        app.UseExceptionHandler("/Home/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
    }

    app.UseHttpsRedirection();
    app.UseStaticFiles();

    app.UseRouting();

    app.UseAuthorization();
    // �ϥ� Session
    app.UseSession();

    app.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Login}/{id?}");

    app.UseSwagger();
    app.UseSwaggerUI();

    app.Run();

}
catch (Exception ex)
{
    WriteLog(ex.Message);
    throw ex;
}

void WriteLog(string message)
{
    var path = Path.GetDirectoryName(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath);
    if (!Directory.Exists(path + "\\Logs\\"))
    {
        Directory.CreateDirectory(path + "\\Logs\\");
    }
    var logFile = path + "\\Logs\\" + string.Format("FiscPms_{0:D3}{1:D2}{2:D2}.log", DateTime.Now.Year - 1911, DateTime.Now.Month, DateTime.Now.Day);
    using (StreamWriter sw = System.IO.File.AppendText(logFile))
    {
        sw.WriteLine(string.Format("{0:T}:{1} ", DateTime.Now, message));
    }
}