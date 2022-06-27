using KaseyWebApi.ClientServices;
using KaseyWebApi.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Net.Http.Headers;
using Microsoft.OpenApi.Models;

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
        // Configure server controllers and endpoints
        services.AddControllers();
        services.AddEndpointsApiExplorer();

        // Add GitHub typed client service
        services.AddHttpClient<GitHubService>();

        services.AddDbContext<ApplicationDbContext>(options =>
            options
                .EnableSensitiveDataLogging()
                .UseNpgsql(
                    @"Host=localhost:5432;Username=postgres;Password=postgres;Database=asp_kc_db"
                ));

        // Configure Swagger OpenAPI services
        services.AddSwaggerGen(options =>
        {
            options.SwaggerDoc("v1", new OpenApiInfo
            {
                Title = "KC HTTP API",
                Version = "v1",
                Description = "The KC Service HTTP API"
            });
        });

        services.AddLogging(opt =>
        {
            opt.AddConsole();
            opt.AddDebug();
        });

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
            app.UseHsts();
            app.UseHttpsRedirection();
        }

        var pathBase = this.Configuration["PATH_BASE"];
        if (!string.IsNullOrEmpty(pathBase))
        {
            app.UsePathBase(pathBase);
        }

        var globalPathBase = new PathString("/api/v1");
        app.UsePathBase(globalPathBase);
        app.UseRouting();
        app.UseEndpoints(builder => builder.MapControllers());

        app.UseHttpLogging();

        // Globally add custom HTTP request header(s) to all server responses
        app.Use(async (context, next) =>
        {
            context.Response.Headers.Add("x-kasey-custom-header", "middleware response");

            await next();
        });

        app.UseAuthentication();
        // TODO: need to get local certs working
        // app.UseHttpsRedirection();
        app.UseSwagger()
            .UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "KC HTTP API V1");
                options.RoutePrefix = string.Empty;
            });
    }
}