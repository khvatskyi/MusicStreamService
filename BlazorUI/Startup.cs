using System;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BlazorUI.Services;
using System.Net.Http;
using BlazorUI.Interfaces.IServices;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using BlazorUI.Data;

namespace BlazorUI
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient(client =>
            new HttpClient
            {
                BaseAddress = new Uri(Configuration.GetSection("AppSettings")["API_Address"])
            });
            services.AddHttpClient();
            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextAccessor>();

            #region Blazor Cookie
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                .AddCookie(options =>
                {
                    options.LoginPath = new PathString("/Account/Login");
                });

            services.AddHttpContextAccessor();
            services.AddScoped<HttpContextAccessor>();
            #endregion

            services.AddRazorPages();
            services.AddServerSideBlazor();

            #region Services
            services.AddTransient<IAccountService, AccountService>();
            services.AddTransient<IAlbumService, AlbumService>();
            services.AddScoped<MusicService>();
            services.AddScoped<PlaylistService>();
            #endregion

            services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseCookiePolicy();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
