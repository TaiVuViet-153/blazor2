public class Topping
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int Quantity { get; set; }

    // tính tổng tiền
    public decimal Total => Price * Quantity;
}