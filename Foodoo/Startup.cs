using System;
using Foodoo.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Foodoo
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private readonly string foodooDb = "Server=localhost;Uid=root;Database=foodoodb;Pwd=root;";
        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddDbContext<FoodooContext>(options =>
                options.UseMySql(foodooDb, ServerVersion.AutoDetect(foodooDb)));
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "Foodoo", Version = "v1"}); });
            services.AddCors(options =>
            {
                options.AddPolicy("AllowCORS",
                    builder =>
                    {
                        builder.WithOrigins("localhost:3000",
                            "95.96.133.218:3000"
                        );
                    });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Foodoo v1"));
            }
            
            using (IServiceScope scope = app.ApplicationServices.CreateScope())
            using (var context = scope.ServiceProvider.GetService<FoodooContext>()) 
                context.Database.EnsureCreated();
  
            app.UseStaticFiles();  
  
            app.UseCors(x => x.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(origin => true).AllowCredentials());
            
            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapControllerRoute(  
                    name:"default",  
                    pattern:"{controller=recipe}/{action=index}/{id?}"  
                );  
            });
        }
    }
}