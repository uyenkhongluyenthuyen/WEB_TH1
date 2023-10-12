using System.ComponentModel.DataAnnotations.Schema;

namespace WEB_TH1.Models
{
    public class Course
    {
        public Course() 
        {
            Enrollments = new HashSet<Enrollment>();
        }
        [DatabaseGenerated(DatabaseGeneratedOption.None)] // không tự động tăng , ko cấp pháp tự đông 
        public int CourseID { get; set; }
        public string Title { get; set; }
        public int Credits { get; set; }
        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
