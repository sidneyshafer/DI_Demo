using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WazeCredit.Data;
using WazeCredit.Middleware;
using WazeCredit.Models;
using WazeCredit.Service;
using WazeCredit.Service.LifeTimeServices;
using WazeCredit.Utility.AppSettingsClasses;
using WazeCredit.Utility.DI_Config;
using static System.Net.Mime.MediaTypeNames;

namespace WazeCredit
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));
            services.AddDatabaseDeveloperPageExceptionFilter();

            services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();

            // Register application services in container
            services.AddTransient<IMarketForecaster, MarketForecasterV2>();
            // Checks to see if an implementation for that service already exists.
            services.TryAddTransient<IMarketForecaster, MarketForecaster>();
            // Replaces the previous implementation with new implementation.
            services.Replace(ServiceDescriptor.Transient<IMarketForecaster, MarketForecaster>());

            // Removes all implementation services for the IMarketForecaster.
            // services.RemoveAll<IMarketForecaster>();

            // Register services for lifetime classes
            services.AddTransient<TransientService>();
            services.AddScoped<ScopedService>();
            services.AddSingleton<SingletonService>();

            // Register services for validation
            // services.AddScoped<IValidationChecker, AddressValidationChecker>();
            // services.AddScoped<IValidationChecker, CreditValidationChecker>();

            // services.TryAddEnumerable(ServiceDescriptor.Scoped<IValidationChecker, AddressValidationChecker>());
            // services.TryAddEnumerable(ServiceDescriptor.Scoped<IValidationChecker, CreditValidationChecker>());

            services.TryAddEnumerable(new[]
            {
                ServiceDescriptor.Scoped<IValidationChecker, AddressValidationChecker>(),
                ServiceDescriptor.Scoped<IValidationChecker, CreditValidationChecker>()
            });

            services.AddScoped<ICreditValidator, CreditValidator>();

            // Register services for credit approval
            services.AddScoped<CreditApprovedHigh>();
            services.AddScoped<CreditApprovedLow>();

            services.AddScoped<Func<CreditApprovedEnum, ICreditApproved>>(ServiceProvider => range =>
            {
                switch (range)
                {
                    case CreditApprovedEnum.High:
                        return ServiceProvider.GetService<CreditApprovedHigh>();
                    case CreditApprovedEnum.Low:
                        return ServiceProvider.GetService<CreditApprovedLow>();
                    default:
                        return ServiceProvider.GetService<CreditApprovedLow>();
                }
            });

            // Configure/Register app setting services in container
            services.AddAppSettingsConfig(Configuration);

            services.AddControllersWithViews().AddRazorRuntimeCompilation();
            services.AddRazorPages();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILoggerFactory loggerFactory)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            // Configure for logging to file
            loggerFactory.AddFile("logs/creditApp-log-{Date}.txt");

            app.UseAuthentication();
            app.UseAuthorization();

            // Register custom middleware
            app.UseMiddleware<CustomMiddleware>();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}
