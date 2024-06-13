using C_Utilities;

using Cf_Viewmodels;
using DataAcessLayers;
using Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
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
            //productVm.CategoryIdList = _unitOfWork._Ilookup.GetCategoryType();
            //ModelState.Remove("CategoryName");

            //productVm.SystemUserId= GetCurrentUserInfo().Result.UserId;
            //productVm.SystemUserName= GetCurrentUserInfo().Result.UserName;
           
       
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

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _unitOfWork._NotPayedmoneyHistoryServess.DeleteNotPayedmoney(id);

                TempData["Message"] = "Cannot save the category. Please check the form.";
                TempData["MessageType"] = "danger";
                return Json(new { success = true, message = "Successfully deleted!" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #region history 
        [HttpGet]
 





        [HttpPost]
        public IActionResult SaveHistory(NotPayedmoneyHistoryVM Entity)
        {

            
            _unitOfWork._NotPayedmoneyHistoryServess.SaveNotPayedmoney(Entity);
            TempData["Message"] = $"{Entity.payedAmount}  تم اضافه المنتج بسعر "; // "Added successfully"
            TempData["MessageType"] = "Save";

            return Json(new { success = true, message = TempData["Message"] });

        }

     
        [HttpPost]
        public IActionResult DeleteHistory(int id)
        {
            try
            {
                _unitOfWork._NotPayedmoneyHistoryServess.DeleteNotPayedmoneyHistory(id);

                TempData["Message"] = "Cannot save the category. Please check the form.";
                TempData["MessageType"] = "danger";
                return Json(new { success = true, message = "Successfully deleted!" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
            #endregion


        }
    }
