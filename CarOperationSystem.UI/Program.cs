using CarOperationSystem.DAL.Data;
using CarOperationSystem.DAL.Repository;
using CarOperationSystem.DAL.Repository.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.Extensions.Options;

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

            builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts => {

                opts.Password.RequireDigit = true;
                opts.Password.RequireLowercase = false;
                opts.Password.RequireNonAlphanumeric = false;
                opts.Password.RequireUppercase = false;
                opts.Password.RequiredLength = 6;

            }).AddEntityFrameworkStores<CustomDbContext>();

         
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
