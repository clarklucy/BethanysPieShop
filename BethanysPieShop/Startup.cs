using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BethanysPieShop.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace BethanysPieShop
{
    public class Startup
    {
        public IConfiguration Configuration { get;  }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration; 
        }

        //the methods in the startup class are called automatically by asp.net core and called by name - not overrides
        public void ConfigureServices(IServiceCollection services)
            //container
            //IServiceCollection is the Dependency Injection service that comes with asp.net core
            //register services here that we want to use in our application (dependency injection)
        {
            //register framework services
            services.AddDbContext<AppDbContext>(options =>
                    options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
            services.AddControllersWithViews(); //support for MVC - before written as AddMvc
            services.AddHttpContextAccessor();
            services.AddSession();

            //register our own services
            //AddTransient, AddSingleton, AddScoped - registration options based on the way objects are created and how long they live
            //----- registration of mock repositories as services
            //services.AddScoped<IPieRepository, MockPieRepository>();
            //services.AddScoped<ICategoryRepository, MockCategoryRepository>();
            //-----------

            //----- registration of real repositories as services
            services.AddScoped<IPieRepository, PieRepository>();
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<ShoppingCart>(sp => ShoppingCart.GetCart(sp));
           

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            //middleware components that intercept/handle incoming HTTP requests and produce an HTTP response
            //can alter or pass on the request to the next component in the pipeline - order is therefore important!

            //env does environment checks. This checks that if we are in development environment mode
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                //middleware to show the developer exception page
            }
            app.UseHttpsRedirection(); //redirects http to https
            app.UseStaticFiles(); //can serve static files e.g. images, css, javascript. By default searches directory wwwroot for static files
            app.UseSession();
            app.UseRouting();
            
            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");


                //------------------------------------------
                //---original code, response to every request is to write "hello world!"
                //endpoints.MapGet("/", async context =>
                //{
                //    await context.Response.WriteAsync("Hello World!");
                //}); 
                //----------------------------------------------
            });
        }
    }
}
