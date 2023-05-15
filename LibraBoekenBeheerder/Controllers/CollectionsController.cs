using System;  
using System.Collections.Generic;
using System.Data;
using LibraBoekenBeheerder.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Data.SqlClient;
using LibraDTO;
using LibraLogic;

namespace LibraBoekenBeheerder.Controllers
{
    public class CollectionsController : Controller
    {
        private readonly Collection collectionClass;
        private CollectionsMapper _collectionsMapper = new CollectionsMapper();

        public CollectionsController()
        {
        }


        [HttpGet]
        public ActionResult Index(CollectionsModel collectionsModel, IConfiguration configuration)
        {
            var dto = _collectionsMapper.toClass(collectionsModel);

            List<CollectionsModel> collectionsModels = new List<CollectionsModel>();

            var dtoList = collectionClass.ReturnAllCollections(configuration);
            foreach (var dtoItem in dtoList)
            {
                var modelItem = _collectionsMapper.toModel(dtoItem);
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
                    var _dto = _collectionsMapper.toClass(collectionsModel);
                    if (collectionClass.CreateCollection(_dto, configuration))
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
                ViewBag.Message = $"Error 420: Error occured while creating the collection {e.Message}";
                throw;
            }
            return View(collectionsModel);
        }
    } 
}

