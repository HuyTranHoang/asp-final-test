using asp_final_test.Models;
using asp_final_test.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace asp_final_test.Areas.Admin.Controllers;

[Area("Admin")]
public class VaccineController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public VaccineController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [TempData] public string? SuccessMessage { get; set; }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Upsert(int? id)
    {
        Vaccine vaccine;

        if (id is null or 0)
        {
            vaccine = new Vaccine();
        }
        else
        {
            vaccine = _unitOfWork.Vaccine.GetById(id);
            if (vaccine == null) return NotFound();
        }

        ViewBag.TypeListItem = new SelectList(_unitOfWork.Type.GetAll(), "Id", "Name",  vaccine.TypeId);
        return View(vaccine);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(int id, [Bind("Id, Name, ManufacturingCountry, ExpirationDate, Price, TypeId")] Vaccine vaccine)
    {
        if (id != vaccine.Id) return NotFound();

        Validation(vaccine);
        ViewBag.TypeListItem = new SelectList(_unitOfWork.Type.GetAll(), "Id", "Name", vaccine.TypeId);

        if (!ModelState.IsValid) return View("upsert", vaccine);

        if (id == 0)
        {
            _unitOfWork.Vaccine.Insert(vaccine);
            SuccessMessage = "New Vaccine added";
        }
        else
        {
            _unitOfWork.Vaccine.Update(vaccine);
            SuccessMessage = "Vaccine updated";
        }

        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    private void Validation(Vaccine vaccine)
    {
        var isCreate = vaccine.Id == 0;

        var categoryNameExist = _unitOfWork.Vaccine
            .Get(isCreate
                ? c => c.Name == vaccine.Name
                : c => c.Name == vaccine.Name && c.Id != vaccine.Id)
            .Any();

        if (categoryNameExist)
            ModelState.AddModelError("Name", "The vaccine name already exist");
    }


    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        var vaccineList = _unitOfWork.Vaccine.GetAll("Type");
        return Json(new { data = vaccineList });
    }

    [HttpDelete]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _unitOfWork.Vaccine.Delete(id);
        return Json(_unitOfWork.Save() > 0 ? new { success = true, message = "Vaccine deleted" } : new { success = false, message = "Error while deleting" });
    }

    #endregion
}