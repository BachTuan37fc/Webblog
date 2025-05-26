// // using aznews.Models;
// // using Microsoft.EntityFrameworkCore;
// // using Microsoft.Extensions.Options;

// // var builder = WebApplication.CreateBuilder(args);

// // builder.Services.AddAuthentication("MyCookie")
// //     .AddCookie("MyCookie", options =>
// //     {
// //         options.LoginPath = "/User/Login";
// //         options.LogoutPath = "/User/Logout";
// //     });

// // builder.Services.AddAuthorization(options =>
// // {
// //     options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
// //     options.AddPolicy("DocGiaOnly", policy => policy.RequireRole("docgia"));
// // });
// // var connection = builder.Configuration.GetConnectionString("DefaultConnection");
// // builder.Services.AddDbContext<DataContext>(Options => Options.UseSqlServer(connection));
// // // Add services to the container.
// // builder.Services.AddControllersWithViews();

// // var app = builder.Build();
// // app.UseAuthentication();
// // app.UseAuthorization();
// // // Configure the HTTP request pipeline.
// // if (!app.Environment.IsDevelopment())
// // {
// //     app.UseExceptionHandler("/Home/Error");
// //     // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
// //     app.UseHsts();
// // }

// // app.UseHttpsRedirection();
// // app.UseStaticFiles();

// // app.UseRouting();

// // app.UseAuthorization();

// // app.MapControllerRoute(
// //     name: "areas",
// //     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// // app.MapControllerRoute(
// //     name: "default",
// //     pattern: "{controller=Home}/{action=Index}/{id?}");

// // app.Run();

// // builder.Services.AddDbContext<DataContext>(options =>
// //     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
// using aznews.Models;
// using Microsoft.EntityFrameworkCore;
// using Microsoft.Extensions.Options;

// var builder = WebApplication.CreateBuilder(args);

// builder.Services.AddAuthentication("MyCookie")
//     .AddCookie("MyCookie", options =>
//     {
//         options.LoginPath = "/Login";
//         options.LogoutPath = "/Login";
//     });

// builder.Services.AddAuthorization(options =>
// {
//     options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
//     options.AddPolicy("DocGiaOnly", policy => policy.RequireRole("docgia"));
// });


// // Cấu hình DbContext
// builder.Services.AddDbContext<DataContext>(options =>
//     options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// // Thêm dịch vụ MVC
// builder.Services.AddControllersWithViews();

// var app = builder.Build();

// // app.UseMiddleware<AuthMiddleware>();

// // if (!app.Environment.IsDevelopment())
// // {
// //     app.UseExceptionHandler("/Home/Error");
// //     app.UseHsts();
// // }

// // app.UseHttpsRedirection();
// // app.UseStaticFiles();

// // app.UseRouting();

// // app.UseAuthorization();



// // Cấu hình pipeline
// // if (app.Environment.IsDevelopment())
// // {
// //     app.UseDeveloperExceptionPage();
// // }
// // else
// // {
// //     app.UseExceptionHandler("/Home/Error");
// //     app.UseHsts();
// // }

// // app.UseHttpsRedirection();
// // app.UseStaticFiles();
// // app.UseRouting();
// // app.UseAuthentication();
// // app.UseAuthorization();

// // Cấu hình routing
// app.MapControllerRoute(
//     name: "areas",
//     pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

// app.MapControllerRoute(
//     name: "default",
//     pattern: "{controller=Home}/{action=Index}/{id?}");

// app.Run();
using aznews.Models;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình xác thực
builder.Services.AddAuthentication("MyCookie")
    .AddCookie("MyCookie", options =>
    {
        options.LoginPath = "/Login";
        options.LogoutPath = "/Login/Logout";
        options.Cookie.Name = "MyCookie";
        options.ExpireTimeSpan = TimeSpan.FromMinutes(30);
    });

// Cấu hình phân quyền
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("AdminOnly", policy => policy.RequireRole("admin"));
    options.AddPolicy("DocGiaOnly", policy => policy.RequireRole("docgia"));
});

// Cấu hình DbContext
builder.Services.AddDbContext<DataContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

// Thêm dịch vụ MVC
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Cấu hình pipeline
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

// Cấu hình routing
app.MapControllerRoute(
    name: "login",
    pattern: "Login",
    defaults: new { controller = "Login", action = "Login" });
    
app.MapControllerRoute(
    name: "areas",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");


app.MapControllerRoute(
     name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.Run();