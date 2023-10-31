using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace asp_final_test.Models;

public class VaccinationSchedule
{
    [Key]
    public int Id { get; set; }

    [Required]
    public string Name { get; set; } = "";

    public int VaccineId { get; set; }
    [ValidateNever]
    public Vaccine Vaccine { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;

    [ValidateNever]
    public ICollection<VaccinationDate> VaccinationDates { get; set; }
}
