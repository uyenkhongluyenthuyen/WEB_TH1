namespace WEB_TH1.Models
{
    public class Major
    {
        public Major() 
        {
            Learners = new HashSet<Learner>();
        }
        public int MajorID { get; set; }
        public string MajorName { get; set; }
        public ICollection<Learner> Learners { get; set; }  //tham chieu tapj hop nguoi hocj hoc chuyen nganh nay
    }
}
