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

    public class ProductController :  BaseController
    {
        Imgoperation Imgoperation;
        public ProductController(
        UnitOfWork unitOfWork, Imgoperation imgoperation,
        UserManager<Applicaionuser> userManager,
        SignInManager<Applicaionuser> signInManager) : base(unitOfWork, userManager, signInManager)
        {
            Imgoperation=imgoperation;
        }

        // GET: Products
        [HttpGet]

        public IActionResult Index(productVM Entity, int? page )
        {

            Entity.PageNumber = page ?? 1;
             ViewBag.CategoryIdList = _unitOfWork._Ilookup.GetCategoryType();

            var products = _unitOfWork._Product.Search(Entity);
            return View(products);
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            var product = _unitOfWork._Product.Get(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public IActionResult Save(int Id)
        {
            if (Id > 0)
            {

                var product = _unitOfWork._Product.Get(Id);
                product.CategoryIdList = _unitOfWork._Ilookup.GetCategoryType();


                return View(product);

            }
            else
            { var Poduct= new productVM();
                Poduct.CategoryIdList = _unitOfWork._Ilookup.GetCategoryType();

                return View(Poduct);


            }

        }

        

        // POST: Products/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Save(productVM productVm)
        { 
            productVm.CategoryIdList = _unitOfWork._Ilookup.GetCategoryType();
            ModelState.Remove("CategoryName");

            //productVm.SystemUserId= GetCurrentUserInfo().Result.UserId;
            //productVm.SystemUserName= GetCurrentUserInfo().Result.UserName;
           
       
            if (!ModelState.IsValid)
            {
                TempData["Message"] = " ادخل البيانات";
                TempData["MessageType"] = "danger";
                return View(productVm);

            }
            if (!_unitOfWork._Product .CheckIfExisit(productVm))
            {
                _unitOfWork._Product.Save(productVm);
                TempData["Message"] = "  تم الاضافه";
                 return RedirectToAction(nameof(Index));


            }
            TempData["Message"] = "  هذا المنتج موجود مسبقا ";
            TempData["MessageType"] = "danger";

            return View(productVm);

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _unitOfWork._Product.Delete(id);

                TempData["Message"] = " تم الحذف";
                TempData["MessageType"] = "Delete";
                return Json(new { success = true, message = "تم الحذف " });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }




    }
}
