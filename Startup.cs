using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using AutoMapper;
using ReferenceOfPerson.Core;
using ReferenceOfPerson.Mapping;
using ReferenceOfPerson.Persistence;
//using ReferenceOfPerson.Migrations;

namespace ReferenceOfPerson
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddAutoMapper(config => config.AddProfile(new MappingProfile()));
            services.AddScoped<ReferenceOfPersonDbContext>();
            services.AddScoped<IUnitOfWork,UnitOfWork>();
            //services.AddScoped<IPersonRepository,PersonRepository>();
            //services.AddScoped<IRelationshipRepository, RelationshipRepository>();
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
                app.UseExceptionHandler("/Error");
            }

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });


        }

    }
}
