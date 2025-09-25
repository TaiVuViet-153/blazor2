using System;

public class CounterService
{
    public int CurrentCounter = 0;

    // đăng ký sự kiện để thông báo khi giá trị thay đổi
    public event Action OnChange;
    // hàm thay đổi giá trị của CurrentCounterr
    // public void ModifyCounter()
    // {

    // }
    public void TangCounter()
    {
        CurrentCounter++;
        NotifyStateChanged();
    }
    public void GiamCounter()
    {
        CurrentCounter--;
        NotifyStateChanged();
    }



    // hàm để gọi sự kiện OnChange
    public void NotifyStateChanged() => OnChange?.Invoke();
}