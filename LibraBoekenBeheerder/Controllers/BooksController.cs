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
        private readonly Collection _collectionClass;
        private readonly Books _booksClass;
        private readonly IConfiguration _configuration;
        private readonly BooksMapper _booksMapper;

        public BooksController(IConfiguration configuration, Collection collectionClass, Books booksClass, BooksMapper booksMapper)
        {
            _configuration = configuration;
            _collectionClass = collectionClass;
            _booksClass = booksClass;
            _booksMapper = booksMapper;
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
            return View();
        }





        [HttpPost]
        public ActionResult Create(BooksModel booksModel, int selectedCollectionId)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dto = _booksMapper.toClass(booksModel);
                    
                    if (_booksClass.CreateBook(dto, selectedCollectionId, _configuration))
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
                var collectionDropDownList = _collectionClass.ReturnCollectionsContaintingBook(id, config);

                List<SelectListItem> items = collectionDropDownList.Select(cddl => new SelectListItem
                {
                    Text = cddl.Name.ToString()
                }).ToList();

                ViewBag.collectionDropDownList = items;

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
                throw;
            }
        }

        public ActionResult Index()
        {
            var configuration = HttpContext.RequestServices.GetService<IConfiguration>();
            var returnBooksList = _booksClass.ReturnAllBooks(configuration);

            var booksList = new List<BooksModel>();
            foreach (var item in returnBooksList)
            {
                var booksItem = _booksMapper.toModel(item);
                booksList.Add(booksItem);
            }

            return View(booksList);
        }




        [HttpPut]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(string _method, BooksModel booksModel, int selectedCollectionId, int bookId = 0)
        {
            try
            {
                IConfiguration configuration = _configuration;
                var collectionDropDownList = _collectionClass.ReturnCollectionsNotContaintingBook(bookId, configuration);

                List<SelectListItem> items = collectionDropDownList.Select(cddl => new SelectListItem
                {
                    Text = cddl.Name.ToString()
                }).ToList();

                ViewBag.collectionDropDownList = items;

                var bookDto = _booksClass.GetABook(bookId, configuration);
                if (bookDto != null && ModelState.IsValid)
                {
                    var mapper = new BooksMapper();
                    var bookClass = mapper.toClass(booksModel);

                    //Dit om ervoor te zorgen dat die het als een PUT request ziet in plaats van een POST
                    if (_method == "PUT")
                    {
                        if (_booksClass.EditBook(bookClass, selectedCollectionId, bookId, _configuration))
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
                        ViewBag.Message = "Invalid request method";
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

            return View(booksModel);
        }
    }
}