using System;  
using System.Collections.Generic;
using System.Data;
using LibraBoekenBeheerder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using LibraDTO;
using LibraLogic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace LibraBoekenBeheerder.Controllers
{
    public class BooksController : Controller
    {
        //test
        private readonly Books _booksClass;
        private readonly IConfiguration _configuration;
        private readonly BooksMapper _booksMapper;
        private readonly CollectionClass _collectionClass;
        private readonly Genre _genreClass;

        public BooksController(IConfiguration configuration)
        {
            _configuration = configuration;
            _booksClass = new Books();
            _booksMapper = new BooksMapper();
            _collectionClass = new CollectionClass();
            _genreClass = new Genre();
        }

    public ActionResult Create()
        {
            IConfiguration config = _configuration;
            //add collections to dropdown list
            var collectionDropDownList = _collectionClass.ReturnAllCollections(config);

            List<SelectListItem> items = collectionDropDownList.Select(cddl => new SelectListItem
            {
                Text = cddl.Name.ToString(),
                Value = cddl.CollectionsID.ToString()
            }).ToList();

            ViewBag.collectionDropDownList = items;
            
            //add genres to dropdown list
            var genreDropDownList = _genreClass.ReturnAllGenres(config);

            List<SelectListItem> genres = genreDropDownList.Select(cddl => new SelectListItem
            {
                Text = cddl.GenreName.ToString(),
                Value = cddl.GenreId.ToString()
            }).ToList();

            ViewBag.genreDropDownList = genres;
            return View();
        }
        [HttpPost]
        public ActionResult Create(BooksModel booksModel, int selectedCollectionId, int selectedGenreId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dto = _booksMapper.toClass(booksModel);
                    
                    if (_booksClass.CreateBook(dto, selectedCollectionId, selectedGenreId, _configuration))
                    {
                        ViewBag.Message = "Book succesfully created!";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = "Error occurred while creating the book";
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

            return View(booksModel);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                IConfiguration config = _configuration;
                //Return collections book is part of
                var collectionDropDownList = _collectionClass.ReturnCollectionsContaintingBook(id, config);

                List<SelectListItem> items = collectionDropDownList.Select(cddl => new SelectListItem
                {
                    Text = cddl.Name.ToString()
                }).ToList();

                ViewBag.collectionDropDownList = items;
                
                //Return genres book has
                var genreDropDownList = _genreClass.ReturnGenresFromBook(id, config);

                List<SelectListItem> genres = genreDropDownList.Select(cddl => new SelectListItem
                {
                    Text = cddl.GenreName.ToString()
                }).ToList();

                ViewBag.genreDropDownList = genres;

                var bookDto = _booksClass.GetABook(id, config);

                if (bookDto != null)
                {
                    var booksMapper = new BooksMapper();
                    var bookModel = booksMapper.toModel(bookDto);
                    return View(bookModel);
                }
                else
                {
                    return Content("Book not found!");
                }

            }
            catch (Exception e)
            {
                ViewBag.Message = $"Exception: {e}";
            }
            return View();
        }

        public ActionResult Index(string searchString)
        {
            var configuration = HttpContext.RequestServices.GetService<IConfiguration>();
            var returnBooksList = _booksClass.ReturnAllBooks(_configuration);
            
            //Return list of books
            var booksList = new List<BooksModel>();
            foreach (var item in returnBooksList)
            {
                var booksItem = _booksMapper.toModel(item);
                booksList.Add(booksItem);
            }
            
            // Search
            //niet een geweldige manier maar nou hoef ik tenminste geen javscr*pt te gebruiken.
            if (!String.IsNullOrEmpty(searchString))
            {
                booksList = booksList.Where(s => s.Title!.Contains(searchString)).ToList();
            }


            return View(booksList);
        }


        public ActionResult Delete(int id)
        {
                
            if (_booksClass.DeleteBook(_configuration, id))
            {
                ViewBag.Message = "Book succesfully deleted";
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
        public ActionResult Edit(int id)
        {
            IConfiguration configuration = _configuration;
            var collectionDropDownList = _collectionClass.ReturnCollectionsNotContaintingBook(id, configuration);

            List<SelectListItem> items = collectionDropDownList.Select(cddl => new SelectListItem
            {
                Text = cddl.Name.ToString(),
                Value = cddl.CollectionsID.ToString()
            }).ToList();

            ViewBag.collectionDropDownList = items;
            return View();
        }


        [HttpPost]
        public ActionResult Edit(BooksModel booksModel, int selectedCollectionId, int id)
        {
            try
            {
                IConfiguration configuration = _configuration;
                var bookDto = _booksClass.GetABook(id, configuration);
                if (bookDto != null && ModelState.IsValid)
                {
                    var mapper = new BooksMapper();
                    var bookClass = mapper.toClass(booksModel);

                        if (_booksClass.EditBook(bookClass, selectedCollectionId, id, _configuration))
                        {
                            ViewBag.Message = "Now check the database to see if it's true";
                            ModelState.Clear();
                        }
                        else
                        {
                            ViewBag.Message = "An error occurred while updating the book";
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
                ViewBag.Message = $"Exception: {e}";
            }
            return View();
        }
    }
}