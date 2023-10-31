using asp_final_test.Models;
using asp_final_test.ViewModels;
using AutoMapper;



namespace asp_final_test;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<VaccinationSchedule, VaccinationScheduleDto>()
            .ForMember(dest => dest.VaccinationDates,
                opt => opt.MapFrom(src => GetVaccinationDatesDto(src)));
    }

    private List<VaccinationDateDto> GetVaccinationDatesDto(VaccinationSchedule vaccinationSchedule)
    {
        var vaccinationDateDtos = new List<VaccinationDateDto>();

        foreach (var date in vaccinationSchedule.VaccinationDates)
        {
            var vaccinationDateDto = new VaccinationDateDto()
            {
                Id = date.Id,
                Date = date.Date
            };

            vaccinationDateDtos.Add(vaccinationDateDto);
        }

        return vaccinationDateDtos;
    }
}