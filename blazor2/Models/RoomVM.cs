public class RoomVM
{
    public int id { get; set; }
    public string name { get; set; }

    public RoomVM(int id)
    {
        this.id = id;
        name = "Room " + id;
    }
}