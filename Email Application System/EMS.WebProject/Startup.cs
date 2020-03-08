using EMS.Data;
using EMS.Data.dbo_Models;
using EMS.Data.Seed;
using EMS.Services;
using EMS.Services.Contracts;
using EMS.Services.Factories;
using EMS.Services.Factories.Contracts;
using EMS.WebProject.BackgroundServices;
using GmailAPI;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EMS.WebProject
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
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddDbContext<SystemDataContext>(options =>
                options.UseSqlServer(this.Configuration.GetConnectionString(Constants.LocalDBConnection)));

            services.AddIdentity<UserDomain, IdentityRole>(options =>
                 options.Stores.MaxLengthForKeys = 128)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<SystemDataContext>()
                .AddDefaultUI()
                .AddDefaultTokenProviders();

            services.AddAuthorization(options =>
            {
                options.AddPolicy("IsPasswordChanged", policy => policy.RequireClaim("IsPasswordChanged", "True"));
            });

            services.AddScoped<IGmailAPIService, GmailAPIService>();
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IApplicationService, ApplicationService>();
            services.AddScoped<IUserFactory, UserFactory>();
            services.AddScoped<IApplicationFactory, ApplicationFactory>();

            services.AddHostedService<BackgroundService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, SystemDataContext context, UserManager<UserDomain> userManager, RoleManager<IdentityRole> roleManager)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseStatusCodePagesWithReExecute("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseAuthentication();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Email}/{action=Index}/{id?}");                
            });

            // Initial seeding
            AccountSeeder.Init(context, userManager, roleManager).Wait();
        }
    }
}
