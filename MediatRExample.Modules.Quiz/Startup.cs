using MediatRExample.Common.Modules;
using MediatRExample.Databases.Quiz.Ioc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediatRExample.Modules.Quiz;

public class Startup : ModuleStartup
{
    public override void ConfigureServices(IServiceCollection services, IConfiguration configuration)
    {
        services.AddQuizDatabase(configuration.GetConnectionString("QuizDatabase"));
    }
}