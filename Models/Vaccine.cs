using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace asp_final_test.Models;

public class Vaccine
{
    public int Id { get; set; }

    [Microsoft.Build.Framework.Required]
    public string Name { get; set; } = "";

    [Microsoft.Build.Framework.Required]
    [DisplayName("Manufacturing Country")]
    public string ManufacturingCountry { get; set; } = "";

    [Microsoft.Build.Framework.Required]
    [DisplayName("Expiration Date")]

    public DateTime ExpirationDate { get; set; } = DateTime.Today;

    [Microsoft.Build.Framework.Required]
    [Range(0, double.MaxValue, ErrorMessage = "Price must be larger than zero")]
    public double Price { get; set; }

    [DisplayName("Category")]
    public int CategoryId { get; set; }
    [ValidateNever]
    public Category Category { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}

