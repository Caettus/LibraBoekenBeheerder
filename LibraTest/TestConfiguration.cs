using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;

public class TestConfiguration : IConfiguration
{
    private readonly IConfigurationRoot _configuration;

    public TestConfiguration()
    {
        var configBuilder = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json");

        _configuration = configBuilder.Build();
    }

    public string this[string key]
    {
        get { return _configuration[key]; }
        set { }
    }

    public IEnumerable<IConfigurationSection> GetChildren()
    {
        return _configuration.GetChildren();
    }

    public IChangeToken GetReloadToken()
    {
        return _configuration.GetReloadToken();
    }

    public IConfigurationSection GetSection(string key)
    {
        return _configuration.GetSection(key);
    }
}