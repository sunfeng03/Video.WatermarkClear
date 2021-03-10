using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Video.Watermark.Common;
using Video.Watermark.DataAccess;

namespace Video.Watermark.Web
{
    public class Startup
    {
        public IConfiguration _configuration { get; }
        public Startup(IWebHostEnvironment env)
        {
            var builder = new ConfigurationBuilder()
               .SetBasePath(Directory.GetCurrentDirectory())
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
             .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);
            ConfigManager.InitConfigurantion(builder.Build());
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllersWithViews();
            //解决core序列化首字母默认小写的问题
            services.AddControllersWithViews().AddJsonOptions(p =>
            {
                p.JsonSerializerOptions.PropertyNamingPolicy = null;
            });
            //解决ViewBag的中文编码问题
            services.AddSingleton(HtmlEncoder.Create(UnicodeRanges.All));

            services.AddRazorPages().AddRazorRuntimeCompilation();
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
            }
            InitDbConnectionStringConfig();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

        /// <summary>
        /// 初始化数据库连接字符串
        /// </summary>
        private void InitDbConnectionStringConfig()
        {
            var dbConnectionStringConfig = new DbConnectionStringConfig();

            dbConnectionStringConfig.MyShopConnectionString = ConfigManager.Configuration["ConnectionStrings:MyShop"];
            DbConnectionStringConfig.InitDefault(dbConnectionStringConfig);

        }
    }
}
