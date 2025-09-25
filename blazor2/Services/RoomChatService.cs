using System.Collections.Generic;

public class RoomChatService
{
    // ds rooms
    public List<RoomVM> lsRoom = new List<RoomVM>()
    {
        new RoomVM(1),
        new RoomVM(2),
        new RoomVM(3)
    };

    public List<RoomVM> GetAllRooms()
    {
        return lsRoom;
    }

    // add room
    public void AddRoom()
    {
        int newId = lsRoom.Count + 1;
        lsRoom.Add(new RoomVM(newId));
    }
}