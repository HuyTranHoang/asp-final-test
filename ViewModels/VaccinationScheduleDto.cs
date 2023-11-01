namespace asp_final_test.ViewModels;

public class VaccinationScheduleDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string VaccineName { get; set; }
    public List<string> vaccinationDates { get; set; }
}