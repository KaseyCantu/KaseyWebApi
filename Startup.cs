using System.Text;
using KaseyWebApi.ClientServices;
using KaseyWebApi.Context;
using KaseyWebApi.Interfaces;
using KaseyWebApi.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Npgsql;

namespace KaseyWebApi;

public class Startup
{
    public Startup(IConfiguration configuration)
    {
        this.Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
        // Configure Authentication
        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = this.Configuration["Jwt:Audience"],
                    ValidIssuer = this.Configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(this.Configuration["Jwt:Key"]))
                };
            });

        // Configure server controllers and endpoints
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // Add GitHub typed client service
        services.AddHttpClient<GitHubService>();

        var connectionStringBuilder =
            new NpgsqlConnectionStringBuilder(this.Configuration.GetConnectionString("DefaultConnection"));

        connectionStringBuilder.Username = this.Configuration["DbUsername"];
        connectionStringBuilder.Password = this.Configuration["DbPassword"];
        var connectionString = connectionStringBuilder.ConnectionString;

        // Configure Database context service
        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .EnableSensitiveDataLogging()
                .UseNpgsql(connectionString));

        // Configure transient services
        services.AddTransient<IUsers, UserRepository>();

        services.AddTransient<IEmployees, EmployeeRepository>();

        // Configure Swagger OpenAPI services
        services.AddSwaggerGen(options =>
        {
            options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                Type = SecuritySchemeType.ApiKey,
                Scheme = "Bearer",
                BearerFormat = "JWT",
                In = ParameterLocation.Header,
                Description = "JWT Authorization header using the Bearer scheme."
            });

            options.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        }
                    },
                    new string[] { }
                }
            });

            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "KC HTTP API",
                Version = "v1",
                Description = "The KC Service HTTP API"
            });
        });

        // Configure logging services
        services.AddLogging(opt =>
        {
            opt.AddConsole();
            opt.AddDebug();
        });

        // Configure MVC functionality
        services.AddMvc();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(
        IApplicationBuilder app,
        IHostEnvironment env,
        ILoggerFactory loggerFactory
    )
    {
        if (!env.IsDevelopment())
        {
            Console.WriteLine("ENV IS NOT DEVELOPMENT");
            app.UseHsts();
            app.UseHttpsRedirection();
        }

        var pathBase = this.Configuration["PATH_BASE"];
        if (!string.IsNullOrEmpty(pathBase))
        {
            app.UsePathBase(pathBase);
        }

        // var globalPathBase = new PathString("/api/v1");
        // app.UsePathBase(globalPathBase);

        app.UseRouting();

        // Enforce authentication on web api service - JWT bearer token based
        app.UseAuthentication();
        app.UseAuthorization();

        app.UseEndpoints(builder => builder.MapControllers());

        app.UseHttpLogging();

        // Globally add custom HTTP request header(s) to all server responses
        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("x-kasey-custom-header", "middleware response");

            await next();
        });

        // TODO: need to get local certs working
        // app.UseHttpsRedirection();
        app.UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "KC HTTP API V1");
                options.RoutePrefix = "swagger";
            });
    }
}