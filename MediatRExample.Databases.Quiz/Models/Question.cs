using System.ComponentModel.DataAnnotations.Schema;
using MediatRExample.Databases.Common.Entities;

namespace MediatRExample.Databases.Quiz.Models;

public class Question : Entity<int>
{
    public string? Text { get; set; }

    public int? CategoryId { get; set; }

    [ForeignKey("CategoryId")]
    public Category? Category { get; set; }
}