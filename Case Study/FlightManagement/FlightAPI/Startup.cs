using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Ocelot.DependencyInjection;
using Ocelot.Middleware;
using Ocelot.Provider.Consul;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightAPI
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
            
            var authenticationProviderKey = "TestKey";
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = authenticationProviderKey;
                //x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                //x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                //x.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })//JWT Bearer
                .AddJwtBearer(authenticationProviderKey,o =>
                {
                    
                    var key = Encoding.UTF8.GetBytes(Configuration["JWT:Key"]);
                    o.SaveToken = true;
                    o.RequireHttpsMetadata = false;
                    o.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["JWT:Issuer"],
                        ValidAudience = Configuration["JWT:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(key)
                    };
                });
            services.AddOcelot(Configuration).AddConsul();
            services.AddCors();
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(
                    builder =>
                    {
                        builder.WithOrigins("http://localhost:30414", "http://localhost:4200")
                                            .AllowAnyHeader()
                                            .AllowAnyMethod();
                    });
            });

        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors();
            app.UseCors(builder =>
            {
                builder
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader();
            });
            app.UseAuthentication();           
            app.UseOcelot().Wait();            
        }
    }
}
