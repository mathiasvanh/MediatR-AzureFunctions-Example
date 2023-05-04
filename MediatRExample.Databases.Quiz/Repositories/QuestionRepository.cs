using MediatRExample.Databases.Common.Repositories;
using MediatRExample.Databases.Quiz.Models;

namespace MediatRExample.Databases.Quiz.Repositories;

public class QuestionRepository : Repository<Question, int, QuizDbContext>, IQuestionRepository
{
    public QuestionRepository(QuizDbContext context) : base(context)
    {
    }
}