using System;
using Microsoft.Extensions.Configuration;

namespace ZenkitAutoTests;

public class SecretsReader
{
    private IConfiguration _configuration { get; }
    
    public SecretsReader()
    {
        IConfigurationBuilder builder = new ConfigurationBuilder().AddJsonFile("secrets.json",false,true);
        _configuration = builder.Build();
    }

    public AccountData GetAccountCredentialsFromSecretJson()
    {
        return _configuration.GetSection("accountData").Get<AccountData>() 
               ?? throw new InvalidOperationException("Can't get accountData from secrets.json");
    }
}