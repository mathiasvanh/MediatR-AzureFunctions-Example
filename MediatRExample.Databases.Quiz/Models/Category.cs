using MediatRExample.Databases.Common.Entities;

namespace MediatRExample.Databases.Quiz.Models;

public class Category : Entity<int>
{
    public string Name { get; set; } = string.Empty;
}