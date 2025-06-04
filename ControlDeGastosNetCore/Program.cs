using System.Globalization;
using ControlDeGastosAPI.Services;
using ControlDeGastosMVC.Controllers;
using ControlDeGastosNetCore.Controllers;

namespace ControlDeGastosNetCore
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();

            builder.Services.AddHttpClient<GastoService>(client =>
            {
                var configuration = builder.Configuration;
                var baseUrl = configuration.GetValue<string>("ApiSettings:BaseUrl");
                client.BaseAddress = new Uri(baseUrl);
            });

            builder.Services.AddHttpClient<PresupuestoController>();
            builder.Services.AddHttpClient<CategoriaController>();
            builder.Services.AddHttpClient<GastosController>();

            var cultureInfo = new CultureInfo("es-AR");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

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
