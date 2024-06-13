using AutoMapper;

using Cf_Viewmodels;

using DataAcessLayers;

using Interfaces;

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

using System.ComponentModel;
using System.Linq;
using System.Reflection;

using X.PagedList;

using static C_Utilities.Enumes;

using CustomerType = C_Utilities.Enumes.CustomerType;

namespace Servess
{
    public class lookupServess : Ilookup
    {
        private readonly ApplicationDBcontext _applicationDBcontext;
        private readonly UserManager<Applicaionuser> _user;


        public lookupServess(UserManager<Applicaionuser> userManager, ApplicationDBcontext applicationDBcontext)
        {
            _applicationDBcontext = applicationDBcontext;
            _user = userManager;



        }
        public IQueryable<SelectListItem> Users(CustomerType CustomerType)
        {

            IQueryable<SelectListItem>? applicationuser = _user.Users.Where(i=>i.CustomerType== CustomerType).Select(x => new SelectListItem { Value = x.Id, Text = x.UserName });
            return applicationuser;
        }
        public List<SelectListItem> GetCustomerType()
        {
            var CustomerType = Enum.GetValues(typeof(CustomerType))
                               .Cast<C_Utilities.Enumes.CustomerType>()
                               .Select(d => new SelectListItem
                               {
                                   Value = ((int)d).ToString(),
                                   Text = DescriptionEnum.GetDescription(d)
                               })
                               .ToList();

            return CustomerType;
        }
             public List<SelectListItem> GetGenderType()
        {
            var CustomerType = Enum.GetValues(typeof(Gender))
                               .Cast<C_Utilities.Enumes.Gender>()
                               .Select(d => new SelectListItem
                               {
                                   Value = ((int)d).ToString(),
                                   Text = DescriptionEnum.GetDescription(d)
                               })
                               .ToList();

            return CustomerType;
        }




        //public IQueryable<SelectListItem> GetCustomerTypesId(int selectedId=0)
        //{

        //    IQueryable<SelectListItem>? applicationuser = _applicationDBcontext.CustomerTypes.Select(x => new SelectListItem {

        //        Value = x.Id.ToString(), 

        //        Text = x.TypesName ,
        //        Selected = x.Id == selectedId


        //    }).OrderBy(c => c.Text).AsNoTracking();
        //    return applicationuser;
        //} 

        //public IQueryable<SelectListItem> GetCategories(int selectedId=0)
        //{

        //    IQueryable<SelectListItem>? applicationuser = _applicationDBcontext.Categories.Select(x => new SelectListItem {

        //        Value = x.Id.ToString(), 

        //        Text = x.CategoryName ,
        //        Selected = x.Id == selectedId


        //    }).OrderBy(c => c.Text).AsNoTracking();
        //    return applicationuser;
        //}






        public List<SelectListItem> GetCategoryType()
        {
            var CustomerType = Enum.GetValues(typeof(CategoryType))
                                   .Cast<CategoryType>()
                                   .Select(d => new SelectListItem
                                   {
                                       Value = ((int)d).ToString(),
                                       Text = DescriptionEnum.GetDescription(d)
                                   })
                                   .ToList();

            return CustomerType;
        }
        
        public List<SelectListItem> HospitalOroprationtyp()
        {
            var HospitalOroprationtypd = Enum.GetValues(typeof(HospitalOroprationtyp))
                                   .Cast<HospitalOroprationtyp>()
                                   .Select(d => new SelectListItem
                                   {
                                       Value = ((int)d).ToString(),
                                       Text = DescriptionEnum.GetDescription(d)
                                   })
                                   .ToList();

            return HospitalOroprationtypd;
        }
    }





  public static   class DescriptionEnum{

        public  static string GetDescription(Gender categoryType)
        {
            var fieldInfo = typeof(Gender).GetField(categoryType.ToString());
            var descriptionAttribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();
            return descriptionAttribute?.Description ?? categoryType.ToString();
        }
        public  static string GetDescription(CategoryType categoryType)
        {
            var fieldInfo = typeof(CategoryType).GetField(categoryType.ToString());
            var descriptionAttribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();
            return descriptionAttribute?.Description ?? categoryType.ToString();
        }  
        public  static string GetDescription(HospitalOroprationtyp categoryType)
        {
            var fieldInfo = typeof(HospitalOroprationtyp).GetField(categoryType.ToString());
            var descriptionAttribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();
            return descriptionAttribute?.Description ?? categoryType.ToString();
        }
         public  static string GetDescription(CustomerType CustomerType)
        {
            var fieldInfo = typeof(CustomerType).GetField(CustomerType.ToString());
            var descriptionAttribute = fieldInfo?.GetCustomAttribute<DescriptionAttribute>();
            return descriptionAttribute?.Description ?? CustomerType.ToString();
        }

       

    }
}
