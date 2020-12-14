
using System.Net;
using System.Text;
using AutoMapper;

using CargaClic.API.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using Microsoft.IdentityModel.Tokens;
using CargaClic.API.Data;
using Common;
using Api.Data;
using Api.Data.Interface;
using Api.Domain.Seguridad;
using Api.Handlers.Seguridad;
using API.Lectura.Interface.Seguridad;
using API.Lectura.Repository;
using Api.Domain.Mantenimiento;
using Api.Repository.Interface.Mantenimiento;
using Api.Repository.Repository.Mantenimiento;
using Api.ReadRepository.Interface.Mantenimiento;
using API.Lectura.Interface.Seguimiento;
using API.Lectura.Repository.Seguimiento;
using API.Escritura.Interface.Seguimiento;

namespace CargaClic.API
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
             services.AddDbContext<DataContext>(x=>x.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
             services.AddSingleton(_ => Configuration);
             services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
             services.AddCors();
             services.AddAutoMapper();
             
             services.AddTransient<Seed>();
          
             services.AddScoped<IAuthRepository,AuthRepository>();

             services.AddScoped< IRepository<RolUser>, Repository<RolUser>>();
             services.AddScoped< IRepository<User>, Repository<User>>();
             services.AddScoped< IRepository<Rol>, Repository<Rol>>();
             services.AddScoped< IRepository<RolPagina>, Repository<RolPagina>>();
             services.AddScoped< IRepository<Pagina>, Repository<Pagina>>();


             services.AddScoped<ISeguridadRepository,SeguridadRepository>();
             services.AddScoped<IClienteReadRepository,ClienteReadRepository>();
             services.AddScoped<IClienteRepository,ClienteRepository>();
             services.AddScoped<ISeguimientoReadRepository,SeguimientoReadRepository>();
             services.AddScoped<ISeguimientoRepository,SeguimientoRepository>();
             

             
             

             services.AddScoped< IRepository<ValorTabla>, Repository<ValorTabla>>();
             services.AddScoped< IRepository<Cliente>, Repository<Cliente>>();
             services.AddScoped< IRepository<Estado>, Repository<Estado>>();
             services.AddScoped< IRepository<Destinatario>, Repository<Destinatario>>();
             
             
             
            
             
             services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                        .AddJwtBearer(options => {
                            options.TokenValidationParameters = new TokenValidationParameters
                            {
                                ValidateIssuerSigningKey = true,
                                IssuerSigningKey = new SymmetricSecurityKey(
                                   Encoding.ASCII.GetBytes(Configuration
                                .GetSection("AppSettings:Token").Value)),
                                ValidateIssuer = false,
                                ValidateAudience = false                            
                            };
                        });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, Seed seeder,ILoggerFactory loggerFactory)
        {
            loggerFactory.AddConsole(Configuration.GetSection("Loggin"));
            loggerFactory.AddDebug();
            loggerFactory.AddFile("Logs/toscanos-{Date}.txt");
            

            

            if (env.IsDevelopment())
            {
                  app.UseExceptionHandler(builder=> { 
                    builder.Run(async context => {
                        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if(error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message); 
                            await context.Response.WriteAsync(error.Error.Message); 
                        }
                    });
                });
            }
            else
            {
                app.UseExceptionHandler(builder=> { 
                    builder.Run(async context => {
                        context.Response.StatusCode = (int) HttpStatusCode.InternalServerError;
                        var error = context.Features.Get<IExceptionHandlerFeature>();
                        if(error != null)
                        {
                            context.Response.AddApplicationError(error.Error.Message); 
                            await context.Response.WriteAsync(error.Error.Message); 
                        }
                    });
                });
               // app.UseHsts();
            }
            // app.UseHttpsRedirection();
            
            //seeder.SeedEstados();
           // seeder.SeedUsers();
            //seeder.SeedPaginas();
            //seeder.SeedRoles();
           // seeder.SeedRolPaginas();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseAuthentication();
            app.UseMvc();
        }
    }
}   

