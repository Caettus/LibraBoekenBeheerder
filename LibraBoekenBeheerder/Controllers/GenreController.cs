using Microsoft.AspNetCore.Mvc;

namespace LibraBoekenBeheerder.Controllers
{
    public class GenreController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
