using System.ComponentModel.DataAnnotations;

namespace asp_final_test.Models;

public class VaccinationDate
{
    [Key]
    public int Id { get; set; }

    [Required]
    public DateTime Date { get; set; }

    public int VaccinationScheduleId { get; set; }
    public VaccinationSchedule VaccinationSchedule { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.Now;
}