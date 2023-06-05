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
using System.Configuration;

namespace LibraBoekenBeheerder.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly CollectionClass _collectionClassClass;
        private readonly Books _booksClass;
        private readonly IConfiguration _configuration;
        private readonly CollectionsMapper _collectionsMapper;

        public CollectionsController(IConfiguration configuration)
        {
            _configuration = configuration;
            _collectionClassClass = new CollectionClass();
            _booksClass = new Books();
            _collectionsMapper = new CollectionsMapper();
        }

        [HttpGet]
        public ActionResult Index(string searchString)
        {
            CollectionsModel collectionsModel = new CollectionsModel();
            
            var dto = _collectionsMapper.toClass(collectionsModel);

            List<CollectionsModel> collectionsModels = new List<CollectionsModel>();

            var dtoList = _collectionClassClass.ReturnAllCollections(_configuration);
            foreach (var dtoItem in dtoList)
            {
                var modelItem = _collectionsMapper.toModel(dtoItem);
                collectionsModels.Add(modelItem);
            }
            
            // Search
            //niet een geweldige manier maar nou hoef ik tenminste geen javscr*pt te gebruiken.
            if (!String.IsNullOrEmpty(searchString))
            {
                collectionsModels = collectionsModels.Where(s => s.Name!.Contains(searchString)).ToList();
            }
            return View(collectionsModels);
        }

        [HttpPost]
        public ActionResult Create(CollectionsModel collectionsModel, IConfiguration configuration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _dto = _collectionsMapper.toClass(collectionsModel);
                    if (_collectionClassClass.CreateCollection(_dto, configuration))
                    {
                        ViewBag.Message = "CollectionClass has been added succesfully";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = "Error 69: Error occured while creating the collectionClass";
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = $"Error 420: Error occured while creating the collectionClass {e.Message}";
                throw;
            }
            return View(collectionsModel);
        }

        [HttpGet]
        public ActionResult Details(int id)
        {
            try
            {
                IConfiguration config = _configuration;
                var collectionBooksList = _collectionClassClass.ReturnBooksInCollection(id, config);
        
                List<BooksModel> items = collectionBooksList.Select(cddl => new BooksModel
                {
                    Title = cddl.Title.ToString(),
                    BookId = cddl.BookId
                }).ToList();
        
                ViewBag.collectionBooksList = items;
        
                var collectionDto = _collectionClassClass.ReturnACollection(config, id);
        
                if (collectionDto != null)
                {
                    var collectionMapper = new CollectionsMapper();
                    var collectionModel = collectionMapper.toModel(collectionDto);
                    return View(collectionModel);
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
        public ActionResult DeleteBook(int collectionID, int bookId)
        {
            IConfiguration configuration = _configuration;

            if (_collectionClassClass.RemoveLinkBookToCollection(collectionID, bookId, _configuration))
            {
                ViewBag.Message = "Book succesfully removed from collection";
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
        
        public ActionResult Delete(int id)
        {
                
            if (_collectionClassClass.DeleteCollection(_configuration, id))
            {
                ViewBag.Message = "Collection succesfully deleted";
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
    } 
}

