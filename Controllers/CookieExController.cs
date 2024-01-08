using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class CookieExController : Controller
    {
        public IActionResult Index()
        {
            CookieOptions option=new CookieOptions();
            option.Expires=DateTime.Now.AddDays(10);
            Response.Cookies.Append("UId", "123", option);
            Response.Cookies.Append("UName", "xyz", option);
            return View();
        }
        public IActionResult DisplayStuinfo()
        {
            studinfo info = new studinfo()
            {
                id = 1,
                name = "vaishnavi",
                EmailId = "vaishnavi@gmail.com"
            };
            StudAddr add = new StudAddr
            {
                address = "khatgun",
                city = "satara"
            };
            viewModelEx obj = new viewModelEx()
            {
              studinfo= info,
              studAddr= add
            };


            return View(obj);
        }
    }
}
