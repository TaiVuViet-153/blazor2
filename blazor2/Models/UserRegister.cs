// tạo ra 1 đối tượng để binding dữ liệu từ form đăng ký,
// dữ liệu này khớp với các trường input trong file register

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

public class UserRegisterViewModel
{
    [Required(ErrorMessage = "Username is required")]
    [MinLength(3, ErrorMessage = "Username is at least 3 characters")]
    [MaxLength(20, ErrorMessage = "Username is maximize 20 characters")]
    public string Username { get; set; } = "Tài";
    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Email is wrong format")]
    public string Email { get; set; } = "tai@gmail.com";
    [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d).{8,}$", ErrorMessage = "Password is required at least 8 characters, including uppercase, lowercase and numbers")]
    public string Password { get; set; } = "123456";
    public bool Gender { get; set; } = false;
    public string City { get; set; } = "Việt Nam"; // data binding của select vẫn nhận giá trị string
    public List<string> Hobbies { get; set; } = new List<string>() {"Ngủ", "Ăn uống"};

}