using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.WebUtilities;
using System.Net.Http.Json;
using System;
using Microsoft.AspNetCore.Components;

public class CryptoService
{
    // dùng httpclient để lấy data
    private readonly HttpClient _httpClient;
    public CryptoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
    }

    // ds crypto (tất cả từ api)
    public List<CryptoData> Cryptos { get; set; } = new List<CryptoData>();

    public List<CryptoData> MyCryptos { get; set; } = new List<CryptoData>();

    // lấy tất cả các Crypto thông qua API
    // Phương thức gọi API và trả về danh sách các đồng crypto
    public async Task<List<CryptoData>> GetCryptoDataAsync()
    {
        // URL gốc của API
        var url = "https://api.coingecko.com/api/v3/coins/markets";

        // Các tham số query cần thiết cho API
        var parameters = new Dictionary<string, string>
        {
            { "vs_currency", "usd" },
            { "order", "market_cap_desc" },
            { "per_page", "50" },
            { "page", "1" },
            { "sparkline", "false" }
        };

        // Tạo URI hoàn chỉnh với query string
        var uri = QueryHelpers.AddQueryString(url, parameters);

        // Tạo yêu cầu HTTP và thêm header User-Agent
        var request = new HttpRequestMessage(HttpMethod.Get, uri);
        request.Headers.Add("User-Agent", "YourAppName/1.0"); // Thay YourAppName bằng tên app của bạn

        // Gửi yêu cầu và chờ phản hồi
        var response = await _httpClient.SendAsync(request);

        // Đảm bảo phản hồi thành công, nếu không sẽ ném ngoại lệ
        response.EnsureSuccessStatusCode();

        // Đọc và parse JSON trả về thành danh sách CryptoData
        return await response.Content.ReadFromJsonAsync<List<CryptoData>>();
    }

    // thêm crypto vào ds yêu thích
    public void AddToMyCryptos(CryptoData crypto)
    {
        if (!MyCryptos.Exists(c => c.Name == crypto.Name))
        {
            MyCryptos.Add(crypto);
            NotifyStateChanged();
        }
    }
    public void RemoveFromMyCryptos(CryptoData crypto)
    {
        if (MyCryptos.Exists(c => c.Name == crypto.Name))
        {
            MyCryptos.Remove(crypto);
            NotifyStateChanged();
        }
    }

    public void ToggleLikeStatus(CryptoData crypto)
    {
        if (MyCryptos.Exists(c => c.Name == crypto.Name))
            MyCryptos.Remove(crypto);
        else
            MyCryptos.Add(crypto);
        NotifyStateChanged();
    }

    public event Action OnChange;

    private void NotifyStateChanged() => OnChange?.Invoke(); 
}