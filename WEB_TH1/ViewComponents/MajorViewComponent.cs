﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using WEB_TH1.Data;
using WEB_TH1.Models;

namespace WEB_TH1.ViewComponents
{
    public class MajorViewComponent: ViewComponent
    {
        SchoolContext db;
        List<Major> majors;

        public MajorViewComponent(SchoolContext _context)
        {
            db = _context;
          majors = db.Majors.ToList();

        }
        public async Task<IViewComponentResult> InvokeAsync()

        {
            return View("RenderMajor", majors);
        }

    }
}
