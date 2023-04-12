using System;  
using System.Collections.Generic;
using System.Data;
using LibraBoekenBeheerder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using LibraDB;
using LibraLogic;

namespace LibraBoekenBeheerder.Controllers
{
    public class BooksController : Controller
    {

        private readonly BooksDAL _booksDAL;

        public BooksController(IConfiguration configuration)
        {
            _booksDAL = new BooksDAL(configuration);
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
            catch
            {
                ViewBag.Message = "Error occurred while creating the book";
            }

            return View(booksModel);
        }

        [HttpGet]
        public ActionResult Index(BooksModel booksModel)
        {
            var dto = _booksMapper.toDTO(booksModel);
            List<BooksModel> booksList = new List<BooksModel>();
            var dtoList = _booksDAL.GetAllBooks();
            foreach (var dtoItem in dtoList)
            {
                var modelItem = _booksMapper.toModel(dtoItem);
                booksList.Add(modelItem);
            }

            return View(booksList);
        }

    }
}