using MediatRExample.Databases.Quiz.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MediatRExample.Databases.Quiz.Ioc;

public static class ServiceCollectionExtensions
{
    public static void AddQuizDatabase(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<QuizDbContext>(options => options.UseSqlServer(connectionString));
        services.AddTransient<IQuestionRepository, QuestionRepository>();
    }
}