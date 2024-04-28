using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//var cnstr = $@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename={builder.Environment.ContentRootPath}App_Data\¸ê®Æ®wÀÉ®×;Integrated Security=True;Trusted_Connection=True;";

// Configuration
var configuration = builder.Configuration;
var connectionString = configuration.GetConnectionString("SecWagoreContext");
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseSqlServer(connectionString));

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

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Login}/{id?}");

app.UseSwagger();
app.UseSwaggerUI();

app.Run();
