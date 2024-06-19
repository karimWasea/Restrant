using Microsoft.AspNetCore.Identity;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace C_Utilities
{
    public static class ConstsntValuse
    {
        //areaname
        public const string Admin = "Admin";
        //rollname
        public const string SuperAdmin = "SuperAdmin";
        public const string SalessManger = "SalessManger";
        public const string SalesMan = "SalesMan";
        public const string hospitalacont = "hospitalacont";
    }
    public class CustomUserValidator<TUser> : IUserValidator<TUser> where TUser : class
    {
        public Task<IdentityResult> ValidateAsync(UserManager<TUser> manager, TUser user)
        {
            return Task.FromResult(IdentityResult.Success);
        }
    }

}
