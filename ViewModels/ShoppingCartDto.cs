using asp_final_test.Models;

namespace asp_final_test.ViewModels;

public class ShoppingCartDto
{
    public IEnumerable<BasketItem> BasketItemList { get; set; }

    public double OrderTotal { get; set; }
}