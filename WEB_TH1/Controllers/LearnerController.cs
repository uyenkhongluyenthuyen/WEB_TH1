using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Printing;
using System.Security.Policy;
using WEB_TH1.Data;
using WEB_TH1.Models;

namespace WEB_TH1.Controllers
{
    public class LearnerController : Controller
    {
        private SchoolContext db;
        public LearnerController(SchoolContext context)
        {
            db = context;
        }

        private int pageSize = 2;
        public IActionResult Index(int? mid)
        {
            var learners = (IQueryable<Learner>)db.Learners.Include(m => m.Major);
          
           if(mid != null)
            {
                learners = (IQueryable<Learner>)db.Learners
                     .Where(l => l.MajorID == mid)
                     .Include(m => m.Major);

            }
            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            ViewBag.pageNum = pageNum;

            var result = learners.Take(pageSize).ToList();
            return View(result);
        }
        public IActionResult LearnerFilter(int? mid, string? keyword, int? pageIndex)
        {
            var learners = (IQueryable<Learner>)db.Learners;
            //int pageSize = 1;
            int page = (int)(pageIndex == null || pageIndex <= 0 ? 1 : pageIndex);
            if(mid != null)
            {
                learners = learners.Where(l=>l.MajorID == mid);
                ViewBag.mid = mid;
            }
            if(keyword != null)
            {
                learners = learners.Where(l=>l.FirstMidName.ToLower()
                          .Contains(keyword.ToLower()));
                ViewBag.keyword = keyword;  
            }

            int pageNum = (int)Math.Ceiling(learners.Count() / (float)pageSize);
            ViewBag.pageNum = pageNum;

             var result = learners
             .Skip(pageSize * (page - 1))
             .Take(pageSize)
             .Include(m => m.Major);  // Thay vì Include(m => m.MajorID)



            return PartialView("LearnerTable",result);
        }


        public IActionResult LearnerByMajorID(int mid)
        {
            var learners = db.Learners.Where(l => l.MajorID == mid).Include(m => m.Major).ToList();
            return PartialView("LearnerTable", learners);   // trả về learner table - nó là 1 Partialview
        }


        //tạo chức năng lọc các learner theo major id (ajax) load dữ liệu ko đông bộ bằng javascipt và 
        // khi click vào chỉ thay đổi phần list learner , còn các phần khác như hearder , footer , menu thì vẫn giữ nguyên -> load trên 1 trang , trả về learner table




        //thêm 2 action create
        public IActionResult Create()
        {
            //dùng 1 trong 2 cách để tạo SelectList gửi về View qua ViewBag để
            //hiển thị danh sách chuyên ngành (Majors)
            var majors = new List<SelectListItem>(); //cách 1
            foreach (var item in db.Majors)
            {
                majors.Add(new SelectListItem
                {
                    Text = item.MajorName,

                    Value = item.MajorID.ToString()
                });

            }
            ViewBag.MajorID = majors;
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName"); //cách 2
            return View();
             
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create([Bind("FirstMidName,LastName,MajorID,EnrollmentDate")]
Learner learner)
        {
            if (ModelState.IsValid)
            {
                db.Learners.Add(learner);
                db.SaveChanges();
                return RedirectToAction(nameof(Index));
            }
            //lại dùng 1 trong 2 cách tạo SelectList gửi về View để hiển thị danh sách Majors
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName");
            return View();
        }
        
        // them 2 action edit
        public IActionResult Edit(int id)
        {
            if (id == null || db.Learners == null)
            {
                return NotFound();
            }

            var learner = db.Learners.Find(id);
            if (learner == null)
            {
                return NotFound();
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);

            
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("LearnerID,FirstMidName,LastName,MajorID,EnrollmentDate")] Learner learner)
        {
            if (id != learner.LearnerID)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                try
                {
                    db.Update(learner);
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LearnerExists(learner.LearnerID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewBag.MajorID = new SelectList(db.Majors, "MajorID", "MajorName", learner.MajorID);
            return View(learner);
        }
        private bool LearnerExists(int id)
        {
            return (db.Learners?.Any(e => e.LearnerID == id)).GetValueOrDefault();
        }

        //Them action delete 
        public IActionResult Delete(int id)
        {
            if (id == null || db.Learners == null)
            {
                return NotFound();
            }
            var learner = db.Learners.Include(l => l.Major)
                .Include(e => e.Enrollments)
                .FirstOrDefault(m => m.LearnerID == id);
            if (learner == null)
            {
                return NotFound();
            }
            if (learner == null)
            {
                return Content("This learner has some enrrollments, can't delete!");
            }
            return View(learner);
        }
        //POST: Leaner/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            if (db.Learners == null)
            {
                return Problem("Entity set 'leaner' is null");
            }
            var learner = db.Learners.Find(id);
            if (learner != null)
            {
                db.Learners.Remove(learner);
            }
            db.SaveChanges();
            return RedirectToAction(nameof(Index));
        }
       
    }
}
