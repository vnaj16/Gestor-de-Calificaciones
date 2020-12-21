using AutoMapper;
using Gestor_de_Notas.Persistance;
using Gestor_de_Notas.Service;
using Gestor_de_Notas.Service.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Gestor_de_Notas
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
            services.AddControllers();



            services.AddDbContext<ApplicationDbContext>(
              opts => opts.UseSqlServer(Configuration.GetConnectionString("Flavio"))
               );
            //services.AddDbContext<ApplicationDbContext>(
            //    opts => opts.UseNpgsql(Configuration.GetConnectionString("FlavioP"))
            //     );
            services.AddAutoMapper(typeof(Startup)); ///utilizar automapper

            services.AddTransient<CicloService, CicloServiceI>();
            services.AddTransient<CursoService, CursoServiceI>();
            services.AddTransient<CampoService, CampoServiceI>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
