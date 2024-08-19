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
    //[Authorize(Roles = ConstsntValuse.SuperAdmin)]
    [Authorize(Roles = $"{ConstsntValuse.SuperAdmin},{ConstsntValuse.SalessManger}")]
     
    public class FinancialUserCashHistoryController :  BaseController
    {
        Imgoperation Imgoperation;
        public FinancialUserCashHistoryController(
        UnitOfWork unitOfWork, Imgoperation imgoperation,
        UserManager<Applicaionuser> userManager,
        SignInManager<Applicaionuser> signInManager) : base(unitOfWork, userManager, signInManager)
        {
            Imgoperation=imgoperation;
        }
         
        // GET: Products
        [HttpGet]

        public IActionResult Index(FinancialUserCashHistoryVM Entity, int? page )
        {

            Entity.PageNumber = page ?? 1;
            ViewBag.totalldaycash = _unitOfWork._iFinancialUserCashHistoryServess.CalCCashByDay();
            var products = _unitOfWork._iFinancialUserCashHistoryServess.SearchFinancialUserCashH(Entity);
            return View(products);
        }




        [HttpGet]
        public IActionResult SearchedByDay(DateTime? dayDate)
        {
            var model = new PaymentTotalandNotpayMoyPaymentTotalByDay();

            // If dayDate is null, default it to today's date
            if (dayDate == null || !dayDate.HasValue)
            {
                dayDate = DateTime.Today;
            }

            // Proceed with the logic since dayDate now has a value
            var result = _unitOfWork._iFinancialUserCashHistoryServess.GetPaymentTotalForDay(dayDate.Value);
            model.totalcash = result.TotalPayment;
            model.totalNotpayed = result.TotalNotPayed;
            model.Sumforday = (result.TotalPayment ?? 0) + (result.TotalNotPayed ?? 0);
            model.totalpayedNotpayedForday =  _unitOfWork._NotPayedmoneyHistoryServess.SumOfPaidInDay();
            model.Seacheday = dayDate; // Assign the date to the SearchedDate property

            return View(model);
        }










        [HttpGet]
        public IActionResult Salesreturns(int id)
        {
            try
            {
                _unitOfWork._iFinancialUserCashHistoryServess.Salesreturns(id);

                TempData["Message"] = "تم  المرتجع";
                TempData["MessageType"] = "Save";
                var Entity = new FinancialUserCashHistoryVM();
                var product = _unitOfWork._iFinancialUserCashHistoryServess.SearchFinancialUserCashH(Entity);
                return View("Index", product);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        // GET: Products/Details/5
        [HttpGet]
        public IActionResult SearchFinancialUserCashHistoryDetails(int Id, int? page)
        {
            page = page ?? 1;

            var product = _unitOfWork._iFinancialUserCashHistoryServess.SearchFinancialUserCashHistoryDetails(Id, page);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

      

        

      

         public IActionResult Delete(int Id)
        {
            try
            {
                _unitOfWork._iFinancialUserCashHistoryServess.DeleteFinancialUserCash(Id);

                TempData["Message"] = " تم الحذف.";
                TempData["MessageType"] = "Delete";
                var model = new FinancialUserCashHistoryVM();
                var products = _unitOfWork._iFinancialUserCashHistoryServess.SearchFinancialUserCashH(model);
                 return View("Index", products);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }

        #region history 







         public IActionResult DeleteHistory(int id, int payedTotalAmount, int frercahid, int productid)
        {
            var Id= id;
            try
            {
                _unitOfWork._iFinancialUserCashHistoryServess.DeleteFinancialUserCashHistories(id, payedTotalAmount, frercahid, productid);

                TempData["Message"] = "تم الامر  بنجاح";
                TempData["MessageType"] = "delete";
                var model = _unitOfWork._iFinancialUserCashHistoryServess.SearchFinancialUserCashHistoryDetails(Id, 1);
                return View("SearchFinancialUserCashHistoryDetails", model);
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }




        [HttpPost]
        public IActionResult SaveHistory(FinancialUserCashHistoryVM entity)
        {
            var id = entity.Id;
            if (entity.Qantity == null)
            {
                TempData["Message"] = "ادخل كميه"; // "Enter Quantity"
                TempData["MessageType"] = "Error";

                var model = _unitOfWork._iFinancialUserCashHistoryServess.SearchFinancialUserCashHistoryDetails(id, 1);
                return View("SearchFinancialUserCashHistoryDetails", model);
            }

            if (!_unitOfWork._PriceProductebytypes.CheckQantityProduct((int)entity.Productid, (decimal)entity.Qantity))
            {
                TempData["Message"] = "الكميه ف المخزن غير كافيه"; // "Insufficient stock quantity"
                TempData["MessageType"] = "Error";

                var model = _unitOfWork._iFinancialUserCashHistoryServess.SearchFinancialUserCashHistoryDetails(id, 1);
                return View("SearchFinancialUserCashHistoryDetails", model);
            }

            _unitOfWork._iFinancialUserCashHistoryServess.SaveFinancialUserCashHistories(entity);
            TempData["Message"] = "تم اضافه بنجاح"; // "Added successfully"
            TempData["MessageType"] = "Success";

            var updatedModel = _unitOfWork._iFinancialUserCashHistoryServess.SearchFinancialUserCashHistoryDetails(id, 1);
            return View("SearchFinancialUserCashHistoryDetails", updatedModel);
        }

        #endregion


    }
}
