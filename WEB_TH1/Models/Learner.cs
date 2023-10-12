namespace WEB_TH1.Models
{
    public class Learner
    {
        public Learner() 
        {
            Enrollments = new HashSet<Enrollment>();
        }    
        public int LearnerID { get; set; }
        public string LastName { get; set; }
        public string FirstMidName { get; set; }
        public DateTime EnrollmentDate { get; set; }
        public int MajorID { get; set; }     //khoa ngoai
        public virtual Major? Major { get; set; }   //1 chuyen nganh
        public ICollection<Enrollment> Enrollments { get; set; }// 1 learner co nhieu enrollment
    }
}
