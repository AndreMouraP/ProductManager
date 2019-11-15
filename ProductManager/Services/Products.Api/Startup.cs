using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Abstrations;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Model.Contexts;
using Model.Entities;
using Products.Api.Factory;
using Products.Api.Mapper;
using Products.Api.Services;
using Products.Api.ViewModel;

namespace Products.Api
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
            var ConnectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING_PRODUCTMANAGER");

            services.AddSingleton<IMapper<Category, CategoryViewModel>,CategoryViewModelMapper>();
            services.AddSingleton<IMapper<Product, ProductViewModel>, ProductViewModelMapper>();
            services.AddSingleton<IService<CategoryViewModel>,CategoryService>();
            services.AddSingleton<IService<ProductViewModel>, ProductService>();

            services.AddSingleton<IContextFactory>(s => 
                new ContextFactory(new DbContextOptionsBuilder<ProductContext>().UseSqlServer(ConnectionString).Options));

            // Register the Swagger services
            services.AddSwaggerDocument(config =>
            {
                config.PostProcess = document =>
                {
                    document.Info.Version = "v1";
                    document.Info.Title = "Products";
                    document.Info.Description = "Service responsible for products and galeries interaction";
                };
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            // Register the Swagger generator and the Swagger UI middlewares
            app.UseOpenApi();
            app.UseSwaggerUi3();

            //app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
