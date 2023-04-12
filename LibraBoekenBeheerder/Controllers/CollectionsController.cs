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
        private readonly CollectionsDAL _collectionsDal;
        private CollectionsMapper _collectionsMapper = new CollectionsMapper();

        public CollectionsController(IConfiguration _configuration)
        {
            _collectionsDal = new CollectionsDAL(_configuration);
        }


        [HttpGet]
        public ActionResult Index(CollectionsModel collectionsModel)
        {
            var dto = _collectionsMapper.toDTO(collectionsModel);

            List<CollectionsModel> collectionsModels = new List<CollectionsModel>();

            var dtoList = _collectionsDal.GetAllCollections();
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
        public ActionResult Create(CollectionsModel collectionsModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var _dto = _collectionsMapper.toDTO(collectionsModel);
                    if (_collectionsDal.CreateCollection(_dto))
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

