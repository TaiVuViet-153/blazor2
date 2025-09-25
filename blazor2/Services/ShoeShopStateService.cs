using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public class ShoeShopStateService
{
    private readonly HttpClient _http;

    //dùng factory
    private readonly IHttpClientFactory _httpClientFactory;

    // public ShoeShopStateService(HttpClient http)
    // {
    //     _http = http;
    // }
    public ShoeShopStateService(IHttpClientFactory httpClientFactory)
    {
        _httpClientFactory = httpClientFactory;
        _http = _httpClientFactory.CreateClient("ShoeShopApi");
    }

    // DS sp
    public List<ProductShoeShopVM> lsProduct = new List<ProductShoeShopVM>();
    public ProductDetailShoeShopVM productDetail = new ProductDetailShoeShopVM();

    public event Action? OnChange;
    private void NotifyStateChanged() => OnChange?.Invoke();

    // viết function get data
    public async Task GetAllData()
    {
        // var data = await _http.GetFromJsonAsync<ResponseEntity<List<ProductShoeShopVM>>>("https://apistore.cybersoft.edu.vn/api/Product");
        var data = await _http.GetFromJsonAsync<ResponseEntity<List<ProductShoeShopVM>>>("/api/Product");
        lsProduct = data.content;
        NotifyStateChanged();
    }

    // lấy chi tiết sản phẩm
    public async Task GetProductById(int id)
    {
        // var data = await _http.GetFromJsonAsync<ResponseEntity<ProductDetailShoeShopVM>>($"https://apistore.cybersoft.edu.vn/api/Product/getbyid?id={id}");
        var data = await _http.GetFromJsonAsync<ResponseEntity<ProductDetailShoeShopVM>>($"/api/Product/getbyid?id={id}");
        productDetail = data.content;
        NotifyStateChanged();
    }


    public async Task<string> AddNewShoe(AddShoeApiVM newShoe)
    {
        // var response = await _http.PostAsJsonAsync("https://apistore.cybersoft.edu.vn/api/Product", newShoe);
        var response = await _http.PostAsJsonAsync("/api/Product", newShoe);
        if (response.IsSuccessStatusCode)
        {
            // nếu thêm mới thành công, bạn có thể làm gì đó ở đây
            var responseContent = await response.Content.ReadFromJsonAsync<ResponseEntity<string>>();
            Console.WriteLine(responseContent.content);
            return responseContent.content;
        }
        else
        {
            // Xử lý lỗi nếu cần
            Console.WriteLine("Lỗi khi thêm mới giày: " + response.ReasonPhrase);
            return "Lỗi khi thêm mới giày: " + response.ReasonPhrase;
        }
    }

    // UPDATE
     public async Task<string> UpdateShoe(AddShoeApiVM newShoe)
    {
        var response = await _http.PutAsJsonAsync("api/Product/updateProduct", newShoe);
        if (response.IsSuccessStatusCode)
        {
            // Nếu thêm mới thành công, bạn có thể làm gì đó ở đây
            // lấy nội dung từ phản hồi của api
            var responseContent = await response.Content.ReadFromJsonAsync<ResponseEntity<string>>();
            Console.WriteLine("cập nhật thành công: " + responseContent.content);
            return responseContent.content;
        }
        else
        {
            // Xử lý lỗi nếu cần
            Console.WriteLine("Lỗi khi cập nhật: " + response.ReasonPhrase);
            return "Lỗi khi cập nhật: " + response.ReasonPhrase;
        }

    }

    public async Task<string> DeleteShoe(int id)
    {
        // var response = await _http.DeleteAsync($"https://apistore.cybersoft.edu.vn/api/Product/{id}");
        var response = await _http.DeleteAsync($"/api/Product/{id}");

        if (response.IsSuccessStatusCode)
        {
            // Get all lại data
            await GetAllData();
            NotifyStateChanged();
            // nếu thêm mới thành công, bạn có thể làm gì đó ở đây
            var responseContent = await response.Content.ReadFromJsonAsync<ResponseEntity<string>>();
            Console.WriteLine(responseContent.content);
            return responseContent.content;
        }
        else
        {
            // Xử lý lỗi nếu cần
            Console.WriteLine("Lỗi khi thêm mới giày: " + response.ReasonPhrase);
            return "Lỗi khi thêm mới giày: " + response.ReasonPhrase;
        }
    }

}