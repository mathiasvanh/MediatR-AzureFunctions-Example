using MediatRExample.Databases.Common;
using MediatRExample.Databases.Quiz.Models;
using Microsoft.EntityFrameworkCore;

namespace MediatRExample.Databases.Quiz;

public class QuizDbContext : BaseDbContext<QuizDbContext>
{
    public QuizDbContext(DbContextOptions<QuizDbContext> builderOptions) : base(builderOptions)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        builder.Entity<Question>().ToTable("Question");
        builder.Entity<Category>().ToTable("Category");

        base.OnModelCreating(builder);
    }
}