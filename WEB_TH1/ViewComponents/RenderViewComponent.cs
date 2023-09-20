using Microsoft.AspNetCore.Mvc;
using WEB_TH1.Models;

namespace WEB_TH1.ViewComponents 
{
    
    public class RenderViewComponent : ViewComponent
    {
        private List<MenuItem> MenuItems = new List<MenuItem>();

        public RenderViewComponent() 
        {
            MenuItems = new List<MenuItem>()
            {
                new MenuItem() {Id =1, Name="Branches", Link="Branches/List"},
                new MenuItem() {Id =2, Name="Student", Link="Student/List"},
                new MenuItem() {Id =3, Name="Subjects", Link="Subjects/List"},
                new MenuItem() {Id =4, Name="Cources", Link="Cources/List"}
            };
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            return View("RenderLeftMenu", MenuItems);
        }
    }
}
