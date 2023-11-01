using asp_final_test.Models;
using asp_final_test.Repository.IRepository;
using asp_final_test.ViewModels;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asp_final_test.Areas.Admin.Controllers;

[Area("Admin")]
public class ScheduleController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public ScheduleController(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    [TempData] public string? SuccessMessage { get; set; }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Upsert(int? id)
    {
        VaccinationSchedule? schedule;

        if (id is null or 0)
        {
            schedule = new VaccinationSchedule();
        }
        else
        {
            schedule = _unitOfWork.VaccinationSchedule.GetById(id);
            if (schedule == null) return NotFound();
        }

        ViewBag.VaccineListItem = new SelectList(_unitOfWork.Vaccine.GetAll(), "Id", "Name", schedule.VaccineId);
        return View(schedule);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(int id, [Bind("Id, Name, VaccineId, VaccinationDates")] VaccinationSchedule schedule)
    {
        if (id != schedule.Id) return NotFound();

        ViewBag.VaccineListItem = new SelectList(_unitOfWork.Vaccine.GetAll(), "Id", "Name", schedule.VaccineId);

        ModelState.Remove("vaccinationDates");

        if (!ModelState.IsValid)
        {
            return View("upsert", schedule);
        }

        if (id == 0)
        {
            _unitOfWork.VaccinationSchedule.Insert(schedule);
            SuccessMessage = "New schedule added";
        }
        else
        {
            _unitOfWork.VaccinationSchedule.Update(schedule);
            SuccessMessage = "Schedule updated";
        }

        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }


    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        // var scheduleList = _unitOfWork.VaccinationSchedule.GetAll(includeProperties: "Vaccine,VaccinationDates");
        // var scheduleDtos = new List<VaccinationScheduleDto>();
        //
        // foreach (var schedule in scheduleList)
        // {
        //     var scheduleDto = new VaccinationScheduleDto()
        //     {
        //         Id = schedule.Id,
        //         Name = schedule.Name,
        //         VaccineName = schedule.Vaccine.Name,
        //         VaccinationDates = new List<VaccinationDateDto>()
        //     };
        //
        //     foreach (var date in schedule.VaccinationDates)
        //     {
        //         scheduleDto.VaccinationDates.Add(new VaccinationDateDto()
        //         {
        //             Id = date.Id,
        //             Date = date.Date
        //         });
        //     }
        //
        //     scheduleDtos.Add(scheduleDto);
        // }
        //
        // return Json(new { data = scheduleDtos });

        var scheduleList = _unitOfWork.VaccinationSchedule.GetAll(includeProperties: "Vaccine");
        var scheduleDtos = _mapper.Map<List<VaccinationScheduleDto>>(scheduleList);
        return Json(new { data = scheduleDtos });

    }


    [HttpDelete]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _unitOfWork.VaccinationSchedule.Delete(id);
        return Json(_unitOfWork.Save() > 0 ? new { success = true, message = "Schedule deleted" } : new { success = false, message = "Error while deleting" });
    }

    #endregion
}