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

        BooksDAL booksDAL = new BooksDAL();
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
                    if (booksDAL.CreateBook(dto))
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

            // Return the view with the booksModel object passed as parameter
            return View(booksModel);
        }

    }
}