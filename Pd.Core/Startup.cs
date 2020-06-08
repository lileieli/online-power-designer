using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pd.Core.Areas.Pd.Models.Base;
using Pd.Core.Extensions;
using Pd.Core.Helper;
using Pd.Core.Helper.Middleware;

namespace Pd.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            new Setting().Initial();
            Configuration = configuration;
        }


        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddRazorPages();
            services.AddControllersWithViews();
            services.AddDbContext<PDBaseContext>();//ע��EF����
            services.AddMvc(options => options.EnableEndpointRouting = false);//����ǰ��·�ɷ�ʽ 3.0Ҫ��������Ժ�Ͳ���ֱ�����ˣ�
            services.AddSession();//ע��Session
            //�����֤
            services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme).
             AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, o =>
             {
                 o.LoginPath = new PathString("/Home/Login");
             });
            services.AddMessage(Builder=>Builder.Tengxun());//ע�����
            //services.AddDbContext<PDBaseContext>(options =>
            //    {
            //        options.UseSqlServer(Setting.APPSett.Sqlconn);
            //    });




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
            app.UseHttpsRedirection();
            app.UseStaticFiles();//������ʾ�̬�ļ�
            app.UseRouting();
            app.UseMiddleware<LogMiddleware>();
            
            app.UseAuthorization();
            app.UseSession();
            //ʹ�������֤����ע�����UseMvcǰ�� ��UseEndpoints���ַ�ʽ�����ﶼ���С�������Ҫ�о�һ��
            app.UseAuthentication();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                name: "default",
                template: "{controller=Home}/{action=Index}/{id?}");

                routes.MapRoute(
                  name: "areas",
                  template: "{area:exists}/{controller=Login}/{action=Index}/{id?}"
                );

            });

            //app.UseEndpoints(endpoints =>
            //{
            //    endpoints.MapControllerRoute(
            //       name: "default",
            //       pattern: "{controller=Home}/{action=Index}/{id?}");

            //    endpoints.MapAreaControllerRoute(
            //        name: "areas", "areas",
            //        pattern: "{area:exists}/{controller=Login}/{action=Index}/{id?}");
            //    endpoints.MapRazorPages();
            //});

        }
    }
}
