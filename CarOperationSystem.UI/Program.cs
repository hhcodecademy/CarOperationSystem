using CarOperationSystem.DAL.Data;
using CarOperationSystem.DAL.Repository;
using CarOperationSystem.DAL.Repository.Interfaces;
using CarOperationSystem.UI.Configurations;
using CarOperationSystem.UI.Services;
using CarOperationSystem.UI.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;

namespace CarOperationSystem.UI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
            builder.Services.AddDbContext<CustomDbContext>(
            opts => opts.UseSqlServer(builder.Configuration.GetConnectionString("SqlConnectionString")));
            builder.Services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));

            IFileProvider physicalProvider = new PhysicalFileProvider(Directory.GetCurrentDirectory());
            builder.Services.AddSingleton<IFileProvider>(physicalProvider);

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts =>
            {

                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequiredLength = 6;

            }).AddEntityFrameworkStores<CustomDbContext>()
            .AddDefaultTokenProviders();

            builder.Services.Configure<DataProtectionTokenProviderOptions>(opts =>
            {
                opts.TokenLifespan = TimeSpan.FromHours(2);
            });
            builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("EmailSettings"));

            builder.Services.AddScoped<IEmailService, EmailService>();

            builder.Services.ConfigureApplicationCookie(cf =>
            {

                CookieBuilder cookieBuilder = new CookieBuilder()
                {
                    Name = "CarOperationUI",
                    Path = "/",


                };
                cf.Cookie = cookieBuilder;
                cf.ExpireTimeSpan = TimeSpan.FromDays(1);

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

            app.MapControllerRoute(
                  name: "areas",
                  pattern: "{area:exists}/{controller=Account}/{action=LogIn}/{id?}"
                );

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
