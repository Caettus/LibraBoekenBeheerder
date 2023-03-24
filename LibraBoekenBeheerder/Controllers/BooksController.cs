using System;  
using System.Collections.Generic;
using System.Data;
using LibraBoekenBeheerder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;

namespace LibraBoekenBeheerder.Controllers
{
    public class BooksController : Controller
    {
        // GET: Student/Create
        public ActionResult Create()
        {
            return View();
        }
 
        // POST: Student/Create
        [HttpPost]
        public ActionResult Create(BooksModel booksModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    DBHandel sdb = new DBHandel();
                    if (sdb.CreateBook(booksModel))
                    {
                        ViewBag.Message = "Student Details Added Successfully";
                        ModelState.Clear();
                    }
                }
                return View();
            }
            catch
            {
                return View();
            }
        }

        // public bool MayBookBeCreated(string title)
        // {
        //     bool result = false;
        //     if (!string.IsNullOrEmpty(title))
        //     {
        //         result = true;
        //     }
        //
        //     return result;
        // }
        
    }
}