﻿using C_Utilities;

using Cf_Viewmodels;
using DataAcessLayers;
using Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

using Servess;

namespace Caffiee.Areas.Admin.Controllers
{
    [Area(ConstsntValuse.Admin)]

    public class NotPayedmoneyHistoryController   :  BaseController
    {
        Imgoperation Imgoperation;
        public NotPayedmoneyHistoryController(
        UnitOfWork unitOfWork, Imgoperation imgoperation,
        UserManager<Applicaionuser> userManager,
        SignInManager<Applicaionuser> signInManager) : base(unitOfWork, userManager, signInManager)
        {
            Imgoperation=imgoperation;
        }

        // GET: Products
        [HttpGet]

        public IActionResult Index(NotPayedmoneyHistoryVM Entity, int? page )
        {

            Entity.PageNumber = page ?? 1;
            ViewBag.AllUsers = _unitOfWork._Ilookup.Users();
            ViewBag.GetPaymentStatus = _unitOfWork._Ilookup.GetPaymentStatus();
            var products = _unitOfWork._NotPayedmoneyHistoryServess.SearchNotPayedmoney(Entity);
            return View(products);
        }
        [HttpGet]
        public IActionResult PrintHospital(NotPayedmoneyHistoryVM Entity, int? page )
        {

            Entity.PageNumber = page ?? 1;
            var products = _unitOfWork._NotPayedmoneyHistoryServess.PrintforHospitallDay(Entity);
            return View(products);
        }

        // GET: Products/Details/5
        public IActionResult SearchNotPayedmoneyHistoryDetails(int id , int? page)
        {
            page = page ?? 1;

            var product = _unitOfWork._NotPayedmoneyHistoryServess.SearchNotPayedmoneyHistoryDetails(id ,page);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

      

        

         [HttpPost]
         public IActionResult Save(NotPayedmoneyHistoryVM productVm)
        { 
             
           
       
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Cannot save the category. Please check the form.";
                TempData["MessageType"] = "danger";
                var model = _unitOfWork._NotPayedmoneyHistoryServess.SearchNotPayedmoney(productVm);
                return View("Index", model);
            }
            if (_unitOfWork._NotPayedmoneyHistoryServess.CheckIfExisitNotPayedmoney(productVm.Id))
            {
                _unitOfWork._NotPayedmoneyHistoryServess.SaveNotPayedmoney(productVm);
                TempData["Message"] = "Cannot save the category. Please check the form.";
                TempData["MessageType"] = "danger";
                return RedirectToAction(nameof(Index));


            }
            TempData["Message"] = "Cannot save the category. Please check the form.";
            TempData["MessageType"] = "danger";
            var product = _unitOfWork._NotPayedmoneyHistoryServess.SearchNotPayedmoney(productVm);
            return View("Index", product);

        }

         [HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                _unitOfWork._NotPayedmoneyHistoryServess.DeleteNotPayedmoney(id);

                TempData["Message"] = "Cannot save the category. Please check the form.";
                TempData["MessageType"] = "danger";
                var prooduct = new NotPayedmoneyHistoryVM();
                var product = _unitOfWork._NotPayedmoneyHistoryServess.SearchNotPayedmoney(prooduct);
                return View("Index", product);
            }
            catch (Exception ex)
            {
                var prooduct = new NotPayedmoneyHistoryVM();
                var product = _unitOfWork._NotPayedmoneyHistoryServess.SearchNotPayedmoney(prooduct);
                return View("Index", product);
            }
        }

        #region history 
  





        [HttpPost]
        public IActionResult SaveHistory(NotPayedmoneyHistoryVM Entity)
        {
             var id=Entity.Id;  
                if (Entity.Qantity == null)
                {

                    TempData["Message"] = $"ادخل كميه "; // "Added successfully"
                    TempData["MessageType"] = "Save";

                var model = _unitOfWork._NotPayedmoneyHistoryServess.SearchNotPayedmoneyHistoryDetails(id, 1);
            }

                if (!_unitOfWork._PriceProductebytypes.CheckQantityProduct((int)Entity.Productid, (decimal)Entity.Qantity))
                {
                    TempData["Message"] = $"الكميه ف المخزن غير كافيه "; // "Added successfully"
                    TempData["MessageType"] = "Save";
                var model2 = _unitOfWork._NotPayedmoneyHistoryServess.SearchNotPayedmoneyHistoryDetails(id,1);
                return View(viewName: "SearchNotPayedmoneyHistoryDetails", model2);

            }

            _unitOfWork._NotPayedmoneyHistoryServess.SaveNotPayedmoney(Entity);
            TempData["Message"] = $"{Entity.payedAmount}  تم اضافه المنتج بسعر "; // "Added successfully"
            TempData["MessageType"] = "Save";

            var model3 = _unitOfWork._NotPayedmoneyHistoryServess.SearchNotPayedmoneyHistoryDetails(id, 1);
            return View(viewName: "SearchNotPayedmoneyHistoryDetails", model3);


        }



        [HttpPost]
        public IActionResult DeleteHistory(int id ,int payedTotalAmount ,int NotPayedmoneyid , int productid)
        {
            try
            {
                _unitOfWork._NotPayedmoneyHistoryServess.DeleteFinancialUserCashHistories(  id,   payedTotalAmount,   NotPayedmoneyid,   productid);

                TempData["Message"] = "Cannot save the category. Please check the form.";
                TempData["MessageType"] = "danger";
                return Json(new { success = true, message = "Successfully deleted!" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }










        [HttpGet]
        public IActionResult Salesreturns(int id)
        {
            try
            {
                _unitOfWork._NotPayedmoneyHistoryServess.Salesreturns(id);

                TempData["Message"] = "Cannot save the category. Please check the form.";
                TempData["MessageType"] = "danger";
                var prooduct = new NotPayedmoneyHistoryVM();
                var product = _unitOfWork._NotPayedmoneyHistoryServess.SearchNotPayedmoney(prooduct);
                return View("Index", product);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
            #endregion


        }
    }
