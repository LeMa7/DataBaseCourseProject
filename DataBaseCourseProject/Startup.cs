using DataBaseCourseProject.ComponentInterfaces;
using DataBaseCourseProject.Components;
using DataBaseCourseProject.Models.Tables;
using DataBaseCourseProject.Models.Views;
using DataBaseCourseProject.ServiceInterfaces;
using DataBaseCourseProject.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace DataBaseCourseProject
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
            services.AddTransient<ITableService<Producer>, ProducerTableService>();
            services.AddTransient<ITableService<User>, UserTableService>();
            services.AddTransient<ITableService<Category>, CategoryTableService>();
            services.AddTransient<ITableService<Subcategory>, SubcategoryTableService>();
            services.AddTransient<ITableService<Product>, ProductTableService>();
            services.AddTransient<ITableService<Review>, ReviewTableService>();
            services.AddTransient<ITableService<Order>, OrderTableService>();
            services.AddTransient<ITableService<OrderDetails>, OrderDetailsTableService>();
            services.AddTransient<ITableService<ShoppingCart>, ShoppingCartTableService>();
            services.AddTransient<ITableService<ShoppingCartDetails>, ShoppingCartDetailsTableService>();
            services.AddTransient<IViewService<ActiveOrderView>, ActiveOrderViewService>();
            services.AddTransient<IOracleComponent, OracleComponent>();
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
    }
}
