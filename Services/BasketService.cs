using asp_final_test.Helper;
using asp_final_test.Models;
using asp_final_test.Repository.IRepository;

namespace asp_final_test.Services;

public class BasketService : IBasketService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IHttpContextAccessor _contextAccessor;


    public BasketService(IUnitOfWork unitOfWork, IHttpContextAccessor contextAccessor)
    {
        _unitOfWork = unitOfWork;
        _contextAccessor = contextAccessor;
    }

    public void Add(int id, int quantity)
    {
        List<BasketItem> shoppingCartList = _contextAccessor.HttpContext.Session.Get<List<BasketItem>>(SD.ShoppingCartSession) ?? new List<BasketItem>();

        var existingItem = shoppingCartList.FirstOrDefault(i => i.Vaccine.Id == id);
        if (existingItem != null)
        {
            existingItem.Count += quantity;
        }
        else
        {
            shoppingCartList.Add(new BasketItem
            {
                Count = quantity,
                Vaccine = _unitOfWork.Vaccine.GetById(id)
            });
        }

        _contextAccessor.HttpContext.Session.Set<List<BasketItem>>(SD.ShoppingCartSession, shoppingCartList);
    }

    public void Remove(int id)
    {
        if (_contextAccessor.HttpContext.Session.Get<List<BasketItem>>(SD.ShoppingCartSession) != default)
        {
            List<BasketItem> shoppingCartList = _contextAccessor.HttpContext.Session.Get<List<BasketItem>>(SD.ShoppingCartSession);

            shoppingCartList.RemoveAll(i => i.Vaccine.Id == id);

            _contextAccessor.HttpContext.Session.Set<List<BasketItem>>(SD.ShoppingCartSession, shoppingCartList);
        }
    }

    public void IncreaseQuantity(int id)
    {
        List<BasketItem> shoppingCartList = _contextAccessor.HttpContext.Session.Get<List<BasketItem>>(SD.ShoppingCartSession);
        if (shoppingCartList != null)
        {
            var existingItem = shoppingCartList.FirstOrDefault(i => i.Vaccine.Id == id);
            if (existingItem != null)
            {
                existingItem.Count++;
                _contextAccessor.HttpContext.Session.Set<List<BasketItem>>(SD.ShoppingCartSession, shoppingCartList);
            }
        }
    }

    public void DecreaseQuantity(int id)
    {
        List<BasketItem> shoppingCartList = _contextAccessor.HttpContext.Session.Get<List<BasketItem>>(SD.ShoppingCartSession);
        if (shoppingCartList != null)
        {
            var existingItem = shoppingCartList.FirstOrDefault(i => i.Vaccine.Id == id);
            if (existingItem != null)
            {
                existingItem.Count = Math.Max(0, existingItem.Count - 1);
                _contextAccessor.HttpContext.Session.Set<List<BasketItem>>(SD.ShoppingCartSession, shoppingCartList);
            }
        }
    }

    public void ClearBasket()
    {
        _contextAccessor.HttpContext.Session.Remove(SD.ShoppingCartSession);
    }
}