using asp_final_test.Helper;
using asp_final_test.Models;
using asp_final_test.Services;
using asp_final_test.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace asp_final_test.Areas.Customer.Controllers;

[Area("Customer")]
public class BasketController : Controller
{
    private readonly IBasketService _basketService;

    public BasketController(IBasketService basketService)
    {
        _basketService = basketService;
    }

    [TempData] public string? SuccessMessage { get; set; }

    public IActionResult Index()
    {
        ShoppingCartDto shoppingCartDto = new ShoppingCartDto();

        if(HttpContext.Session.Get<List<BasketItem>>(SD.ShoppingCartSession) != default)
        {
            shoppingCartDto.BasketItemList = HttpContext.Session.Get<List<BasketItem>>(SD.ShoppingCartSession)!;
        } else
        {
            shoppingCartDto.BasketItemList = new List<BasketItem>();
        }

        foreach (var cart in shoppingCartDto.BasketItemList)
        {
            shoppingCartDto.OrderTotal += cart.Count * cart.Vaccine.Price;
        }

        return View(shoppingCartDto);
    }

    [HttpPost]
    [IgnoreAntiforgeryToken]
    public IActionResult AddToCart([Bind("Product, Count")] BasketItem basketItem)
    {
        _basketService.Add(basketItem.Vaccine.Id, basketItem.Count);
        SuccessMessage = "Add to cart successfully!";
        return RedirectToAction("Index", "Home");
    }

    public IActionResult Plus(int productId)
    {
        _basketService.IncreaseQuantity(productId);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Minus(int productId)
    {
        _basketService.DecreaseQuantity(productId);
        return RedirectToAction(nameof(Index));
    }

    public IActionResult Remove(int productId)
    {
        _basketService.Remove(productId);
        return RedirectToAction(nameof(Index));
    }
}
