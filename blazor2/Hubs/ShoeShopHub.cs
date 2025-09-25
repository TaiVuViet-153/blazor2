using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

public class ShoeShopHub : Hub
{
    private readonly ShoeShopStateService _shoeShopStateService;

    public ShoeShopHub(ShoeShopStateService shoeShopStateService)
    {
        _shoeShopStateService = shoeShopStateService;
    }

    public async Task GetLatestShoe()
    {
        Console.WriteLine("[SignalR] Get list shoe");
        await Clients.All.SendAsync("ShoeList", _shoeShopStateService.lsProduct);
    }

    public override async Task OnConnectedAsync()
    {
        Console.WriteLine("[SignalR] Customer coming: " + Context.ConnectionId);
        await base.OnConnectedAsync();
        await _shoeShopStateService.GetAllData();
        await GetLatestShoe();
    }

    public override async Task OnDisconnectedAsync(Exception exception)
    {
        Console.WriteLine("[SignalR] Customer leaving: " + Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    // Add Shoe
    public async Task<string> AddShoe(AddShoeApiVM newShoe)
    {
        Console.WriteLine("[SignalR] " + Context.ConnectionId + " adding shoe");
        string mess = await _shoeShopStateService.AddNewShoe(newShoe);
        await GetLatestShoe();
        return mess;
    }

    // Edit Shoe
    public async Task EditShoe(AddShoeApiVM newShoe)
    {
        Console.WriteLine("[SignalR] " + Context.ConnectionId + " editting shoe");
        await _shoeShopStateService.UpdateShoe(newShoe);
        await GetLatestShoe();
    }

    // Delete Shoe
    public async Task DeleteShoe(int id)
    {
        Console.WriteLine("[SignalR] " + Context.ConnectionId + " deleting shoe");
        await _shoeShopStateService.DeleteShoe(id);
        await GetLatestShoe();
    }
} 