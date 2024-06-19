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

 

    public class UserController : BaseController
    {
        public UserController(
        UnitOfWork unitOfWork, 
        UserManager<Applicaionuser> userManager,
        SignInManager<Applicaionuser> signInManager) : base(unitOfWork, userManager, signInManager)
        {
            
        }
        [HttpGet]

        public async Task<IActionResult> Index(ApplicaionuserVM user)
        {
            ViewBag.GetCustomerType = _unitOfWork._Ilookup.GetCustomerType();

            var users = await _unitOfWork._userService.Search(user);
            return View(users);
        }

        public async Task<IActionResult> Details(string id)
        {
            var user = await _unitOfWork._userService.GetByIdAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return View(user);
        }

      

   

        [HttpGet]
        public async Task<IActionResult> Save(string id)
        {
            if (id != null)
            {
                var user = await _unitOfWork._userService.GetByIdAsync(id);
                user.CustomerTypeList= _unitOfWork._Ilookup.GetCustomerType();    
                user.GenderList= _unitOfWork._Ilookup.GetGenderType();    
                if (user == null)
                {
                    return NotFound();
                }
                return View(user);

            }else
            {
                var auserr = new ApplicaionuserVM();
                auserr.CustomerTypeList = _unitOfWork._Ilookup.GetCustomerType();
                auserr.GenderList = _unitOfWork._Ilookup.GetGenderType();
                return View(auserr);

            }

        }    [HttpPost]
        public async Task<IActionResult> Save(ApplicaionuserVM user)
        {
            user.CustomerTypeList = _unitOfWork._Ilookup.GetCustomerType();
            user.GenderList = _unitOfWork._Ilookup.GetGenderType();
            if (await _unitOfWork._userService.CheckIfExists(user))
            {
                TempData["Message"] = "هذا الاكونت موجود مسبقا";
                TempData["MessageType"] = "Save";
                return View(user);

            }
         
            if (!ModelState.IsValid)
            {
                TempData["Message"] = "ادخل البيانات صحيحه";
                TempData["MessageType"] = "Save";
                return View(user);
            }
            
         
            if (user.Id != null&& user.Id!=string.Empty)
            {
                var updatedUser = await _unitOfWork._userService.UpdateAsync(user);
                TempData["Message"] = "تم التعديل بنجاح";
                TempData["MessageType"] = "Save";

                return RedirectToAction(nameof(Index));
            }
            else {
                ApplicaionuserVM? createdUser = await _unitOfWork._userService.CreateAsync(user);
                TempData["Message"] = "تم الاضافه بنجاح";
                TempData["MessageType"] = "Save";
                return RedirectToAction(nameof(Index));
            }
        }

     



        public async Task<JsonResult> Delete(string id)
        {
            try
            {
                var success = await _unitOfWork._userService.DeleteAsync(id);
               

                TempData["Message"] = "تم الحذف بنجاح";
                TempData["MessageType"] = "Delete";
                return Json(new { success = true, message = "Successfully deleted!" });

            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }





       
    }

}
