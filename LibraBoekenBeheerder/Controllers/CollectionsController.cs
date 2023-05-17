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
        public ActionResult Index()
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
                var collectionDropDownList = _collectionClassClass.ReturnCollectionsContaintingBook(id, config);
        
                List<SelectListItem> items = collectionDropDownList.Select(cddl => new SelectListItem
                {
                    Text = cddl.Name.ToString()
                }).ToList();
        
                ViewBag.collectionDropDownList = items;
        
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
    } 
}

