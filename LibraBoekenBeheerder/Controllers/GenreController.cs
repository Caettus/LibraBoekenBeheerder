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
        private readonly Genre _genreClass;
        private readonly GenreService _genreService;
        private readonly IConfiguration _config;
        public GenreController(IConfiguration configuration)
        {
            _genreMapper = new GenreMapper();
            _genreClass = new Genre(configuration);
            _config = configuration;
            _genreService = new GenreService(configuration);
        }

        public IActionResult Index()
        {
            GenreModel genreModel = new GenreModel();

            var dto = _genreMapper.toClass(genreModel);

            List<GenreModel> genreModels = new List<GenreModel>();

            var genreList = _genreService.ReturnAllGenres(_config);
            foreach (var genre in genreList)
            {
                var modelItem = _genreMapper.toModel(genre);
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

                    if (_genreService.CreateGenre(genre, _config))
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
        
        public ActionResult Delete(int id)
        {
                
            if (_genreClass.DeleteGenre(id))
            {
                ViewBag.Message = "Genre succesfully deleted";
                ModelState.Clear();
            }
            else
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors);
                foreach (var error in errors)
                {
                    Console.WriteLine($"{error.ErrorMessage}");
                }
            }
            return RedirectToAction("Index");
        }
        
        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                IConfiguration config = _config;
                var genreBooksList = _genreClass.ReturnBooksInGenre(id);
        
                List<BooksModel> items = genreBooksList.Select(cddl => new BooksModel
                {
                    Title = cddl.Title.ToString(),
                    BookId = cddl.BookId
                }).ToList();
        
                ViewBag.genreBooksList = items;
        
                var genreDto = _genreClass.ReturnAGenre(id);
        
                if (genreDto != null)
                {
                    var genreModel = _genreMapper.toModel(genreDto);
                    return View(genreModel);
                }
                else
                {
                    return Content("Book not found!");
                }
        
            }
            catch (Exception e)
            {
                ViewBag.Message = $"Exception: {e}";
                throw;
            }
        }
    }
}
