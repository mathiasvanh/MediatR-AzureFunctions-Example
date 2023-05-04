using MediatRExample.Databases.Quiz.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace MediatRExample.Databases.Quiz.Configurations;

public class CategoryConfiguration: IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        builder
            .Property(Category => Category.Id)
            .HasColumnName("CategoryId");
    }
}