using asp_final_test.Models;
using asp_final_test.Repository.IRepository;
using asp_final_test.ViewModels;
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
        var vaccines = _unitOfWork.Vaccine.GetAll();

        return View(vaccines);
    }

    public IActionResult Upsert(int? id)
    {
        var vaccineDto = new VaccineDto();

        if (id is null or 0)
        {
            vaccineDto.Vaccine = new Vaccine();
        }
        else
        {
            vaccineDto.Vaccine = _unitOfWork.Vaccine.GetById(id);
            if ( vaccineDto.Vaccine == null) return NotFound();
        }

        vaccineDto.CategoryListItem = new SelectList(_unitOfWork.Category.GetAll(), "Id", "Name",  vaccineDto.Vaccine.CategoryId);
        return View(vaccineDto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(int id, [Bind("Vaccine")] VaccineDto vaccineDto)
    {
        if (id != vaccineDto.Vaccine.Id) return NotFound();

        Validation(vaccineDto.Vaccine);
        vaccineDto.CategoryListItem = new SelectList(_unitOfWork.Category.GetAll(), "Id", "Name", vaccineDto.Vaccine.CategoryId);

        if (!ModelState.IsValid) return View("upsert", vaccineDto);

        if (id == 0)
        {
            _unitOfWork.Vaccine.Insert(vaccineDto.Vaccine);
            SuccessMessage = "New Vaccine added";
        }
        else
        {
            _unitOfWork.Vaccine.Update(vaccineDto.Vaccine);
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
        var vaccineList = _unitOfWork.Vaccine.GetAll(includeProperties: "Category");
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