using Caffiee.Models;

using DataAcessLayers;

using Interfaces;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using Servess;

using System.Diagnostics;

namespace Caffiee.Areas.Admin.Controllers
{

    public class BaseController : Controller
    {
        protected UnitOfWork _unitOfWork;
        private readonly UserManager<Applicaionuser> _userManager;
        private readonly SignInManager<Applicaionuser> _signInManager;

        public BaseController(
            UnitOfWork unitOfWork,
            UserManager<Applicaionuser> userManager,
            SignInManager<Applicaionuser> signInManager)  
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // Method to get the current user ID
        protected async Task<(string UserId, string UserName)> GetCurrentUserInfo()
        {
            var user = await _userManager.GetUserAsync(User);
            return (user?.Id, user?.UserName);
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
