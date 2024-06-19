using AutoMapper;
using C_Utilities;

using Cf_Viewmodels;
using DataAcessLayers;
using Interfaces;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using X.PagedList;

using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Servess.FinancialUserCashHistoryServess;

namespace Servess
{
    public class ApplicationUserService : PaginationHelper<FinancialUserCashHistoryVM>, IUserService
    {
        private readonly UserManager<Applicaionuser> _userManager;

        public ApplicationUserService(UserManager<Applicaionuser> userManager)
        {
            _userManager = userManager;
        }


        public async Task<IPagedList<ApplicaionuserVM>> Search(ApplicaionuserVM applicationUserVM)
        {
            var queryable = _userManager.Users.Where(i => i.IsAdmin == false


            && (i.CustomerType == applicationUserVM.CustomerType || applicationUserVM.CustomerType == 0 || applicationUserVM.CustomerType == 0) && (i.FullCustumName.Contains(applicationUserVM.UserNme) || applicationUserVM.UserNme == null)).Select(u => new ApplicaionuserVM
            {
                Id = u.Id,
                CustomerType = u.CustomerType,
                Gender = u.Gender
                 , UserNme = u.FullCustumName ?? "",
            });

            int pageNumber = applicationUserVM.PageNumber ?? 1;

            // Assuming GetPagedData is a method in PaginationHelper that takes an IQueryable and a page number
            var pagedList = GetPagedData(queryable, pageNumber);

            return pagedList;
        }


        public async Task<ApplicaionuserVM> GetByIdAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return null;

            return new ApplicaionuserVM
            {
                Id = user.Id,
                CustomerType = user.CustomerType,
                Gender = user.Gender,
                UserNme = user.FullCustumName
            };
        }




        public async Task<bool> CheckIfExists(ApplicaionuserVM userVm)
        {
            // Check if a user with the same CustomerType and FullCustumName exists, excluding the current user
            var exists = await _userManager.Users
                .AnyAsync(i => i.Id != userVm.Id && i.CustomerType == userVm.CustomerType && i.FullCustumName == userVm.UserNme);

            return exists;
        }


        public async Task<ApplicaionuserVM> CreateAsync(ApplicaionuserVM userVm)
        {
            var random = new Random();
            int randomNumber = random.Next(3, 9999); // Generate a random number between 1000 and 9999

            var newUser = new Applicaionuser
            {
                FullCustumName = userVm.UserNme, // Typically, you set UserName or Email, not Id directly
                CustomerType = userVm.CustomerType,
                Gender = userVm.Gender,
                UserName = $"karim.{randomNumber}@hh.com",
                NormalizedUserName = $"karim.{randomNumber}@hh.com".ToUpper() // NormalizedUserName should be in upper case
            };

            var result = await _userManager.CreateAsync(newUser);
            if (result.Succeeded)
            {
                userVm.Id = newUser.Id;
                return userVm;
            }
            return null;
        }



        public async Task<ApplicaionuserVM> UpdateAsync(ApplicaionuserVM userVm)
        {
            var existingUser = await _userManager.FindByIdAsync(userVm.Id);
            if (existingUser == null) return null;

            existingUser.CustomerType = userVm.CustomerType;
            existingUser.Gender = userVm.Gender;
            existingUser.FullCustumName = userVm.UserNme;

           var result = await _userManager.UpdateAsync(existingUser);
            if (result.Succeeded)
            {
                return userVm;
            }
            return null;
        }

        public async Task<bool> DeleteAsync(string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return false;

            var result = await _userManager.DeleteAsync(user);
            return result.Succeeded;
        }
}    }

