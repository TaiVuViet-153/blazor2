// quản lý state
// ds topping
using System;
using System.Collections.Generic;

public class BurgerService
{
    public List<Topping> Toppings { get; set; } = new List<Topping>()
    {
        new Topping() { Name = "cheese", Price = 10000, Quantity = 2},
        new Topping() { Name = "beef", Price = 20000, Quantity = 3},
        new Topping() { Name = "salad", Price = 5000, Quantity = 1},
    };

    // hàm thay đổi giá trị của topping
    // hàm thay đổi số lượng topping ( nhận vào tên và số lượng cần thay đổi)

    // vd hiện tại có 5 salad -> nhận vào +1 => 5 + 1 = 6
    // vd hiện tại có 5 salad -> nhận vào -1 => 5 + (-1) = 4

    public void ChangeToppingQuantity(string toppingName, int changedQuantity)
    {
        var selectedTopping = Toppings.Find(item => item.Name == toppingName);

        if (selectedTopping != null)
        {
            if (selectedTopping.Quantity == 0 && changedQuantity < 0)
            {
                return;
            }

            selectedTopping.Quantity += changedQuantity;
            NotifyStateChanged();
            
        }

    }

    // tính tổng tiền
    public decimal TongTien()
    {
        decimal tong = 0;
        foreach (var t in Toppings)
        {
            tong += t.Total;
        }
        return tong;
    }

    public event Action OnChange;


    public void NotifyStateChanged() => OnChange?.Invoke();
}