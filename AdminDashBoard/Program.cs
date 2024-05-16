using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entiry.IdentityEntity;
using RealEstate.Domain.InterFace.Repository;
using RealEstate.Reopsitory.Context;
using RealEstate.Reopsitory.Repossitory;
using System.Reflection;

namespace AdminDashBoard
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<EstateContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("SQLConnection"));
            });
            builder.Services.AddAutoMapper(Assembly.GetExecutingAssembly());
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 10;
                options.Password.RequireNonAlphanumeric = true;
            }).AddEntityFrameworkStores<EstateContext>();
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
                pattern: "{controller=Admin}/{action=Login}/{id?}");

            app.Run();
        }
    }
}