using System;  
using System.Collections.Generic;
using System.Data;
using LibraBoekenBeheerder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using LibraLogic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibraBoekenBeheerder.Controllers
{
    public class BooksController : Controller
    {
        private readonly Books _booksClass;
        private readonly Collection _collectionClass;
        private readonly IConfiguration _configuration;

        public BooksController(IConfiguration configuration)
        {
            _booksClass = new Books();
            _collectionClass = new Collection();
            _configuration = configuration;
        }

        BooksMapper _booksMapper = new BooksMapper();
      
        public ActionResult Create(IConfiguration configuration)
        {
            //add collections to dropdown list
            var collectionDropDownList = _collectionClass.ReturnAllCollections(configuration);

            List<SelectListItem> items = collectionDropDownList.Select(cddl => new SelectListItem
            {
                Text = cddl.Name.ToString(),
                Value = cddl.CollectionsID.ToString()
            }).ToList();

            ViewBag.collectionDropDownList = items;
            return View();
        }

        public ActionResult Edit(int id, IConfiguration configuration)
        {
            var collectionDropDownList = _collectionClass.ReturnCollectionsNotContaintingBook(id, configuration);

            List<SelectListItem> items = collectionDropDownList.Select(cddl => new SelectListItem
            {
                Text = cddl.Name.ToString()
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
                    var booksClass = _booksMapper.toClass(booksModel);
                    
                    if (_booksClass.CreateBook(booksClass, selectedCollectionId, _configuration))
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
        public ActionResult Details(int id = 0)
        {
            try
            {
                var bookDto = _booksClass.GetABook(id);

                if (bookDto != null)
                {
                    var booksMapper = new BooksMapper();
                    var bookModel = booksMapper.toModel(bookDto);
                    //Moet dit hier?
                    var collectionDropDownList = _collectionBooksDAL.GetCollectionsContainingBook(id);
            
                    List<SelectListItem> items = collectionDropDownList.Select(cddl => new SelectListItem
                    {
                        Text = cddl.Name.ToString()
                    }).ToList();

                    ViewBag.collectionDropDownList = items;
                    //Vast wel
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

        [HttpGet]
        public ActionResult Index(BooksModel booksModel)
        {
            var dto = _booksMapper.toDTO(booksModel);
            var dtoList = _booksClass.GetAllBooks();
            List<BooksModel> booksList = new List<BooksModel>();
            
            foreach (var dtoItem in dtoList)
            {
                var modelItem = _booksMapper.toModel(dtoItem);
                booksList.Add(modelItem);
            }
            return View(booksList);
        }

        [HttpPut]
        public ActionResult Edit(BooksModel booksModel, int selectedCollectionId, int BookId = 0)
        {
            try
            {
                var bookDto = _booksClass.GetABook(BookId);
                if (bookDto != null && ModelState.IsValid)
                {
                    var dto = _booksMapper.toDTO(booksModel);

                    if (_booksClass.EditBook(dto, selectedCollectionId, BookId, _configuration))
                    {
                        ViewBag.Message = "Nu de database checken of het ook waar is";
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
                ViewBag.Message = $"Exception: {e}";
                throw;
            }

            return View(booksModel);
        }

    }
}