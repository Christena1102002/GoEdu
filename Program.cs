using GoEdu.Data;
using Microsoft.AspNetCore.Mvc;
using GoEdu.Repositories;
using Microsoft.EntityFrameworkCore;
using GoEdu.Hubs;
using GoEdu.Models;
using Microsoft.AspNetCore.Identity;

namespace GoEdu
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            //add session
            builder.Services.AddSession(option =>
            {
                option.IdleTimeout = TimeSpan.FromMinutes(30);
            });
            // add context
            builder.Services.AddDbContext<GoEduContext>(option => {
                option.UseSqlServer(builder.Configuration.GetConnectionString("cs"));
            });
            // add identity service and connect identity store to out context
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequireDigit = false;
                options.Password.RequireNonAlphanumeric = false;


            })
               .AddEntityFrameworkStores<GoEduContext>();

            //register unit of work 
            builder.Services.AddScoped<UnitOfWork, UnitOfWork>();

            builder.Services.AddControllersWithViews(options =>
            {
                options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
            });

            builder.Services.AddSignalR();
            // SignalR Service for external applications and pages
            builder.Services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.AllowAnyMethod()
                    .AllowAnyHeader()
                    .SetIsOriginAllowed(url => true)
                    .AllowCredentials();

                });
            });
            var app = builder.Build();
            //roles 

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
            }
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapHub<CommentHub>("/CommentHub");
            #region Custom Route

            //app.MapControllerRoute("Route1", "{controller=Employee}/{action=Index}/{id?}");

            //app.MapControllerRoute("Route1", "r1/{age:int:range(20,60)}/{name}",
            //    new {controller="Route" ,action="Method1"});

            //app.MapControllerRoute("Route2", "r2",
            //    new { controller = "Route", action = "Method2" });
            #endregion

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
