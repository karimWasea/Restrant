//using C_Utilities;
//using Caffiee.Models;

//using DataAcessLayers;

//using Microsoft.AspNetCore.Identity;
//using Microsoft.AspNetCore.Mvc;

//using Servess;

//using System.Diagnostics;

//namespace Caffiee.Areas.Admin.Controllers
//{

//    [Area(ConstsntValuse.Admin)]
//    //[Authorize(Roles = SystemRols.SuperAdmin)]
//    public class CustomerTypeController : BaseController
//    {
//        public CustomerTypeController(
//         UnitOfWork unitOfWork,
//         UserManager<Applicaionuser> userManager,
//         SignInManager<Applicaionuser> signInManager) : base(unitOfWork, userManager, signInManager)
//        {

//        }



//        public IActionResult Index()
//        {
//            return View();
//        }

//        public IActionResult Privacy()
//        {
//            return View();
//        }

//        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
//        public IActionResult Error()
//        {
//            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
//        }
//    }
//}
