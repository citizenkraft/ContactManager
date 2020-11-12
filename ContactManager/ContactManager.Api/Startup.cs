using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ContactManager.Api.Profiles;
using ContactManager.Core.Components;
using ContactManager.Core.Repositories;
using ContactManager.Data.Sql.Repositories;
using ContactManager.Sql;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace ContactManager.Api
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
            services.AddControllers().AddJsonOptions(c => { c.JsonSerializerOptions.IgnoreNullValues = true; });

            //Register DataContext
            services.AddDbContext<ContactManagerDataContext>(options => options.UseSqlServer(Configuration["ConnectionStrings:DefaultConnection"]));

            //Register repositories
            services.AddScoped<IContactRepository, ContactRepository>();

            //Register Components
            services.AddScoped<IContactComponent, ContactComponent>();

            //Register Others
            services.AddSwaggerGen();
            

            services.AddAutoMapper(typeof(MapperProfile));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stadium Express V1");
            });
        }
    }
}
