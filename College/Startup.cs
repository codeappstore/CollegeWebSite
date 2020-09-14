using College.Access.IRepository;
using College.Access.Repository;
using College.Database.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.Extensions.Hosting;
using Newtonsoft.Json.Serialization;

namespace College
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
            services.AddControllersWithViews().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddDbContext<CollegeContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
            );

            services.AddMemoryCache();
            services.AddSession(s =>
            {
                s.IdleTimeout = System.TimeSpan.FromMinutes(60);
            });
            services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            #region Repository Registery

            #region Auth

            services.AddScoped<IAuthRepo, AuthRepo>();

            #endregion

            #region Role

            services.AddScoped<IRoleRepo, RoleRepo>();

            #endregion

            #region Privilege

            services.AddScoped<IPrivilegeRepo, PrivilegeRepo>();

            #endregion

            #region Access

            services.AddScoped<IAccessRepo, AccessRepo>();

            #endregion

            #region Email

            services.AddScoped<IEmailRepo, EmailRepo>();

            #endregion

            #region Layout

            services.AddScoped<ILayoutRepo, LayoutRepo>();

            #endregion

            #region Frontend

            services.AddScoped<IFrontEndRepo, FrontEndRepo>();

            #endregion

            #region Downloads

            services.AddScoped<IDownloadsRepo, DownloadsRepo>();

            #endregion

            #region Gallery

            services.AddScoped<IGalleryRepo, GalleryRepo>();

            #endregion

            #endregion

            services.AddProgressiveWebApp();

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.Use(async (context, next) =>
            {
                await next();
                if (context.Response.StatusCode <= 400 && context.Response.StatusCode >= 500)
                {
                    context.Request.Path = "/Error";
                    await next();
                }
                if (context.Response.StatusCode == 500)
                {
                    context.Request.Path = "/ErrorIndex500";
                    await next();
                }
            });

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSession();


            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
