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
    public class CollectionsController : Controller
    {
        private Collection collectionClass = new Collection();
        private CollectionsMapper _collectionsMapper = new CollectionsMapper();

        public CollectionsController(IConfiguration _configuration)
        {
            collectionClass = new Collection();
        }


        [HttpGet]
        public ActionResult Index(CollectionsModel collectionsModel, IConfiguration configuration)
        {
            var toClass = _collectionsMapper.toClass(collectionsModel);

            List<CollectionsModel> collectionsModels = new List<CollectionsModel>();

            var collectionList = collectionClass.ReturnAllCollections(configuration);
            foreach (var Item in collectionList)
            {
                var modelItem = _collectionsMapper.toModel(Item);
                collectionsModels.Add(modelItem);
            }
            return View(collectionsModels);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(CollectionsModel collectionsModel, IConfiguration configuration)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var toClass = _collectionsMapper.toClass(collectionsModel);
                    if (collectionClass.CreateCollection(toClass, configuration))
                    {
                        ViewBag.Message = "Collection has been added succesfully";
                        ModelState.Clear();
                    }
                    else
                    {
                        ViewBag.Message = "Error 69: Error occured while creating the collection";
                    }
                }
            }
            catch (Exception e)
            {
                ViewBag.Message = "Error 420: Error occured while creating the collection";
                throw;
            }
            return View(collectionsModel);
        }
    } 
}

