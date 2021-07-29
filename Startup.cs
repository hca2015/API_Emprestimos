/*
 
BANCO DE DADOS:

docker run -d -e "ACCEPT_EULA=Y" -e "SA_PASSWORD=yourStrong(!)Password" -e "MSSQL_PID=Developer" -p 1433:1433 mcr.microsoft.com/mssql/server
 
Add-Migration Teste -Context BaseDbContext
Remove-Migration -Context BaseDbContext
 
 
 */

using API_Emprestimos.Repository;
using API_Emprestimos.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System;
using System.Text;

namespace API_Emprestimos
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
            services.AddCors(options => options.AddDefaultPolicy(policy =>
                policy
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
            //.SetIsOriginAllowed(_ => true)
            ));

            services.AddControllers().AddNewtonsoftJson(options =>
                options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "API_Emprestimos", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Autorize-se [IdentityController] e insira o token para testar os demais métodos",
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement {
                   {
                     new OpenApiSecurityScheme
                     {
                       Reference = new OpenApiReference
                       {
                         Type = ReferenceType.SecurityScheme,
                         Id = "Bearer"
                       }
                      },
                      Array.Empty<string>()
                    }
                  });
            });

            string lConnectionString = Configuration.GetConnectionString("MSSQL");

            string docker = Environment.GetEnvironmentVariable("AMBIENTE_DOCKER");

            if (docker == "SIM")
                lConnectionString = lConnectionString.Replace("localhost", "sqlserver");

            services.AddDbContext<IdentityContext>(options => options.UseSqlServer(lConnectionString));
            services.AddDbContext<BaseDbContext>(options => options.UseSqlServer(lConnectionString));

            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<IdentityContext>()
                .AddErrorDescriber<PortugueseIdentityErrorDescriber>()
                .AddDefaultTokenProviders();

            services.AddIdentityCore<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<IdentityContext>();

            services.Configure<IdentityOptions>(options =>
            {
                // Password settings.
                options.Password.RequireDigit = true;
                options.Password.RequireLowercase = true;
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequireUppercase = true;
                options.Password.RequiredLength = 8;
                options.Password.RequiredUniqueChars = 1;

                // Lockout settings.
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(7);
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.AllowedForNewUsers = true;

                // User settings.
                options.User.AllowedUserNameCharacters =
                    "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";
                options.User.RequireUniqueEmail = true;

                //Sign in settings
                //Op��es para caso enviemos um link de confirma��o via e-mail
                options.SignIn.RequireConfirmedAccount = false;
                options.SignIn.RequireConfirmedEmail = false;
                //Op��o para caso enviemos um link de confirma��o via SMS (pode usar o Amazon SNS)
                options.SignIn.RequireConfirmedPhoneNumber = false;
            });

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["jwt:key"])),
                    ClockSkew = TimeSpan.Zero
                };
            });

            services.AddAuthorization(options =>
            {
                AuthorizationPolicyBuilder defaultAuthorizationPolicyBuilder = new(JwtBearerDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                options.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            //Repositorios
            services.AddTransient<UsuarioRepository>();
            services.AddTransient<PedidoEmprestimoRepository>();
            services.AddTransient<OfertaEmprestimoRepository>();
            services.AddTransient<AceiteEmprestimoRepository>();

            //Services
            services.AddTransient<CancelarPedidoService>();
            services.AddTransient<CancelarOfertaService>();
            services.AddTransient<AceitarOfertaService>();

            //Contexto de execucao
            services.AddScoped(typeof(ContextoExecucao), (services) => new ContextoExecucao());
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "API_Emprestimos v1");
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            try
            {
#if !DEBUG
                Thread.Sleep(TimeSpan.FromSeconds(15)); //para o banco terminar de iniciar
#endif
                serviceProvider.GetService<IdentityContext>().Database.Migrate();
                serviceProvider.GetService<BaseDbContext>().Database.Migrate();
            }
#pragma warning disable CS0168 // Variable is declared but never used
            catch (Exception e)
#pragma warning restore CS0168 // Variable is declared but never used
            {
            }

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapGet("/", async context =>
                {
                    await context.Response.WriteAsync("Hello World from Emprestimo API!");
                });
            });
        }
    }
}
