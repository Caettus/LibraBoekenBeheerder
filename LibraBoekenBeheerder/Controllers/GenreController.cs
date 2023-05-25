using LibraBoekenBeheerder.Models;
using LibraLogic;
using LibraLogic.Mappers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibraBoekenBeheerder.Controllers
{
    public class GenreController : Controller
    {
        private readonly GenreMapper _genreMapper;
        private readonly Genre genreClass;
        private readonly IConfiguration _config;
        public GenreController(IConfiguration configuration)
        {
            _genreMapper = new GenreMapper();
            genreClass = new Genre();
            _config = configuration;
        }

        public IActionResult Index()
        {
            GenreModel genreModel = new GenreModel();

            var dto = _genreMapper.toClass(genreModel);

            List<GenreModel> genreModels = new List<GenreModel>();

            var dtoList = genreClass.ReturnAllGenres(_config);
            foreach (var dtoItem in dtoList)
            {
                var modelItem = _genreMapper.toModel(dtoItem);
                genreModels.Add(modelItem);
            }
            return View(genreModels);
        }

        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(GenreModel genreModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var genre = _genreMapper.toClass(genreModel);

                    if (genreClass.CreateGenre(genre, _config))
                    {
                        ViewBag.Message = "Genre succesfully created!";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = "Error occurred while creating the Genre";
                    }
                }
                else
                {
                    var errors = ModelState.Values.SelectMany(v => v.Errors);
                    foreach (var error in errors)
                    {
                        Console.WriteLine($"{error.ErrorMessage}");
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Error = $"Error occured while creating the book: {e.Message}";
            }

            return View(genreModel);
        }
    }
}
