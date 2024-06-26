using C_Utilities;

using Cf_Viewmodels;
using DataAcessLayers;
using Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;

using Servess;

namespace Caffiee.Areas.Admin.Controllers
{
    [Area(ConstsntValuse.Admin)]

    [Authorize(Roles = $"{ConstsntValuse.SuperAdmin},{ConstsntValuse.SalessManger}")]

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
            ViewBag.HospitalOroprationtyp = _unitOfWork._Ilookup.HospitalOroprationtyp();
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
                TempData["Message"] = "يوجد خطاء فالبيانات";
                TempData["MessageType"] = "delete";
                var model = _unitOfWork._NotPayedmoneyHistoryServess.SearchNotPayedmoney(productVm);
                return View("Index", model);
            }

            bool exists = _unitOfWork._NotPayedmoneyHistoryServess.CheckIfExisitNotPayedmoney(productVm.Id);
            bool saveSuccessful = _unitOfWork._NotPayedmoneyHistoryServess.SaveNotPayedmoney(productVm);

            if (exists && saveSuccessful)
            {
                TempData["Message"] = "تم الحفظ بنجاح";
                TempData["MessageType"] = "Save";
                return RedirectToAction(nameof(Index));
            }

            TempData["Message"] = "يوجد خطاء";
            TempData["MessageType"] = "delete";
            var product = _unitOfWork._NotPayedmoneyHistoryServess.SearchNotPayedmoney(productVm);
            return View("Index", product);
        }


        //[HttpGet]
        public IActionResult Delete(int id)
        {
            try
            {
                _unitOfWork._NotPayedmoneyHistoryServess.DeleteNotPayedmoney(id);

                TempData["Message"] = "تم الحذف بنجاح";
                TempData["MessageType"] = "Delete";
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
              
                return View(viewName: "SearchNotPayedmoneyHistoryDetails", model);
            }

                if (!_unitOfWork._PriceProductebytypes.CheckQantityProduct((int)Entity.Productid, (decimal)Entity.Qantity))
                {
                    TempData["Message"] = $"الكميه ف المخزن غير كافيه "; // "Added successfully"
                    TempData["MessageType"] = "Save";
                var model2 = _unitOfWork._NotPayedmoneyHistoryServess.SearchNotPayedmoneyHistoryDetails(id,1);
                return View(viewName: "SearchNotPayedmoneyHistoryDetails", model2);

            }

        var saved=    _unitOfWork._NotPayedmoneyHistoryServess.SaveNotPayedmoneyHistory(Entity);
            //if (!saved)
            //{
            //    TempData["Message"] = $"{Entity.payedAmount}  تم اضافه المنتج بسعر "; // "Added successfully"
            //    TempData["MessageType"] = "Save";

            //    var model4 = _unitOfWork._NotPayedmoneyHistoryServess.SearchNotPayedmoneyHistoryDetails(id, 1);
            //    return View(viewName: "SearchNotPayedmoneyHistoryDetails", model4);
            //}
            TempData["Message"] = $" تم الاضافه   "; // "Added successfully"
            TempData["MessageType"] = "Save";

            var model3 = _unitOfWork._NotPayedmoneyHistoryServess.SearchNotPayedmoneyHistoryDetails(id, 1);
            return View(viewName: "SearchNotPayedmoneyHistoryDetails", model3);


        }



         public IActionResult DeleteHistory(int id ,int payedTotalAmount ,int NotPayedmoneyid , int productid)
        {
            try
            {
                _unitOfWork._NotPayedmoneyHistoryServess.DeleteFinancialUserCashHistories(  id,   payedTotalAmount,   NotPayedmoneyid,   productid);

                TempData["Message"] = "  تم  الحذف بنجاح";
                TempData["MessageType"] = "Delete";
                var model3 = _unitOfWork._NotPayedmoneyHistoryServess.SearchNotPayedmoneyHistoryDetails(id, 1);
                return View(viewName: "SearchNotPayedmoneyHistoryDetails", model3);
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

                TempData["Message"] = " تم بنجاح";
                TempData["MessageType"] = " save";
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
