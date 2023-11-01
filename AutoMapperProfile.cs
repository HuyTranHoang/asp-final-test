using asp_final_test.Models;
using asp_final_test.ViewModels;
using AutoMapper;



namespace asp_final_test;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<VaccinationSchedule, VaccinationScheduleDto>()
            .ForMember(dest => dest.vaccinationDates,
                opt => opt.MapFrom(src => GetVaccinationDatesDto(src)));
    }

    private List<string> GetVaccinationDatesDto(VaccinationSchedule vaccinationSchedule)
    {
        var inputString = vaccinationSchedule.VaccinationDates;
        char[] delimiterChars = { ';' };
        var stringArray = inputString.Split(delimiterChars, StringSplitOptions.RemoveEmptyEntries);
        return stringArray.ToList();
    }
}