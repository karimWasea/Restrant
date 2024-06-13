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

    public class CategoryController : BaseController
    {

        public CategoryController(
        UnitOfWork unitOfWork,
        UserManager<Applicaionuser> userManager,
        SignInManager<Applicaionuser> signInManager) : base(unitOfWork, userManager, signInManager)
        {

        }

        // GET: Products
        [HttpGet]

        public IActionResult Index(CategoryVm Entity, int? page)
        {
            //int pageNumber = page ?? 1;

            Entity.PageNumber = page ?? 1;
            var products = _unitOfWork._Category.Search(Entity);
            return View(products);
        }

        // GET: Products/Details/5
        public IActionResult Details(int id)
        {
            var product = _unitOfWork._Category.Get(id);
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

                var   Entity = _unitOfWork._Category.Get(Id);
                Entity.CategoryIdList = _unitOfWork._Ilookup.GetCategoryType();


                return View(  Entity);

            }
            else
            { var cat= new CategoryVm();
                cat.CategoryIdList = _unitOfWork._Ilookup.GetCategoryType();


                return View(cat);


            }

        }



        // POST: Products/Create
        [HttpPost]
        //[ValidateAntiForgeryToken]
        public IActionResult Save(CategoryVm Entity  )
        {
            Entity.CategoryIdList = _unitOfWork._Ilookup.GetCategoryType();

            if (!ModelState.IsValid)
            {
                TempData["Message"] = "Cannot save the category. Please check the form.";
                TempData["MessageType"] = "danger";
                return View(Entity);

            }
            if (Entity.Imge == null)
            {
                TempData["Message"] = "Cannot save the category. Please check the form.";
                TempData["MessageType"] = "danger";
            }
            if (!_unitOfWork._Category.CheckIfExisit(Entity))
            {
                _unitOfWork._Category.Save(Entity);
                TempData["Message"] = $" successfully Add!";
                TempData["MessageType"] = "Save";
                return RedirectToAction(nameof(Index));


            }
            TempData["Message"] = $" cannott save!";
            TempData["MessageType"] = "Delete";

            return View(Entity);

        }

        [HttpPost]
        public IActionResult Delete(int id)
        {
            try
            {
                _unitOfWork._Category.Delete(id);
                return Json(new { success = true, message = "Successfully deleted!" });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }




    }
}
