using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;

// Hub để quản lý các kết nối và tương tác realtime
// định nghĩa những phương thức mà client có thể gọi tới server / lắng nghe server
public class RoomHub : Hub
{
    private readonly RoomChatService _roomChatService;

    public RoomHub(RoomChatService roomChatService)
    {
        _roomChatService = roomChatService;
    }

    // khi client kết nối đến hub thì sẽ được nhận về ds phòng hiện có
    // method ten SendRoomList
    public async Task SendRoomList()
    {
        Console.WriteLine("[SignalR] Client SEND ROOM: ");
        await Clients.All.SendAsync("ReceiveRoomList", _roomChatService.lsRoom);
    }

    // khi client kết nối đến hub thì hàm này sẽ chạy
    public override async Task OnConnectedAsync()
    {
        Console.WriteLine("[SignalR] Client connected: " + Context.ConnectionId);
        await base.OnConnectedAsync();
        await SendRoomList();
    }

    // khi client ngắt kết nối đến hub hàm này sẽ chạy
    public override async Task OnDisconnectedAsync(Exception exception)
    {
        Console.WriteLine("[SignalR] Client disconnected: " + Context.ConnectionId);
        await base.OnDisconnectedAsync(exception);
    }

    // AddRoom
    public async Task AddRoom()
    {
        // chỉ cần gọi hàm AddRoom trong state RoomChatService
        // không cần viết logic nữa
        Console.WriteLine("[SignalR] Client ADD ROOM: ");
        _roomChatService.AddRoom();
        await SendRoomList();
    }
}