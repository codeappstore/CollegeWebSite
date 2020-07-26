using AutoMapper;
using CollegeWebsite.DataAccess;
using CollegeWebsite.DataAccess.Models.Emails.Dtos;
using CollegeWebsite.DataAccess.Models.Emails.Services.IRepo;
using CollegeWebsite.DataAccess.Models.Emails.Services.Repo;
using CollegeWebsite.DataAccess.Models.Pages.Services.IRepo;
using CollegeWebsite.DataAccess.Models.Pages.Services.Repo;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Serialization;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;

namespace CollegeWebsite
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
            services.AddControllersWithViews();
            services.AddDbContext<CollegeDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("CollegeConnection"))
            );
            services.AddControllersWithViews().AddNewtonsoftJson(s =>
            {
                s.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            });

            services.AddMemoryCache();
            services.AddSession(s =>
            {
                s.IdleTimeout = System.TimeSpan.FromMinutes(60);
            });
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();

            // Setting Up Jwt Token
            var secretKey = Configuration.GetValue<string>("Secretkey");
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secretKey)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            #region StaticPagesRepo

            services.AddScoped<IStaticPagesRepo, StaticPagesRepo>();

            #endregion

            // Email Configuration Object
            services.Configure<EmailConfigDto>(Configuration.GetSection("EmailConfiguration"));

            #region StaticPagesRepo

            services.AddScoped<IEmailConfigRepo, EmailConfigRepo>();

            #endregion

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

            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
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
