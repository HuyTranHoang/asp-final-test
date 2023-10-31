using asp_final_test.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;
using Type = asp_final_test.Models.Type;

namespace asp_final_test.Areas.Admin.Controllers;

[Area("Admin")]
public class TypeController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public TypeController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    [TempData] public string? SuccessMessage { get; set; }

    public IActionResult Index()
    {
        var types = _unitOfWork.Type.GetAll();

        return View(types);
    }

    public IActionResult Upsert(int? id)
    {
        Type type;

        if (id is null or 0)
        {
            type = new Type();
        }
        else
        {
            type = _unitOfWork.Type.GetById(id);
            if (type == null) return NotFound();
        }

        return View(type);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Upsert(int id, [Bind("Id,Name")] Type type)
    {
        if (id != type.Id) return NotFound();

        Validation(type);


        if (!ModelState.IsValid) return View("upsert", type);

        if (id == 0)
        {
            _unitOfWork.Type.Insert(type);
            SuccessMessage = "New type added";
        }
        else
        {
            _unitOfWork.Type.Update(type);
            SuccessMessage = "Type updated";
        }

        _unitOfWork.Save();
        return RedirectToAction(nameof(Index));
    }

    private void Validation(Type type)
    {
        var isCreate = type.Id == 0;

        var categoryNameExist = _unitOfWork.Type
            .Get(isCreate
                ? c => c.Name == type.Name
                : c => c.Name == type.Name && c.Id != type.Id)
            .Any();

        if (categoryNameExist)
            ModelState.AddModelError("Name", "The type name already exist");
    }


    #region API CALLS

    [HttpGet]
    public IActionResult GetAll()
    {
        var categoryList = _unitOfWork.Type.GetAll();
        return Json(new { data = categoryList });
    }

    [HttpDelete]
    [ValidateAntiForgeryToken]
    public IActionResult Delete(int id)
    {
        _unitOfWork.Type.Delete(id);
        return Json(_unitOfWork.Save() > 0 ? new { success = true, message = "Type deleted" } : new { success = false, message = "Error while deleting" });
    }

    #endregion
}