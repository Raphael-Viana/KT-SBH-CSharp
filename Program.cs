using KTCSharpApi;

static IHostBuilder CreateHostBuilder(string[] args)
{
    return Microsoft.Extensions.Hosting.Host
        .CreateDefaultBuilder(args)
        .ConfigureWebHostDefaults(GetWebHostBuilder);
}
                                                                                    
static void GetWebHostBuilder(IWebHostBuilder webBuilder)
{
    webBuilder.UseStartup<Startup>();
}

var builder = CreateHostBuilder(args);
var app = builder.Build();

app.Run();