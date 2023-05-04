using MediatRExample.Databases.Quiz.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediatRExample.Databases.Quiz.Configurations;

public class QuestionConfiguration: IEntityTypeConfiguration<Question>
{
    public void Configure(EntityTypeBuilder<Question> builder)
    {
        builder
            .Property(Question => Question.Id)
            .HasColumnName("QuestionId");
    }
}