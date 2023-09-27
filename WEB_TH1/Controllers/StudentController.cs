using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Diagnostics;
using WEB_TH1.Models;
using WEB_TH1.Services.Interfaces;

namespace WEB_TH1.Controllers
{
    [Route("Admin/Student")]
    public class StudentController : Controller
    {
        private List<Student> listStudents = new List<Student>();
        readonly IBufferedFileUploadService _bufferedFileUploadService;

        public StudentController(IBufferedFileUploadService bufferedFileUploadService) 
        {

            //Tạo danh sách sinh viên với 4 dữ liệu mẫu
            listStudents = new List<Student>(){
                new Student() { Id = 101, Name = "Hải Nam", Branch = Branch.IT,
                Gender = Gender.Male, IsRegular=true,
                Address = "A1-2018", Email = "nam@g.com" },

                new Student() { Id = 102, Name = "Minh Tú", Branch = Branch.BE,
                Gender = Gender.Female, IsRegular=true,
                Address = "A1-2019", Email = "tu@g.com" },

                new Student() { Id = 103, Name = "Hoàng Phong", Branch = Branch.CE,
                Gender = Gender.Male, IsRegular=false,
                Address = "A1-2020", Email = "phong@g.com" },

                new Student() { Id = 104, Name = "Xuân Mai", Branch = Branch.EE,
                Gender = Gender.Female, IsRegular = false,
                Address = "A1-2021", Email = "mai@g.com" },

             };

            _bufferedFileUploadService = bufferedFileUploadService;

        }
        [Route("Index")]
        public IActionResult Index()
        {
            //Trả về View Index.cshtml cùng Model là danh sách sv listStudents
            return View(listStudents);

        }
        [HttpGet("Add")]
        public IActionResult Create()
        {
           
            //lấy danh sách các giá trị Gender để hiển thị radio button trên form
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            //lấy danh sách các giá trị Branch để hiển thị select-option trên form
            //Để hiển thị select-option trên View cần dùng List<SelectListItem>
            ViewBag.AllBranches = new List<SelectListItem>(){
                 new SelectListItem { Text = "IT", Value = "1" },
                 new SelectListItem { Text = "BE", Value = "2" },
                 new SelectListItem { Text = "CE", Value = "3" },
                 new SelectListItem { Text = "EE", Value = "4" }
            };
            return View();
        }
        [HttpPost("Add")]
        public IActionResult Create(Student s)
        {
            if (ModelState.IsValid)
            {
                s.Id = listStudents.Last<Student>().Id + 1;
                listStudents.Add(s);
                return View("Index", listStudents);
            }
           

            //lấy danh sách các giá trị Gender để hiển thị radio button trên form
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>().ToList();
            //lấy danh sách các giá trị Branch để hiển thị select-option trên form
            //Để hiển thị select-option trên View cần dùng List<SelectListItem>
            ViewBag.AllBranches = new List<SelectListItem>(){
                 new SelectListItem { Text = "IT", Value = "1" },
                 new SelectListItem { Text = "BE", Value = "2" },
                 new SelectListItem { Text = "CE", Value = "3" },
                 new SelectListItem { Text = "EE", Value = "4" }
            };
            return View();
        }

        public async Task<ActionResult> Create(Student s, IFormFile avatarfile)
        {
            if (avatarfile != null)
                try
                {
                    if (await _bufferedFileUploadService.UploadFile(avatarfile))
                    {
                        Debug.WriteLine("File Upload Successful");
                        ViewBag.Message = "File Upload Successful";
                    }
                    else
                    {
                        Debug.WriteLine("File Upload Failed");
                        ViewBag.Message = "File Upload Failed";
                    }
                }
                catch
                {
                    Debug.WriteLine("File Upload Failed");
                    //Log ex
                    ViewBag.Message = "File Upload Failed";
                }

            s.Id = listStudents.Last<Student>().Id + 1;

            if (s.AvatarUrl != null)
                s.AvatarUrl = Path.Combine("UploadedFiles", s.AvatarUrl);

            
            listStudents.Add(s);
           
            return View("Index", listStudents);
        }


    }
}
