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

namespace LibraBoekenBeheerder.Controllers
{
    public class BooksController : Controller
    {
        
        private readonly BooksDAL _booksDAL;
        private readonly CollectionBooksDAL _collectionBooksDAL;

        public BooksController(IConfiguration configuration)
        {
            _booksDAL = new BooksDAL(configuration);
            _collectionBooksDAL = new CollectionBooksDAL(configuration);
        }

        BooksMapper _booksMapper = new BooksMapper();
        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }
        

        [HttpPost]
        public ActionResult Create(BooksModel booksModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var dto = _booksMapper.toDTO(booksModel);
                    
                    //Moet dit hier?
                    int id = 0;
                    var collectionDropDownList = _collectionBooksDAL.GetCollectionsNotContainingBook(id);
            
                    List<SelectListItem> items = collectionDropDownList.Select(cddl => new SelectListItem
                    {
                        Text = cddl.Name.ToString()
                    }).ToList();

                    ViewBag.collectionDropDownList = items;
                    //Vast wel
                    
                    if (_booksDAL.CreateBook(dto))
                    {
                        ViewBag.Message = "Book has been Added Successfully";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = "Error occurred while creating the book";
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