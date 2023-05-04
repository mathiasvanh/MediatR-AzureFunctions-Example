using MediatRExample.Databases.Common.Repositories;
using MediatRExample.Databases.Quiz.Models;

namespace MediatRExample.Databases.Quiz.Repositories;

public interface IQuestionRepository: IRepository<Question, int>
{
}