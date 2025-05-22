using Microsoft.EntityFrameworkCore;
using ControlDeGastosNetCore.Models;
using ControlDeGastosNetCore.Services;
using ControlDeGastosNetCore.Repository;
using ControlDeGastosNetCore.Repositories;

namespace ControlDeGastosNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            builder.Services.AddScoped<IGastoRepository, GastoRepository>();
            builder.Services.AddScoped<IGastoService, GastoService>();
            builder.Services.AddScoped<IPresupuestoService, PresupuestoService>();
            builder.Services.AddScoped<IPresupuestoRepository, PresupuestoRepository>();

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
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
