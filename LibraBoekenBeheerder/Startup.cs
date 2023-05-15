using Microsoft.Extensions.Configuration;
using LibraLogic;
using System.Collections;
using LibraBoekenBeheerder.Controllers;

public class Startup
{
    private readonly IConfiguration _configuration;

    public Startup(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        // Other service registrations...

        services.AddScoped<Collection>(); // Add this line to register the Collection class
        services.AddScoped<CollectionsMapper>();
        services.AddScoped<CollectionsController>();

        // Other registrations...
    }

}