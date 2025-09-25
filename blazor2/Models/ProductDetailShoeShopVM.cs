public class ProductDetailShoeShopVM
{
    public int id { get; set; }
    public string name { get; set; }
    public string alias { get; set; }
    public double price { get; set; }
    public bool feature { get; set; }
    public string description { get; set; }
    public string[] size { get; set; }
    public string shortDescription { get; set; }
    public int quantity { get; set; }
    public string[] detaildetailedImages { get; set; }
    public string image { get; set; }
    public string imgLink { get; set; }
    public Category[] categories { get; set; }
    public RelatedProduct[] relatedProducts { get; set; }
}

public partial class Category
{
    public string id { get; set; }
    public string category { get; set; }
}

public partial class RelatedProduct
{
    public int id { get; set; }
    public string name { get; set; }
    public string alias { get; set; }
    public bool feature { get; set; }
    public double price { get; set; }
    public string description { get; set; }
    public string shortDescription { get; set; }
    public string image { get; set; }
    public object imgLink { get; set; }
}