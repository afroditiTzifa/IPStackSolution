using AutoMapper;
using Lib.Data.Entities;
using Lib.Data.Repositories;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace WebApi {
    public class Startup {
        public Startup (IConfiguration configuration) {
            Configuration = configuration;

        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices (IServiceCollection services) {

            services.AddDbContext<IPStackDB> (options => options.UseSqlServer (Configuration.GetConnectionString ("DefaultConnection")));
            services.AddScoped<IIPDetailsDataProvider, IPDetailsJsonDataProvider> ();
            services.AddAuthorization ();
            services.AddControllers ();
            services.AddMemoryCache ();
            services.AddAutoMapper (typeof (Startup));
            services.Configure<AppSettings> (Configuration.GetSection ("AppSettings"));

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment ()) {
                app.UseDeveloperExceptionPage ();
            }

            app.UseHttpsRedirection ();

            app.UseRouting ();

            app.UseAuthorization ();

            app.UseEndpoints (endpoints => {
                endpoints.MapControllers ();
            });
        }
    }
}