using Microsoft.Build.Framework;

namespace asp_final_test.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}