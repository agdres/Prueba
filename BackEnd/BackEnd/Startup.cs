using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Text;
// Uso SQL Server
using BackEnd.Models.DB;
using Microsoft.EntityFrameworkCore;
// Configuración Jwt
using BackEnd.Models.Configuraciones;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
// Servicios
using BackEnd.Services.Personas;
using BackEnd.Services.Usuarios;

namespace BackEnd
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

            // Configuración Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Prueba Técnica Double V Partners", Version = "v1" });

            });

            // Almacenar en IConfiguration la cadena de conexion
            var conectionString = Configuration.GetSection("ConnectionStrings");
            services.Configure<ConnectionStrings>(conectionString);

            #region Configuración conexión SQL Server y Politica de cords

            services.AddHttpContextAccessor();
            services.AddDbContext<Double_VContext>(options => options.UseSqlServer(Configuration.GetConnectionString("Conexion")), ServiceLifetime.Transient);

            services.AddCors(options => options.AddPolicy("AllowWebApp", builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod()));
            #endregion

            #region Configuracion JWT

            var appSettings = Configuration.GetSection("AppSettings");
            services.Configure<AppSettings>(appSettings);
            

            var _appSettings = appSettings.Get<AppSettings>();




            var keyMayor = Encoding.ASCII.GetBytes(_appSettings.Key);
            // Configuracion basica JWT
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x => {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(keyMayor),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            #endregion

            #region Configuración Interfaces
            // Asignación de Interfaces a clases para inyeccion de dependencias
            services.AddScoped<IPersonas, Personas_Serv>();
            services.AddScoped<IUsuarios, Usuarios_Serv>();
          
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "BackEnd v1"));
            }
            // Habilitacion de Cors
            app.UseCors("AllowWebApp");

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
