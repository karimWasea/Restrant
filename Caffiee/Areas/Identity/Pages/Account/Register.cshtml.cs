// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading;
using System.Threading.Tasks;

using C_Utilities;

using DataAcessLayers;

using Interfaces;

using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;

using Servess;

using static C_Utilities.Enumes;

namespace Caffiee.Areas.Identity.Pages.Account
{
     
    public class RegisterModel : PageModel
    {
        private UnitOfWork _unitOfWork;

        private readonly SignInManager<Applicaionuser> _signInManager;
        private readonly UserManager<Applicaionuser> _userManager;
        private readonly IUserStore<Applicaionuser> _userStore;
        private readonly IUserEmailStore<Applicaionuser> _emailStore;
        private readonly ILogger<RegisterModel> _logger;
        private readonly IEmailSender _emailSender;
        private readonly RoleManager<IdentityRole> _roles;

        public RegisterModel(UnitOfWork unitOfWork,
            UserManager<Applicaionuser> userManager,
            IUserStore<Applicaionuser> userStore,
            SignInManager<Applicaionuser> signInManager,
            ILogger<RegisterModel> logger,
            IEmailSender emailSender ,
            RoleManager<IdentityRole> roles)
        {
            _roles=roles;   
            _unitOfWork = unitOfWork;
            _userManager = userManager;
            _userStore = userStore;
            _emailStore = GetEmailStore();
            _signInManager = signInManager;
            _logger = logger;
            _emailSender = emailSender;
            _roles = roles;

        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        ///  


        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            public List<SelectListItem> CustomerTypeList { get; set; }

            public CustomerType Custemertype { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }
            public string UserName { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }
            [Required]
            [Display(Name = "Gender")]
            public Gender Gender { get; set; }
            List<SelectListItem> CustomerType { get; set; } = new List<SelectListItem>();
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summaryCustemertype
            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }


        public async Task OnGetAsync(string returnUrl = null)
        {
            // Fetch department options and store them in ViewData
            var Custemertyp = _unitOfWork._Ilookup.GetCustomerType();
           
            ViewData["CustomerType"] = Custemertyp;
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = CreateUser();
                user.FullCustumName = Input.UserName;
                user.IsAdmin = true;


                await _userStore.SetUserNameAsync(user, Input.UserName, CancellationToken.None);
                await _emailStore.SetEmailAsync(user, Input.Email, CancellationToken.None);
                var result = await _userManager.CreateAsync(user, Input.Password);

                if (result.Succeeded)
                {

                    _logger.LogInformation("User created a new account with password.");
                    
                        if (!_roles.Roles.Any())
                        {
                            using (IDbContextTransaction transaction = await _unitOfWork. _context.Database.BeginTransactionAsync())
                            {
                                try
                                {
                                    var superAdminRole = new IdentityRole
                                    {
                                        Name = ConstsntValuse.SuperAdmin,
                                        Id = Guid.NewGuid().ToString()
                                    };
                                    var salesManRole = new IdentityRole
                                    {
                                        Name = ConstsntValuse.SalesMan,
                                        Id = Guid.NewGuid().ToString()
                                    }; 
                                var SalessManger = new IdentityRole
                                    {
                                        Name = ConstsntValuse.SalessManger,
                                        Id = Guid.NewGuid().ToString()
                                    };   
                            
                            
                                var Usersessrole = new IdentityRole
                                    {
                                        Name = ConstsntValuse.Users,
                                        Id = Guid.NewGuid().ToString()
                                    };

                                    // Create roles
                                    var createSuperAdminRoleResult = await _roles.CreateAsync(superAdminRole);
                                    var createSalesManRoleResult = await _roles.CreateAsync(salesManRole);
                                   var createSalessMangerRoleResult = await _roles.CreateAsync(SalessManger);
                                   var createUsersrRoleResult = await _roles.CreateAsync(Usersessrole);

                                    if (!createSuperAdminRoleResult.Succeeded || !createSalesManRoleResult.Succeeded)
                                    {
                                        // Rollback if any role creation failed
                                        await transaction.RollbackAsync();
                                        throw new Exception("Failed to create roles");
                                    }

                                    // Save changes
                                    await _unitOfWork. _context.SaveChangesAsync();

                                    // Assign user to SuperAdmin role
                                    var addToRoleResult = await _userManager.AddToRoleAsync(user, ConstsntValuse.SuperAdmin);

                                    if (!addToRoleResult.Succeeded)
                                    {
                                        // Rollback if assigning role to user failed
                                        await transaction.RollbackAsync();
                                        throw new Exception("Failed to add user to SuperAdmin role");
                                    }

                                    // Commit the transaction if all operations succeed
                                    await transaction.CommitAsync();
                                }
                                catch (Exception)
                                {
                                    // Rollback the transaction in case of any error
                                    await transaction.RollbackAsync();
                                    throw;
                                }

                            }
                        }
                    else
                    {
                        // Assign user to SuperAdmin role
                        var addToRoleResult = await _userManager.AddToRoleAsync(user, ConstsntValuse.Users);

                    }
                                var userId = await _userManager.GetUserIdAsync(user);
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { area = "Identity", userId = userId, code = code, returnUrl = returnUrl },
                        protocol: Request.Scheme);

                    await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    if (_userManager.Options.SignIn.RequireConfirmedAccount)
                    {
                        return RedirectToPage("RegisterConfirmation", new { email = Input.Email, returnUrl = returnUrl });
                    }
                    else
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        return LocalRedirect(returnUrl);
                    }
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }

        private Applicaionuser CreateUser()
        {
            try
            {
                return Activator.CreateInstance<Applicaionuser>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(Applicaionuser)}'. " +
                    $"Ensure that '{nameof(Applicaionuser)}' is not an abstract class and has a parameterless constructor, or alternatively " +
                    $"override the register page in /Areas/Identity/Pages/Account/Register.cshtml");
            }
        }

        private IUserEmailStore<Applicaionuser> GetEmailStore()
        {
            if (!_userManager.SupportsUserEmail)
            {
                throw new NotSupportedException("The default UI requires a user store with email support.");
            }
            return (IUserEmailStore<Applicaionuser>)_userStore;
        }
    }
}
