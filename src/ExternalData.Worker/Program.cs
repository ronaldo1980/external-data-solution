using ExternalData.Infrastructure;

var builder = Host.CreateApplicationBuilder(args);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
                      ?? "Data Source=externaldata.db";

builder.Services.AddInfrastructure(connectionString);

builder.Services.AddHttpClient("awesome-api", client =>
{
    client.BaseAddress = new Uri("https://economia.awesomeapi.com.br/");
    client.Timeout = TimeSpan.FromSeconds(30);
});

builder.Services.AddHostedService<ExternalData.Worker.Worker>();

var host = builder.Build();

host.Run();