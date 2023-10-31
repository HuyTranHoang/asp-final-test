using System.Diagnostics;
using asp_final_test.Models;
using asp_final_test.Repository.IRepository;
using Microsoft.AspNetCore.Mvc;

namespace asp_final_test.Areas.Customer.Controllers;

[Area("Customer")]
public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly IUnitOfWork _unitOfWork;

    public HomeController(IUnitOfWork unitOfWork, ILogger<HomeController> logger)
    {
        _unitOfWork = unitOfWork;
        _logger = logger;
    }

    public IActionResult Index()
    {
        var vaccines = _unitOfWork.Vaccine.GetAll("Category");
        return View(vaccines);
    }

    public IActionResult Details(int id)
    {
        var basketItem = new BasketItem
        {
            Vaccine = _unitOfWork.Vaccine.Get(p => p.Id == id, "Category").FirstOrDefault(),
            Count = 1
        };

        if (basketItem.Vaccine == null) return NotFound();

        return View(basketItem);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}