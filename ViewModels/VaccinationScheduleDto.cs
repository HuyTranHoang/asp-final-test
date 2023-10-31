using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace asp_final_test.ViewModels;

public class VaccinationScheduleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string VaccineName { get; set; }
    public ICollection<VaccinationDateDto> VaccinationDates { get; set; }
}