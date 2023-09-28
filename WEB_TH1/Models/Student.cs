using System.ComponentModel.DataAnnotations;

namespace WEB_TH1.Models
{
    public class Student
    {
        
        public int Id { get; set; }//Mã sinh viên

        [StringLength(100, MinimumLength = 4, ErrorMessage ="Tên Phải từ 4 đến 100 kí tự")]
        [Required(AllowEmptyStrings = false, ErrorMessage = "Bạn phải nhập tên")]
        //[Display(Name="Họ Tên")]
        public string? Name { get; set; } //Họ tên
        [Required(ErrorMessage = "Email bắt buộc phải được nhập")]
        [RegularExpression(@"[A-Za-z0-9._%+-]+@[A-Za-z0-9.-]+\.[A-Za-z]{2,4}", ErrorMessage ="mail phải có đuôi gmail.com")]
        
        public string? Email { get; set; } //Email
        [StringLength(100, MinimumLength = 8, ErrorMessage = "Mật khẩu từ 8 ký tự trở lên")]
        [RegularExpression(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$", ErrorMessage = "Mật khẩu phải có ký tự viết hoa, viết thường, chữ số và ký tự đặc biệt")]
        [Required(ErrorMessage ="Bạn phải nhập password")]
        //[Required(ErrorMessage = "Mật khẩu từ 8 ký tự trở lên, có ký tự viết hoa, viết thường, chữ số và ký tự đặc biệt")]
        public string? Password { get; set; }//Mật khẩu
        [Required]
        public Branch? Branch { get; set; }//Ngành học
        [Required(ErrorMessage ="Bạn Phải chọn giới tính")]
        public Gender? Gender { get; set; }//Giới tính
        [Required]
        public bool IsRegular { get; set; }//Hệ: true-chính qui, false-phi cq
        [DataType(DataType.MultilineText)]
        [Required]
        public string? Address { get; set; }//Địa chỉ
        [Range(typeof(DateTime), "1/1/1963", "12/31/2005", ErrorMessage = "Ngày tháng bắt buộc phải được chọn trong khoảng 1/1/1963 - 31/12/2005")]
        [DataType(DataType.Date)]
        [Required(ErrorMessage ="Bạn phải chọn ngày sinh")]
        public DateTime? DateOfBorth { get; set; }//Ngày sinh
        [Required(ErrorMessage ="Bạn phải nhập điểm")]
        [Range(typeof(float), "0.0", "10.0", ErrorMessage = "Điểm phải từ 0.0 tới 10.0")]
        [RegularExpression(@"^\d+(\.\d{1,1})?$", ErrorMessage = "Điểm chỉ được chứa một chữ số sau dấu chấm")]
        public float? Diem { get; set; } // diem 
        public string? AvatarUrl { get; set; }


    }
}
