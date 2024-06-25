using C_Utilities;

using Cf_Viewmodels;
using DataAcessLayers;
using Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Servess;

using System.Data;

using static C_Utilities.Enumes;

namespace Caffiee.Areas.Admin.Controllers
{
    [Area(ConstsntValuse.Admin)]

    [Authorize(Roles = $"{ConstsntValuse.SuperAdmin},{ConstsntValuse.SalesMan}")]
    //[Authorize(Roles = ConstsntValuse.SalesMan)]

    public class PriceProductebytypesController : BaseController
    {


        public PriceProductebytypesController(
            UnitOfWork unitOfWork,
            UserManager<Applicaionuser> userManager,
            SignInManager<Applicaionuser> signInManager) : base(unitOfWork, userManager, signInManager)
        {

        }
        [HttpGet]

        public IActionResult Index(PriceProductebytypesVM Entity, int? page)
        {

            Entity.PageNumber = page ?? 1;
            ViewBag.CustomerTypeIdList = _unitOfWork._Ilookup.GetCustomerType();
            var Entitys = _unitOfWork._PriceProductebytypes.Search(Entity);
            return View(Entitys);
        }


        [Authorize(Roles = $"{ConstsntValuse.SuperAdmin},{ConstsntValuse.SalesMan}")]

        [HttpGet]
        [Route("Admin/PriceProductebytypes/GetProductbytyp")]
         public IActionResult GetProductbytyp(PriceProductebytypesVM Entity, int? page)
        {
            ViewBag.CustomerType = Entity.CustomerType;
            ViewBag.UsersLists = _unitOfWork._Ilookup.Users(Entity.CustomerType);
            ViewBag.HospitalOroprationtypLists = _unitOfWork._Ilookup.HospitalOroprationtyp( );
            Entity.PageNumber = page ?? 1;
            var Entitys = _unitOfWork._PriceProductebytypes.SearchForTypes(Entity);
            return View(Entitys);
        }





        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            var Entity = _unitOfWork._PriceProductebytypes.Get(id);
            if (Entity == null)
            {
                return NotFound();
            }
            return View(Entity);
        }

        // GET: Products/Create [Authorize(Roles = ConstsntValuse.SalesMan)]
 
         public IActionResult Save(int Id, int ProductId)
        {
            if (Id > 0)
            {

                var Entity = _unitOfWork._PriceProductebytypes.Get(Id);
                Entity.ProductId = ProductId;
                Entity.ProductName = _unitOfWork._Product.Get(ProductId).ProductName;
                Entity.ProductOldPrice = (int?)_unitOfWork._Product.Get(ProductId).Price;
                //product.CategoryIdList = _unitOfWork._Ilookup.GetCategories();

                Entity.CustomerTypeIdList = _unitOfWork._Ilookup.GetCustomerType();

                return View(Entity);

            }
            else
            {
                var Entitys = new PriceProductebytypesVM();
                Entitys.ProductId = ProductId;
                Entitys.ProductName = _unitOfWork._Product.Get(ProductId).ProductName;
                Entitys.ProductOldPrice = (int?)_unitOfWork._Product.Get(ProductId).Price;

                Entitys.CustomerTypeIdList = _unitOfWork._Ilookup.GetCustomerType();

                return View(Entitys);


            }

        }



        // POST: Products/Create
        [HttpPost]
 
         //[ValidateAntiForgeryToken]
        public IActionResult Save(PriceProductebytypesVM Entity)
        {
            Entity.CustomerTypeIdList = _unitOfWork._Ilookup.GetCustomerType();
            ModelState.Remove("CategoryName");
            ModelState.Remove("ProductName");
            ModelState.Remove("CustomerTypeName");
            ModelState.Remove("ProductName");
            ModelState.Remove("NotpayedUserid");

            if (!ModelState.IsValid)
            {
                return View(Entity);

            }
            if (!_unitOfWork._PriceProductebytypes.CheckIfExisit(Entity))
            {
                _unitOfWork._PriceProductebytypes.Save(Entity);
                TempData["Message"] = "تم الحفظ بنجاح";
                TempData["MessageType"] = "Save";

                return RedirectToAction(nameof(Index));


            }
            TempData["Message"] = " لم يتم الحفظ";
            TempData["MessageType"] = "danger";

            return View(Entity);

        }

        [HttpPost]
 
         public IActionResult Delete(int id)
        {
            try
            {
                _unitOfWork._PriceProductebytypes.Delete(id);
                TempData["Message"] = " تم الحذف";
                TempData["MessageType"] = "danger";
                return Json(new { success = true, message = "Successfully deleted!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }




        #region   notpayedcard
       
        
        [HttpGet]
 
         public IActionResult GetallfromShopingCartNopayed(PriceProductebytypesVM Entity, int? page)
        {
            ViewBag.CustomerType = Entity.CustomerType;
            ViewBag.Catid = Entity.Catid;
            Entity.PageNumber = page ?? 1;
            IEnumerable<PriceProductebytypesVM>? Entitys = _unitOfWork._PriceProductebytypes.GetallfromShopingCartNopayed(Entity);
            return View(Entitys); // Replace "ShoppingCartPartial" with your actual partial view name
        }

        [HttpPost]
 
         public IActionResult AddShopingCaterNotpayedHistory(PriceProductebytypesVM Entity)
        
        {
            
            if (!_unitOfWork._PriceProductebytypes.CheckQantityProduct(Entity.ProductId, (decimal)Entity.ShopingCaterQantity))
            {
                TempData["Message"] = "الكميه غير كافيه فالمخزن";
                TempData["MessageType"] = "Delete";

                return Json(new { success = true, message = TempData["Message"] });

            }

         
               ModelState.Remove("Qantity");
            if (!(Entity.HospitalOroprationtypId== HospitalOroprationtyp.Hospital &&string.IsNullOrEmpty(Entity.NotpayedUserid)))
            {
                ModelState.Remove("HospitalOroprationtypId");

                if (!ModelState.IsValid)
                {
                    TempData["Message"] = " ادخل بيانات العميل او نوع العميل ";
                    TempData["MessageType"] = "Save";

                    return Json(new { success = true, message = TempData["Message"] });

                }
            }

            if (!_unitOfWork._PriceProductebytypes.CheckIfOneCustmeidinshopingingcard(Entity))
            {
                TempData["Message"] = "يجب ان تكون الفاتوره لعميل واحد";
                TempData["MessageType"] = "Delete";

                return Json(new { success = true, message = TempData["Message"] });
            }
            Entity.totalprice = Entity.price * Entity.ShopingCaterQantity;
            _unitOfWork._PriceProductebytypes.AddShopingCaterNotpayedHistory(Entity);
            TempData["Message"] = $"  تم الحفظ "; // "Added successfully"
            TempData["MessageType"] = "Save";

            return Json(new { success = true, message = TempData["Message"] });
        }




        //[HttpPost]
 
         public IActionResult UpdateShopingCaterNotpayedHistory(PriceProductebytypesVM Entity)

        {
           

            if (Entity.ShopingCaterQantity == null)
            {
                TempData["Message"] = $"  ادخل الكميه"; // "Added successfully"
                TempData["MessageType"] = "Delete";
                IEnumerable<PriceProductebytypesVM>? Model2 = _unitOfWork._PriceProductebytypes.GetallfromShopingCartNopayed(Entity);
                return View("GetallfromShopingCartNopayed", Model2);


            }
            if (!_unitOfWork._PriceProductebytypes.CheckQantityProduct(Entity.ProductId, (decimal)Entity.ShopingCaterQantity))
            {
                TempData["Message"] = "الكميه غير كافيه فالمخزن";
                TempData["MessageType"] = "Delete";

                IEnumerable<PriceProductebytypesVM>? Model3 = _unitOfWork._PriceProductebytypes.GetallfromShopingCartNopayed(Entity);
                return View("GetallfromShopingCartNopayed", Model3);

            }
            _unitOfWork._PriceProductebytypes.UpdateShopingCaterNotpayedHistory(Entity);
            TempData["Message"] = $"  تم التعديل بنجاح"; // "Added successfully"
            TempData["MessageType"] = "Edit";
            IEnumerable<PriceProductebytypesVM>? Model = _unitOfWork._PriceProductebytypes.GetallfromShopingCartNopayed(Entity);
            return View("GetallfromShopingCartNopayed" , Model);
        }




 
         public IActionResult DeleteNopayed(int Id)
        {

            _unitOfWork._PriceProductebytypes.DeleteShopingCaterNotpayedHistory(Id);
            var Entity = new PriceProductebytypesVM();
           var model = _unitOfWork._PriceProductebytypes.GetallfromShopingCartNopayed(Entity);
            TempData["Message"] = $" تم الحذف بنجاح "; // "Added successfully"
            TempData["MessageType"] = "Delete";
            return View("GetallfromShopingCartNopayed", model);
        }

        #endregion
        #region ShopingCater
      
        [HttpGet]
 
         public IActionResult GetallfromShopingCart(PriceProductebytypesVM Entity, int? page)
        {
            ViewBag.CustomerType = Entity.CustomerType;
            ViewBag.Catid = Entity.Catid;
            Entity.PageNumber = page ?? 1;
            var Entitys = _unitOfWork._PriceProductebytypes.GetallfromShopingCart(Entity);
            return View(Entitys); // Replace "ShoppingCartPartial" with your actual partial view name
        }

        [HttpPost]
        public IActionResult AddShopingCaterCashHistory(PriceProductebytypesVM Entity)

        { 
 
            if (Entity.ShopingCaterQantity == null)
            {
                TempData["Message"] = $"   ادخل الكميه  "; // "Added successfully"
                TempData["MessageType"] = "delete";

                return Json(new { success = true, message = TempData["Message"] });
            }
            if (!_unitOfWork._PriceProductebytypes.CheckQantityProduct(Entity.ProductId, (decimal)Entity.ShopingCaterQantity))
            {
                TempData["Message"] = $"    الكميه فالمخزن غير كافيه  "; // "Added successfully"
                TempData["MessageType"] = "delete";

                return Json(new { success = true, message = TempData["Message"] });
            }

        
            Entity.totalprice = Entity.price * Entity.ShopingCaterQantity;
            _unitOfWork._PriceProductebytypes.AddShopingCaterCashHistory(Entity);
            TempData["Message"] = $"{Entity.totalprice}  تم اضافه المنتج بسعر "; // "Added successfully"
            TempData["MessageType"] = "Save";

            return Json(new { success = true, message = TempData["Message"] });
        }
 
         public IActionResult UpdateShopingCaterCashHistory(PriceProductebytypesVM Entity)

        {







            if (Entity.ShopingCaterQantity == null)
            {
                TempData["Message"] = $"  ادخل الكميه"; // "Added successfully"
                TempData["MessageType"] = "Delete";
                var model2 = _unitOfWork._PriceProductebytypes.GetallfromShopingCart(Entity);

                return View("GetallfromShopingCart", model2);
            }
            if (!_unitOfWork._PriceProductebytypes.CheckQantityProduct(Entity.ProductId, (decimal)Entity.ShopingCaterQantity))
            {
                TempData["Message"] = "الكميه غير كافيه فالمخزن";
                TempData["MessageType"] = "Delete";

                var model3 = _unitOfWork._PriceProductebytypes.GetallfromShopingCart(Entity);

                return View("GetallfromShopingCart", model3);

            }


            Entity.totalprice = Entity.price * Entity.ShopingCaterQantity;
            _unitOfWork._PriceProductebytypes.UpdateShopingCaterCashHistory(Entity);
            TempData["Message"] = $"{Entity.totalprice}  تم التعديل   بنجاح "; // "Added successfully"
            TempData["MessageType"] = "Edit";
             var model = _unitOfWork._PriceProductebytypes.GetallfromShopingCart(Entity);

            return View("GetallfromShopingCart", model);
        }

 
         public IActionResult DeleteCash(int Id)
        {

            _unitOfWork._PriceProductebytypes.DeleteShopingCaterCashHistory(Id);
            TempData["Message"] = $" تم الحذف بنجاح "; // "Added successfully"
            TempData["MessageType"] = "Delete";
            var Entity = new PriceProductebytypesVM();
            var model = _unitOfWork._PriceProductebytypes.GetallfromShopingCart(Entity);

            return View("GetallfromShopingCart" , model);
        }

        #endregion


        #region freecart
        [HttpGet]
 
         public IActionResult FreeFinancialUserCash(CustomerType CustomerType  )
        {
            var Entity = new PriceProductebytypesVM();

            if (!_unitOfWork._PriceProductebytypes.checkedifShopingCaterCashHistoryHavedata())
            {
                TempData["Message"] = $"لايوجد بيانات  "; // "Added successfully"
                TempData["MessageType"] = "delete";

                var model2 = _unitOfWork._PriceProductebytypes.SearchForTypes(Entity);
                return View("GetProductbytyp", model2);

            }

            Entity.CustomerType = (CustomerType)CustomerType;
                _unitOfWork._PriceProductebytypes.FreeShopingCaterCashHistoryToFinancialUserCash( "","");
            var model = _unitOfWork._PriceProductebytypes.SearchForTypes(Entity);
            return View("GetProductbytyp" , model); // Replace "ShoppingCartPartial" with your actual partial view name
        }   [HttpGet]

        public IActionResult FreeShopingCaterCashHistoryToNotpayed(CustomerType CustomerType )
        {
            var Entity = new PriceProductebytypesVM();

            if (!_unitOfWork._PriceProductebytypes.checkedifhopingCaterNotpayedHavedata())
            {
                TempData["Message"] = $"لايوجد بيانات  "; // "Added successfully"
                TempData["MessageType"] = "delete";

                var model2 = _unitOfWork._PriceProductebytypes.SearchForTypes(Entity);
                return View(viewName: "GetProductbytyp", model2);

            }
            Entity.CustomerType = (CustomerType)CustomerType;
                _unitOfWork._PriceProductebytypes.FreeShopingCaterCashHistoryToNotpayed( "","");
            var model = _unitOfWork._PriceProductebytypes.SearchForTypes(Entity);
            return View(viewName: "GetProductbytyp" , model); // Replace "ShoppingCartPartial" with your actual partial view name
        }

        #endregion

        #region enddebite
        [HttpGet]
         public IActionResult EnDDebiteForPersone(string NotpayedUserid)
        {
            try
            {
                var entity = new PriceProductebytypesVM
                {
                    NotpayedUserid = NotpayedUserid,
                    UsersLists = _unitOfWork._Ilookup.Users()
                };

                _unitOfWork._PriceProductebytypes.EnDDebite(entity);

                return Json(new { success = true, message = "تم انهاء الدين بنجاح." });
            }
            catch (Exception ex)
            {
                // Log the exception (implement logging as per your application needs)
                 return Json(new { success = false, message = "حدث خطأ أثناء إنهاء الدين." });
            }
        }


        public IActionResult GetDibts(PriceProductebytypesVM Entity )
            {

            var ReturnEntity = _unitOfWork._PriceProductebytypes.GetDibts(Entity);
            ReturnEntity.UsersLists = _unitOfWork._Ilookup.Users();


            return View(ReturnEntity); // Replace "ShoppingCartPartial" with your actual partial view name


        }


         public IActionResult EnDDebiteForHospital()
        {
            var Entity = new PriceProductebytypesVM();
            var ReturnEntity = _unitOfWork._PriceProductebytypes.EnDDebiteHospital();
            Entity.UsersLists = _unitOfWork._Ilookup.Users();

            TempData["Message"] = $"تم الانهاء بنجاح "; // "Added successfully"
            TempData["MessageType"] = "Save";


            return View(viewName: "GetDibts", Entity); // Replace "ShoppingCartPartial" with your actual partial view name


        }

        public IActionResult GetDibtsHospital()
        {

            var ReturnEntity = _unitOfWork._PriceProductebytypes.GetDibtsHospital();

            ReturnEntity.UsersLists= _unitOfWork._Ilookup.Users();
            return View(viewName: "GetDibts" , ReturnEntity); // Replace "ShoppingCartPartial" with your actual partial view name


        }










        #endregion















    }




}
