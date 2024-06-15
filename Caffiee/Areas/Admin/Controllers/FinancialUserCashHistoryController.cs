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
         
            var products = _unitOfWork._iFinancialUserCashHistoryServess.SearchFinancialUserCashH(Entity);
            return View(products);
        }  
        
   

        // GET: Products/Details/5
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

      

        

      

        [HttpPost]
        public IActionResult Delete(int Id)
        {
            try
            {
                _unitOfWork._iFinancialUserCashHistoryServess.DeleteFinancialUserCash(Id);

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
        public IActionResult DeleteHistory(int Id)
        {
            try
            {
                _unitOfWork._iFinancialUserCashHistoryServess.DeleteFinancialUserCashHistories(Id);

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
