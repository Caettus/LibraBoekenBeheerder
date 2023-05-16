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
        services.AddScoped<ICollection, Collection>();

        services.Register();
    }
}