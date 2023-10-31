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
        var schedules = _unitOfWork.VaccinationSchedule.GetAll(includeProperties: "Vaccine,VaccinationDates");
        return View(schedules);
    }

    public IActionResult Upsert(int? id)
    {
        VaccinationSchedule schedule;

        if (id is null or 0)
        {
            schedule = new VaccinationSchedule();
        }
        else
        {
            schedule = _unitOfWork.VaccinationSchedule.Get(s => s.Id == id, includeProperties: "VaccinationDates").FirstOrDefault();
            if (schedule == null) return NotFound();
        }

        ViewBag.VaccineListItem = new SelectList(_unitOfWork.Vaccine.GetAll(), "Id", "Name", schedule.VaccineId);
        return View(schedule);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(int id, [Bind("Id, Name, VaccineId")] VaccinationSchedule schedule,
        [Bind("vaccinationDates")] List<DateTime> vaccinationDates)
    {
        if (id != schedule.Id) return NotFound();

        ViewBag.VaccineListItem = new SelectList(_unitOfWork.Vaccine.GetAll(), "Id", "Name", schedule.VaccineId);
        ViewBag.VaccinationDates = vaccinationDates;

        if (vaccinationDates == null || vaccinationDates.Count == 0)
        {
            ViewBag.ErrorMessage = "Schedule needs at least 1 date";
            return View("upsert", schedule);
        }

        ModelState.Remove("vaccinationDates");

        if (!ModelState.IsValid)
        {
            return View("upsert", schedule);
        }

        if (id == 0)
        {
            _unitOfWork.VaccinationSchedule.Insert(schedule);
            _unitOfWork.Save();

            foreach (var date in vaccinationDates)
            {
                var newVaccinationDate = new VaccinationDate
                {
                    VaccinationScheduleId = schedule.Id,
                    Date = date
                };
                _unitOfWork.VaccinationDate.Insert(newVaccinationDate);
            }
            SuccessMessage = "New schedule added";
            _unitOfWork.Save();
            return RedirectToAction(nameof(Index));
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

        var scheduleList = _unitOfWork.VaccinationSchedule.GetAll(includeProperties: "Vaccine,VaccinationDates");
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