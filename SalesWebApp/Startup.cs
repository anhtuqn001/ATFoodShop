using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using SalesWebApp.Models;

namespace SalesWebApp
{
    public class Startup
    {
        private IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContextPool<AppDbContext>(
            //    options => options.UseSqlServer(_config.GetConnectionString("d")));
            //services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(options =>
            //{
            //    options.UseNpgsql(_config.GetConnectionString("DefaultConnection"));
            //});
            services.AddEntityFrameworkNpgsql().AddDbContext<AppDbContext>(options =>
            {
                string conStr = null;
                string dataUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
                if (dataUrl != null)
                {
                    var databaseUri = new Uri(dataUrl);
                    var userInfo = databaseUri.UserInfo.Split(':');

                    var builder = new NpgsqlConnectionStringBuilder
                    {
                        Host = databaseUri.Host,
                        Port = databaseUri.Port,
                        Username = userInfo[0],
                        Password = userInfo[1],
                        Database = databaseUri.LocalPath.TrimStart('/'),
                        SslMode = SslMode.Require,
                        TrustServerCertificate = true
                    };
                    conStr = builder.ToString();
                }
                options.UseNpgsql(conStr);
            }
            );

            services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {
                options.Password.RequiredLength = 10;
            }).AddEntityFrameworkStores<AppDbContext>();

            services.AddMvc(options => {
                var policy = new AuthorizationPolicyBuilder()
                                .RequireAuthenticatedUser()
                                .Build();
                options.Filters.Add(new AuthorizeFilter(policy));
            }).AddXmlDataContractSerializerFormatters();
            services.AddScoped<IFoodRepository, SQLFoodRepository>();
            services.AddScoped<IShoppingCartRepository, SQLShoppingCartRepository>();
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped(sp => ShoppingCart.GetCart(sp));
            services.AddSession();
            services.AddMemoryCache();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            UpdateDatabase(app);

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseStatusCodePagesWithReExecute("/Error/{0}");
            }

            app.UseFileServer();
            app.UseSession();
            app.UseAuthentication();
            app.UseMvc(routes => {
                routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
            });

        }

        private static void UpdateDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices
                .GetRequiredService<IServiceScopeFactory>()
                .CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<AppDbContext>())
                {
                    context.Database.Migrate();
                }
            }
        }
    }
}
