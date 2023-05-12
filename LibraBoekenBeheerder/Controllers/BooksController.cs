using System;  
using System.Collections.Generic;
using System.Data;
using LibraBoekenBeheerder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using LibraDB;
using LibraLogic;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace LibraBoekenBeheerder.Controllers
{
    public class BooksController : Controller
    {
        
        private readonly CollectionsDAL _collectionsDAL;
        private readonly BooksDAL _booksDAL;
        private readonly CollectionBooksDAL _collectionBooksDAL;
        private readonly Books _booksClass;

        public BooksController(IConfiguration configuration)
        {
            _collectionsDAL = new CollectionsDAL(configuration);
            _booksDAL = new BooksDAL(configuration);
            _collectionBooksDAL = new CollectionBooksDAL(configuration);
            _booksClass = new Books();
        }

        BooksMapper _booksMapper = new BooksMapper();
        // GET: Student/Create
      
        public ActionResult Create()
        {
            //add collections to dropdown list
            var collectionDropDownList = _collectionsDAL.GetAllCollections();

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
                    var dto = _booksMapper.toDTO(booksModel);
                    
                    if (_booksClass.CreateBook(dto, selectedCollectionId))
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
                ViewBag.Error = $"Error occured while creating the book: {e.Message}";
            }

            return View(booksModel);
        }

        [HttpGet]
        public ActionResult Details(int id = 0)
        {
            try
            {
                var bookDto = _booksDAL.GetABook(id);

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
            var dtoList = _booksDAL.GetAllBooks();
            List<BooksModel> booksList = new List<BooksModel>();
            
            foreach (var dtoItem in dtoList)
            {
                var modelItem = _booksMapper.toModel(dtoItem);
                booksList.Add(modelItem);
            }
            return View(booksList);
        }

        [HttpPut]
        public ActionResult Edit(int id = 0) 
        {
            try
            {
                var bookDto = _booksDAL.GetABook(id);
                if (bookDto != null)
                {
                    var booksMapper = new BooksMapper();
                    var booksModel = booksMapper.toModel(bookDto);
                    var collectionDropDownList = _collectionBooksDAL.GetCollectionsNotContainingBook(id);

                    List<SelectListItem> items = collectionDropDownList.Select(cddl => new SelectListItem
                    {
                        Text = cddl.Name.ToString()
                    }).ToList();

                    ViewBag.collectionDropDownList = items;

                    return View(booksModel);
                }
                else
                {
                    return Content("Book could not be found");
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