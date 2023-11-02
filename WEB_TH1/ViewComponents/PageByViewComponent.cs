using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Cryptography;
using WEB_TH1.Data;
using WEB_TH1.Models;


namespace WEB_TH1.ViewComponents
{
    public class PageByViewComponent : ViewComponent
    {
        List<PageBy> pageBies;

        

        public PageByViewComponent(List<Major> majors)
        {
            pageBies = new List<PageBy>();
            int pageSize = 1;
            int totalMajors = majors.Count;

            int totalPages = (int)Math.Ceiling((Double)totalMajors / pageSize);
            for (int i = 1; i <= totalPages; i++)
            {
                pageBies.Add(new PageBy { Id = i, Title = i.ToString() });
            }
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderPageBy", pageBies);
        }
    }
}
