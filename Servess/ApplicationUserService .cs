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
            var queryable = _userManager.Users.Select(u => new ApplicaionuserVM
            {
                Id = u.Id,
                CustomerType = u.CustomerType,
                Gender = u.Gender
            });

            int pageNumber = applicationUserVM.PageNumber ?? 1;

            // Assuming GetPagedData is a method in PaginationHelper that takes an IQueryable and a page number
            var pagedList =   GetPagedData(queryable, pageNumber);

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
                UserNme = user.UserName
            };
        }

        public async Task<ApplicaionuserVM> CreateAsync(ApplicaionuserVM userVm)
        {
            var newUser = new Applicaionuser
            {
                UserName = userVm.Id, // Typically, you set UserName or Email, not Id directly
                CustomerType = userVm.CustomerType,
                Gender = userVm.Gender,
                NormalizedUserName = userVm.UserNme ?? "" // Assign UserName from userVm.UserNme or empty string if null
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
            existingUser.UserName=userVm.UserNme;

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

