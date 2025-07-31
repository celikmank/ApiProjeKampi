var builder = WebApplication.CreateBuilder(args);

// 1. Cookie authentication'ı ekle
builder.Services.AddAuthentication("Cookies")
    .AddCookie("Cookies", options =>
    {
        options.LoginPath = "/Login/Index"; // Giriş sayfası
        options.AccessDeniedPath = "/Login/AccessDenied"; // Yetki reddi sayfası (isteğe bağlı)
    });

builder.Services.AddHttpClient();
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 2. Authentication ve Authorization middleware'lerini sırayla ekle
app.UseAuthentication(); // <- Bunu mutlaka Authorization'dan önce koy
app.UseAuthorization();

app.MapControllerRoute(
    name: "admin",
    pattern: "admin/{action=Index}/{id?}",
    defaults: new { controller = "Admin" });

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Default}/{action=Index}/{id?}");

app.Run();
